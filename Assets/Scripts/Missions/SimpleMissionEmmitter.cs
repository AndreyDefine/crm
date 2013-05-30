using UnityEngine;
using System.Collections;

public class SimpleMissionEmmitter : BaseMissionEmmitter
{
	public int priority;
	public int missionRespaunTime;
	private int missionMaxCount = 1;
	private bool canEmmitMission = true;
	float curTime;
	
	void Start ()
	{
		//PlayerPrefs.DeleteAll();//TODO: delete this
		curTime = Time.time;
		
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionCurrentTag);
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
				} else {
					availableMissionsPrefabs.Add (missionPrefab);
				}
			}else{
				finishedMissionsNumber++;
			}
		}
	}
	
	protected override int GetPriority ()
	{
		return priority;
	}
	
	protected override bool canEmmitMissions ()
	{
		return canEmmitMission;
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
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
		GlobalOptions.GetGuiLayer ().AddMission (mission, priority);
		availableMissionsPrefabs.Remove (missionPrefab);
	}
	
	public override ArrayList GetCurrentMissions ()
	{
		return currentMissions;
	}
	
	public override int GetCountFinishedMissions ()
	{
		return finishedMissionsNumber;
	}
}
