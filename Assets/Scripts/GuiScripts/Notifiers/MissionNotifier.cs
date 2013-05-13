using UnityEngine;
using System.Collections;


public class MissionNotifier : BaseNotifier
{
	public Abstract missionIconPlace;
	public CrmFont crmFont;
	private Mission mission;
	private MissionIco missionIco;
	
	private float flyOutTime;
	
	public void SetMission(Mission mission){
		this.mission = mission;
		SetMissionIcon(Instantiate(mission.iconPrefab) as MissionIco);
		SetMissionText(mission.missionName);
	}
	
	private void SetMissionIcon (MissionIco ico){
		int count = missionIconPlace.singleTransform.GetChildCount();
		for(int i=0;i<count;i++){
			Destroy(missionIconPlace.singleTransform.GetChild(0).gameObject);
		}
		ico.singleTransform.parent = missionIconPlace.singleTransform;
		ico.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
		missionIco = ico;
	}
	
	private void SetMissionText (string text){
		crmFont.text = text;
	}
	
	void Update(){
		if(state==NotifierStates.SHOWN&&Time.time-flyOutTime>SHOW_TIME){
			GlobalOptions.GetGuiLayer().AddCurrentMission(this);
			FlyOut();	
		}
	}
	
	public override void FlyInStopped(){
		FlyInEnd();
		flyOutTime = Time.time;
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
	
	public Mission GetMission(){
		return mission;
	}
	
	public MissionIco GetMissionIco(){
		return missionIco;
	}
}
