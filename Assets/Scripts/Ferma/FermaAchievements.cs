using UnityEngine;
using System.Collections;

public class FermaAchievements : Abstract {
	public FermaAchievement fermaAchievementCollect;
	public FermaAchievement fermaAchievementRun;
	
	void Start(){
		fermaAchievementCollect.SetMissionEmmitter(GlobalOptions.GetMissionEmmitters().collectMissionEmmitter);
		fermaAchievementRun.SetMissionEmmitter(GlobalOptions.GetMissionEmmitters().runMissionEmmitter);
	}
}
