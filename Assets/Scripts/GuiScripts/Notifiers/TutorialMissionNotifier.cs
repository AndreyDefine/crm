using UnityEngine;
using System.Collections;


public class TutorialMissionNotifier : MissionNotifier , IMissionListener
{
	Abstract tutorial = null;
	public CompleteFX completePrefab;
	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		mission.RemoveMissionListener(this);
	}
	
	public override void SetMission (Mission mission)
	{
		base.SetMission (mission);
		mission.AddMissionListener(this);
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		//do nothing
	}
	
	public void MissionActivated (Mission mission)
	{
		//do nothing
	}
	
	public void MissionFinished (Mission mission)
	{
		CompleteFX completeFx = Instantiate(completePrefab) as CompleteFX;
		completeFx.singleTransform.parent = missionIconPlace.singleTransform;
		completeFx.singleTransform.localPosition = new Vector3(0f,0f,-0.2f);
		completeFx.Play();
		FlyOut();
	}	
	
	protected override void Update ()
	{
	}

	public override void FlyInStopped ()
	{
		base.FlyInStopped();
		mission.SetActive();
		tutorial = Instantiate(((BaseTutorialMission)mission).tutorialPrefab) as Abstract;
		tutorial.singleTransform.position = new Vector3(0f,0f,1.5f);
	}
	
	public override void FlyOut (Vector3 outPosition)
	{
		base.FlyOut (outPosition);
		if(tutorial!=null){
			Destroy(tutorial.gameObject);
		}
	}
	
	public void Skip(){
		PersonInfo.FinishTutorial();
		FlyOut();
	}
}
