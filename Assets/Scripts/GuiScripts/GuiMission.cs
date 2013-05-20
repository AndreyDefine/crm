using UnityEngine;
using System.Collections;

public class GuiMission : Abstract {
	
	public CrmFont missionName;
	public CrmFont progressText;
	public GuiProgress progress;
	public GuiProgress progressBlick;
	public GameObject complete;
	public GameObject missionIcoPlace;
	
	public void SetMission(Mission mission){
		progressText.text = mission.GetLongProgressRepresentation();
		progress.SetProgressWithColor(mission.GetProgress());
		progressBlick.SetProgress(mission.GetProgress());
		if(mission.GetState()==MissionStates.FINISHED){
			complete.active = true;
			missionName.text = mission.missionFinishedText;
		}else{
			complete.active = false;	
			missionName.text = mission.missionName;
		}
		MissionIco missionIco = Instantiate(mission.iconPrefab) as MissionIco;
		missionIco.singleTransform.parent = missionIcoPlace.transform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
}
