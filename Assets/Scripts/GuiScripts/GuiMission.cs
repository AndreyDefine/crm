using UnityEngine;
using System.Collections;

public class GuiMission : Abstract {
	
	public CrmFont name;
	public CrmFont progressText;
	public GuiProgress progress;
	public GameObject complete;
	public GameObject missionIcoPlace;
	
	public void SetMission(Mission mission){
		name.text = mission.missionName;
		progressText.text = mission.GetLongProgressRepresentation();
		progress.SetProgress(mission.GetProgress());
		if(mission.GetState()==MissionStates.FINISHED){
			complete.active = true;
		}else{
			complete.active = false;		
		}
		MissionIco missionIco = Instantiate(mission.iconPrefab) as MissionIco;
		missionIco.singleTransform.parent = missionIcoPlace.transform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
}
