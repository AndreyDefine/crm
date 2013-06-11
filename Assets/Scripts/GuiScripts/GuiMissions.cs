using UnityEngine;
using System.Collections;

public class GuiMissions : Abstract
{

	public GuiMission[] guiMissions;
	
	public void InitMissions ()
	{
		BaseMissionEmmitter[] emmitters = new BaseMissionEmmitter[3];
		MissionEmmitters missionEmmitters = GlobalOptions.GetMissionEmmitters();
		emmitters[0] = missionEmmitters.GetFermaMissionEmmitter();
		emmitters[1] = missionEmmitters.collectMissionEmmitter;
		emmitters[2] = missionEmmitters.runMissionEmmitter;
		int j = 0;
		for (int i=0; i<3; i++) {
			Mission mission = null;
			ArrayList currentMissions = emmitters [i].GetCurrentMissions ();
			ArrayList thisLifeFinishedMissions = emmitters [i].GetThisLifeFinishedMissions ();
			if (currentMissions.Count > 0) {
				mission = currentMissions [0] as Mission;
			} else if (thisLifeFinishedMissions.Count > 0) {
				mission = thisLifeFinishedMissions [thisLifeFinishedMissions.Count - 1] as Mission;
			}
			if (mission != null) {
				guiMissions [j].gameObject.SetActive (true);
				guiMissions [j].SetMission (mission);
				j++;
			}
		}
		for (; j<3; j++) {
			guiMissions [j].gameObject.SetActive (false);
		}
	}
}
