using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurrentMissionsSerializer
{
	
	public static string CURRENT_MISSION_DATA_TAG = "current_mission_data_";
	
	public static void SaveCurrentMissions (ArrayList currentMissions, string tag)
	{
		string currentMissionsData = "";//TODO use stringBuilder?
		for (int i=0; i<currentMissions.Count; i++) {
			Mission mission = (Mission)currentMissions [i];
			if (i != 0) {
				currentMissionsData += "|";
			}
			currentMissionsData += mission.GetId ();
			//Сохраняем данные по одной миссии
			SaveMissionData(mission);
		}
		PlayerPrefs.SetString (tag, currentMissionsData);
	}
	
	public static void SaveMissionData(Mission mission){
		PlayerPrefs.SetString(CURRENT_MISSION_DATA_TAG+mission.GetId(), mission.Serialize ());	
	}
	
	public static void RemoveMissionData(Mission mission){
		PlayerPrefs.DeleteKey(CURRENT_MISSION_DATA_TAG+mission.GetId());
	}
	
	public static string GetMissionData(string missionId){
		return PlayerPrefs.GetString(CURRENT_MISSION_DATA_TAG+missionId,"");
	}
	
	public static Dictionary<string, string> GetCurrentMissionsKeyData (string tag)
	{
		Dictionary<string, string> currentMissionsKeyData = new Dictionary<string, string> ();
		string currentMissionsData = PlayerPrefs.GetString (tag, "");
		if (!currentMissionsData.Equals ("")) {
			char[] splitData = new char[] { '|' };
			string[] currentMissions = currentMissionsData.Split (splitData);
			for (int i=0; i<currentMissions.Length; i++) {
				string id = currentMissions[i];
				currentMissionsKeyData [id] = GetMissionData(id);
			}
		}
		return currentMissionsKeyData;
	}
}
