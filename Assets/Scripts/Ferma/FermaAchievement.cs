using UnityEngine;
using System.Collections;

public class FermaAchievement : Abstract {
	public CrmFont achievementName;
	public CrmFont progressText;
	public GuiProgress progress;
	public GuiProgress progressBlick;
	public FermaAchievementMission fermaAchievementMission;
	
	public void SetMissionEmmitter(BaseMissionEmmitter missionEmmitter){
		
		ArrayList missions = missionEmmitter.GetCurrentMissions();
		Debug.Log(missions.Count);
		if(missions.Count>0){
			fermaAchievementMission.SetMission((Mission)missions[0]);
		}
		progressText.text = string.Format("{0}/{1}",missionEmmitter.GetCountFinishedMissions(),missionEmmitter.GetCountMissions());
		achievementName.text = missionEmmitter.missionEmmitterName;
		progress.SetProgressWithColor(missionEmmitter.GetProgress());
		progressBlick.SetProgress(missionEmmitter.GetProgress());
	}
	
}
