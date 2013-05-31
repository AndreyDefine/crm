using UnityEngine;
using System.Collections;

public class GuiMission : Abstract {
	
	public TwoLineText missionName;
	public CrmFont progressText;
	public GuiProgress progress;
	public GuiProgress progressBlick;
	public GuiMissionPlace missionPlacePrefab;
	
	public void SetMission(Mission mission){
		progressText.text = mission.GetLongProgressRepresentation();
		progress.SetProgressWithColor(mission.GetProgress());
		progressBlick.SetProgress(mission.GetProgress());
		if(mission.GetState()==MissionStates.FINISHED){
			missionName.text = mission.missionFinishedText;
		}else{
			missionName.text = mission.missionName;
		}
		
		GuiMissionPlace missionPlace = Instantiate(missionPlacePrefab) as GuiMissionPlace;
		missionPlace.singleTransform.parent = singleTransform;
		missionPlace.singleTransform.localPosition = new Vector3(0f, 0f, -0.04f);
		missionPlace.SetMission(mission);
	}
}
