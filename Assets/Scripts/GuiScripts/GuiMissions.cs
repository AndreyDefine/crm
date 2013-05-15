using UnityEngine;
using System.Collections;

public class GuiMissions : Abstract {

	public GuiMission[] guiMissions;
	
	public void InitMissions(){
		ArrayList missions = GlobalOptions.GetMissionEmmitters().GetThisLifeFinishedMissions();
		missions.AddRange(GlobalOptions.GetMissionEmmitters().GetCurrentMissions());
		
		for(int i=0;i<missions.Count;i++){
			guiMissions[i].gameObject.SetActiveRecursively(true);
			guiMissions[i].SetMission((Mission)missions[i]);
		}
		for(int j=missions.Count;j<guiMissions.Length;j++){
			guiMissions[j].gameObject.SetActiveRecursively(false);
		}
	}
}
