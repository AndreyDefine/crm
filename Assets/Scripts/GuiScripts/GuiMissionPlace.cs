using UnityEngine;
using System.Collections;

public class GuiMissionPlace : GuiButtonBase {
	
	public GameObject complete;
	public Abstract missionIcoPlace;
	public DialogFermaMissionInfo dialogFermaMissionInfo;
	DialogFermaMissionInfo dialogInfo = null;
	MissionIco missionIco = null;
	Mission mission;
	bool hasMission = false;
	public void SetMission(Mission mission){
		this.mission = mission;
		if(mission.GetState()==MissionStates.FINISHED){
			complete.active = true;
		}else{
			complete.active = false;	
		}
		hasMission = true;
		missionIco = Instantiate(mission.iconPrefab) as MissionIco;
		missionIco.singleTransform.parent = missionIcoPlace.singleTransform;
		missionIco.singleTransform.localScale = new Vector3(1f,1f,1f);//tk2d bug?
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.1f);
	}
	
	protected override void MakeOnTouch ()
	{
		if(hasMission){
			dialogInfo = Instantiate(dialogFermaMissionInfo) as DialogFermaMissionInfo;
			//ArrayList missionsToBuy = fermaMissions.GetFermaLocationPlace().missionEmmitter.GetAvailableNotBoughtMissionsPrefabs();
			dialogInfo.SetMission(mission);
			dialogInfo.singleTransform.parent = singleTransform;
			dialogInfo.singleTransform.localPosition = new Vector3(0f,0f,-0.5f);
			dialogInfo.singleTransform.position = new Vector3(0f,0f,dialogInfo.singleTransform.position.z);
			dialogInfo.Show();
		}
	}
}
