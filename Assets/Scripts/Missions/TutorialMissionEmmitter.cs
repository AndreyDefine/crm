using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialMissionEmmitter : SimpleMissionEmmitter
{
	public override void EmmitMissions (bool force = false)
	{
		if(!PersonInfo.tutorial){
			return;
		}
		if(!force&&(GlobalOptions.gameState!=GameStates.GAME||IsTimeOut())){
			return;
		}
		if (currentMissions.Count == 0) {//инициализируем текущую
			Mission currentMission = GetOneMissionObject ();	
			if (currentMission != null) {
				currentMissions.Add (currentMission);
			}else{
				PersonInfo.FinishTutorial();
			}
			CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
		}
	}
}
