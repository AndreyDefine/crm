using UnityEngine;
using System.Collections;

public class SimpleMissionEmmitter : Abstract, IMissionEmmitter, IMissionListener
{
	public string misionFinishedTag;
	public int missionRespaunTime;
	public int missionMaxCount;
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
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData(misionFinishedTag+"data_");
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
		return PlayerPrefs.GetInt(misionFinishedTag+id,0)!=0;
	}
	
	public void SetMissionFinished(string id){
		PlayerPrefs.SetInt(misionFinishedTag+id,1);
	}
		
	// Update is called once per frame
	void Update ()
	{
		if(GlobalOptions.gameState!=GameStates.GAME){//TODO: check
			curTime = Time.time;	
			return;
		}
		
		if (Time.time - curTime > missionRespaunTime) {
			if(currentMissions.Count<missionMaxCount&&availableMissionsPrefabs.Count!=0){
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
		CurrentMissionsSerializer.SaveCurrentMissions(currentMissions, misionFinishedTag+"data_");
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
	
	public ArrayList GetCurrentMissions(){
		return currentMissions;
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
	
	public void MissionFinished (Mission mission)
	{
		SetMissionFinished(mission.GetId());
		currentMissions.Remove(mission);
		CurrentMissionsSerializer.SaveCurrentMissions(currentMissions, misionFinishedTag+"data_");
		CurrentMissionsSerializer.RemoveMissionData(mission);
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		if(!mission.oneLife){
			CurrentMissionsSerializer.SaveMissionData(mission);
		}
	}
}