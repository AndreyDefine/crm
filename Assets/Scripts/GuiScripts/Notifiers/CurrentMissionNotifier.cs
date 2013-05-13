using UnityEngine;
using System.Collections;


public class CurrentMissionNotifier : BaseNotifier, IMissionListener
{
	public Abstract missionIconPlace;
	public CrmFont crmFont;
	private MissionFlyingIco missionFlyingIco = null;
	private Mission mission;
	
	public void MissionProgressChanged (Mission mission)
	{
		SetText(mission.GetProgressRepresentation());
		if(missionIconPlace.GetComponent<Animation> () == null){
			AnimationFactory.Attention(missionIconPlace,0.5f,1.2f,"Attention");
		}else if(!missionIconPlace.animation.isPlaying){
			missionIconPlace.animation.Play("Attention");
		}
	}
	
	private void SetText(string text){
		crmFont.text = text;
	}
	
	public void MissionFinished (Mission mission)
	{
		FlyOut();
	}
	
	public void SetMission(Mission mission){
		mission.SetActive();
		this.mission = mission;
		mission.AddMissionListener(this);
		SetText(mission.GetProgressRepresentation());
	}
	
	public void SetMissionFlyingIco(MissionFlyingIco missionFlyingIco){
		this.missionFlyingIco = missionFlyingIco;
		missionFlyingIco.SetCurrentMissionNotifier(this);
	}
	
	public override void FlyIn(Vector3 inPostion){
		base.FlyIn(inPostion);
		if(missionFlyingIco!=null){
			missionFlyingIco.FlyXYZ(inPostion+new Vector3(0f,0f,-1f));
		}
	}
	
	public override void FlyPlace(Vector3 position){
		base.FlyPlace(position);
		if(state==NotifierStates.FLYING_OUT){
			return;
		}
		if(missionFlyingIco!=null){
			missionFlyingIco.FlyXYZ(position+new Vector3(0f,0f,-1f));
		}
		
	}
	
	public override void FlyInStopped(){
		FlyInEnd();
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
	
	public void MissionFlyingIcoFlyStopped(){
		MissionIco missionIco = missionFlyingIco.GetMissionIco();
		SetMissionIco(missionIco);
		Destroy(missionFlyingIco.gameObject);
		missionFlyingIco = null;
	}
	
	public void SetMissionIco(MissionIco missionIco){
		missionIco.singleTransform.parent = missionIconPlace.singleTransform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
}
