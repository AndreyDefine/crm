using UnityEngine;
using System.Collections;


public class CurrentMissionNotifier : BaseNotifier, IMissionListener
{
	public Abstract missionIconPlace;
	public GameObject complete;
	public CrmFont crmFont;
	private MissionFlyingIco missionFlyingIco = null;
	private Mission mission;
	
	void Start(){
		complete.SetActive(false);	
	}
		
	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		mission.RemoveMissionListener(this);
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		SetText(mission.GetProgressRepresentation());
		if(missionIconPlace.GetComponent<Animation> () == null){
			AnimationFactory.Attention(missionIconPlace,0.5f,1.2f,"Attention");
		}else if(!missionIconPlace.animation.isPlaying){
			missionIconPlace.animation.Play("Attention");
		}
	}
	
	public void MissionActivated (Mission mission)
	{
		//do nothing
	}
	
	private void SetText(string text){
		crmFont.text = text;
	}
	
	public void MissionFinished (Mission mission)
	{
		complete.SetActive(true);
		crmFont.gameObject.SetActive(false);
		FlyOut();
	}
	
	public void SetMission(Mission mission){
		mission.SetActive();
		this.priority = mission.GetPriority();
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
	
	public override void FlyInStopped(){
		FlyInEnd();
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
	
	public void MissionFlyingIcoFlyStopped(){
		MissionIco missionIco = missionFlyingIco.GetMissionIco();
		missionIco.singleTransform.parent = missionIconPlace.singleTransform;
		AnimationFactory.FlyXYZ(missionIco, new Vector3(0f,0f,-0.01f),0.8f,"flyXYZFin");
		Destroy(missionFlyingIco.gameObject);
		missionFlyingIco = null;
	}
	
	public void SetMissionIco(MissionIco missionIco){
		missionIco.singleTransform.parent = missionIconPlace.singleTransform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
}
