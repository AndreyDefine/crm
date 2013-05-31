using UnityEngine;
using System.Collections;

public class FermaAchievement : Abstract {
	public CrmFont achievementName;
	public CrmFont progressText;
	public GuiProgress progress;
	public GuiProgress progressBlick;
	public GuiMissionPlace missionPlacePrefab;
	
	public void SetMissionEmmitter(BaseMissionEmmitter missionEmmitter){
		
		ArrayList missions = missionEmmitter.GetCurrentMissions();
		Debug.Log(missions.Count);
		
		GuiMissionPlace missionPlace = Instantiate(missionPlacePrefab) as GuiMissionPlace;
		missionPlace.singleTransform.parent = singleTransform;
		missionPlace.singleTransform.localPosition = new Vector3(0f, 0f, -0.04f);
		missionPlace.singleTransform.localScale = new Vector3(1f,1f,1f);//tk2d bug?
		if(missions.Count>0){
			missionPlace.SetMission((Mission)missions[0]);
		}
		
		progressText.text = string.Format("{0}/{1}",missionEmmitter.GetCountFinishedMissions(),missionEmmitter.GetCountMissions());
		achievementName.text = missionEmmitter.missionEmmitterName;
		progress.SetProgressWithColor(missionEmmitter.GetProgress());
		progressBlick.SetProgress(missionEmmitter.GetProgress());
		
		
	}
	
}
