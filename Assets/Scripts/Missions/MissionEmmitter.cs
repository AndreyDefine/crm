using UnityEngine;
using System.Collections;

public class MissionEmmitter : Abstract, IMissionListener
{
	private static string MISSION_FINISHED_TAG="mission_finished_";
	private static int MISSIONS_MAX_COUNT = 4;
	private static int MISSION_RESPAUN_TIME = 8;
	public Mission[] missions;
	private ArrayList availableMissionsPrefabs = new ArrayList();
	Hashtable prefabKeyHashTable = new Hashtable();
	private ArrayList currentMissions = new ArrayList();
	float curTime;

	void Start ()
	{
		//PlayerPrefs.DeleteAll();//TODO: delete this
		curTime = Time.time;
		
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData();
		for(int i=0;i<missions.Length;i++){
			Mission missionPrefab = missions[i];
			string id = missionPrefab.name;
			if(!IsMissionFinished(id)){
				prefabKeyHashTable[missionPrefab] = id;
				if(currentMissionsKeyData.ContainsKey(id)){
					Mission mission = InstantiateMission(missionPrefab);	
					mission.Unserialize(currentMissionsKeyData[id].ToString());
					currentMissions.Add(mission);
					mission.SetActive();
				}else{
					availableMissionsPrefabs.Add(missionPrefab);
				}
			}
		}
	}
	
	public bool IsMissionFinished(string id){
		return PlayerPrefs.GetInt(MISSION_FINISHED_TAG+id,0)!=0;
	}
	
	public void SetMissionFinished(string id){
		PlayerPrefs.SetInt(MISSION_FINISHED_TAG+id,1);
	}
		
	// Update is called once per frame
	void Update ()
	{
		if(GlobalOptions.gameState!=GameStates.GAME){//TODO: check
			curTime = Time.time;	
			return;
		}
		
		if (Time.time - curTime > MISSION_RESPAUN_TIME) {
			if(currentMissions.Count<MISSIONS_MAX_COUNT&&availableMissionsPrefabs.Count!=0){
				AddOneMissionObject ();
			}
			curTime = Time.time;
		}
	}
	
	private void AddOneMissionObject ()
	{
		//int randomIndex = Random.Range (0, availableMissions.Count);
		int randomIndex = 0;
		Mission missionPrefab = (Mission)availableMissionsPrefabs[randomIndex];
		Mission mission = InstantiateMission(missionPrefab);
		currentMissions.Add(mission);
		CurrentMissionsSerializer.SaveCurrentMissions(currentMissions);
		GlobalOptions.GetGuiLayer().AddMission(mission);
		availableMissionsPrefabs.Remove(missionPrefab);
	}
	
	private Mission InstantiateMission(Mission missionPrefab){
		string id = (string)prefabKeyHashTable[missionPrefab];
		Mission mission = (Instantiate (missionPrefab) as Mission);
		mission.AddMissionListener(this);
		mission.SetId(id);
		return mission;
	}
	
	public void NotifyCoinsCollected(int coins){
		ArrayList missions = GetActiveCurrentMissions();
		for(int i=0;i<missions.Count;i++){
			Mission mission = (Mission)missions[i];
			mission.NotifyCoinsCollected(coins);
		}
	}
	
	public void NotifyMetersRunned(int meter){
		ArrayList missions = GetActiveCurrentMissions();
		for(int i=0;i<missions.Count;i++){
			Mission mission = (Mission)missions[i];
			mission.NotifyMetersRunned(meter);
		}
	}
	
	private ArrayList GetActiveCurrentMissions(){
		ArrayList activeMissions = new ArrayList();
		for(int i=0;i<currentMissions.Count;i++){
			if(((Mission)currentMissions[i]).GetState()==MissionStates.ACTIVE){
				activeMissions.Add (currentMissions[i]);
			}
		}
		return activeMissions;	
	}
	
	public ArrayList GetCurrentMissions(){
		return currentMissions;
	}
	
	public void MissionFinished (Mission mission)
	{
		SetMissionFinished(mission.GetId());
		currentMissions.Remove(mission);
		CurrentMissionsSerializer.SaveCurrentMissions(currentMissions);
		CurrentMissionsSerializer.RemoveMissionData(mission);
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		if(!mission.oneLife){
			CurrentMissionsSerializer.SaveMissionData(mission);
		}
	}
}
