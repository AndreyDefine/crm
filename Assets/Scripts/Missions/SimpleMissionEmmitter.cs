using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleMissionEmmitter : BaseMissionEmmitter
{
	public int priority;
	public int missionRespaunTime;
	
	void Start ()
	{
		//PlayerPrefs.DeleteAll();//TODO: delete this
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Dictionary<string, string> currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionCurrentTag);
		for (int i=0; i<missions.Length; i++) {
			Mission missionPrefab = missions [i];
			string id = missionPrefab.name;
			missionPrefab.SetId(id);
			if (!IsMissionFinished (id)) {
				prefabKeyDictionary [missionPrefab] = id;
				if (currentMissionsKeyData.ContainsKey (id)) {
					Mission mission = InstantiateMission (missionPrefab);	
					mission.Unserialize (currentMissionsKeyData [id].ToString ());
					currentMissions.Add (mission);
				} else {
					availableMissionsPrefabs.Add (missionPrefab);
				}
			} else {
				finishedMissionsNumber++;
			}
		}
		EmmitMissions (true);
	}
	
	protected override int GetPriority ()
	{
		return priority;
	}
	
	public override void LevelBegin ()
	{
		base.LevelBegin ();
		EmmitMissions(true);
	}
	
	public virtual void EmmitMissions (bool force = false)
	{
		if(!force&&(GlobalOptions.gameState!=GameStates.GAME||IsTimeOut())){
			return;
		}
		if (currentMissions.Count == 0) {//инициализируем текущую
			Mission currentMission = GetOneMissionObject ();	
			if (currentMission != null) {
				currentMissions.Add (currentMission);
			}
			CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
		}
	}
		
	void Update ()
	{
		EmmitMissions ();
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
