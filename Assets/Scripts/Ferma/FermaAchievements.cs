using UnityEngine;
using System.Collections;

public class FermaAchievements : Abstract {
	public FermaAchievement fermaAchievementCollect;
	public FermaAchievement fermaAchievementRun;
	
	void Start(){
		fermaAchievementCollect.SetMissionEmmitter(GlobalOptions.GetMissionEmmitters().missionEmmitters[1]);
		fermaAchievementRun.SetMissionEmmitter(GlobalOptions.GetMissionEmmitters().missionEmmitters[2]);
	}
}
