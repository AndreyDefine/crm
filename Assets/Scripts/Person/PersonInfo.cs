using UnityEngine;
using System.Collections;

public class PersonInfo {
	public static int GetPersonLevel(){
		Debug.Log(GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions());
		Debug.Log(GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions()/3);
		
		return GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions()/3+1;
	}
	
	public static float GetCurrentLevelProgress(){
		return (GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions()-(GetPersonLevel()-1)*3)/3f;
	}
}
