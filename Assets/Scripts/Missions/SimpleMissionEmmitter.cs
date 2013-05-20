using UnityEngine;
using System.Collections;

public class SimpleMissionEmmitter : BaseMissionEmmitter, IMissionListener
{
	public string misionFinishedTag;
	public int missionRespaunTime;
	private int missionMaxCount = 1;
	public Mission[] missions;
	private ArrayList availableMissionsPrefabs = new ArrayList ();
	Hashtable prefabKeyHashTable = new Hashtable ();
	private ArrayList currentMissions = new ArrayList ();
	private ArrayList thisLifeFinishedMissions = new ArrayList ();
	private bool canEmmitMission = true;
	private int finishedMissionsNumber = 0;
	float curTime;
	
	void Start ()
	{
		//PlayerPrefs.DeleteAll();//TODO: delete this
		curTime = Time.time;
		
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionFinishedTag + "data_");
		for (int i=0; i<missions.Length; i++) {
			Mission missionPrefab = missions [i];
			string id = missionPrefab.name;
			if (!IsMissionFinished (id)) {
				prefabKeyHashTable [missionPrefab] = id;
				if (currentMissionsKeyData.ContainsKey (id)) {
					Mission mission = InstantiateMission (missionPrefab);	
					mission.Unserialize (currentMissionsKeyData [id].ToString ());
					currentMissions.Add (mission);
					canEmmitMission = false;
					mission.SetActive ();
				} else {
					availableMissionsPrefabs.Add (missionPrefab);
				}
			}else{
				finishedMissionsNumber++;
			}
		}
	}
	
	public bool IsMissionFinished (string id)
	{
		return PlayerPrefs.GetInt (misionFinishedTag + id, 0) != 0;
	}
	
	public void SetMissionFinished (string id)
	{
		PlayerPrefs.SetInt (misionFinishedTag + id, 1);
	}
	
	public override void LevelBegin ()
	{
		for(int i=0;i<thisLifeFinishedMissions.Count;i++){
			Destroy(((Mission)thisLifeFinishedMissions[i]).gameObject);
		}
		thisLifeFinishedMissions.Clear();
		if (GetCurrentMissions ().Count == 0) {
			canEmmitMission = true;
		}
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (GlobalOptions.gameState != GameStates.GAME) {//TODO: check
			curTime = Time.time;	
			return;
		}
		
		if (Time.time - curTime > missionRespaunTime) {
			if (canEmmitMission && currentMissions.Count < missionMaxCount && availableMissionsPrefabs.Count != 0) {
				canEmmitMission = false;
				AddOneMissionObject ();
			}
			curTime = Time.time;
		}
	}
	
	private void AddOneMissionObject ()
	{
		//int randomIndex = Random.Range (0, availableMissions.Count);
		int randomIndex = 0;
		Mission missionPrefab = (Mission)availableMissionsPrefabs [randomIndex];
		Mission mission = InstantiateMission (missionPrefab);
		currentMissions.Add (mission);
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionFinishedTag + "data_");
		GlobalOptions.GetGuiLayer ().AddMission (mission);
		availableMissionsPrefabs.Remove (missionPrefab);
	}
	
	private Mission InstantiateMission (Mission missionPrefab)
	{
		string id = (string)prefabKeyHashTable [missionPrefab];
		Mission mission = (Instantiate (missionPrefab) as Mission);
		mission.AddMissionListener (this);
		mission.SetId (id);
		return mission;
	}
	
	public override ArrayList GetCurrentMissions ()
	{
		return currentMissions;
	}
	
	public override ArrayList GetThisLifeFinishedMissions ()
	{
		return thisLifeFinishedMissions;
	}
	
	public void MissionFinished (Mission mission)
	{
		finishedMissionsNumber++;
		SetMissionFinished (mission.GetId ());
		currentMissions.Remove (mission);
		thisLifeFinishedMissions.Add(mission);
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionFinishedTag + "data_");
		CurrentMissionsSerializer.RemoveMissionData (mission);
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		if (!mission.oneLife) {
			CurrentMissionsSerializer.SaveMissionData (mission);
		}
	}
	
	public override int GetCountFinishedMissions ()
	{
		return finishedMissionsNumber;
	}
}
