using UnityEngine;
using System.Collections;


public class TutorialMissionNotifier : MissionNotifier , IMissionListener
{
	public GameObject complete;
	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		mission.RemoveMissionListener(this);
	}
	
	public override void SetMission (Mission mission)
	{
		base.SetMission (mission);
		mission.AddMissionListener(this);
		complete.SetActive(false);
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
		complete.SetActive(true);
		FlyOut();
	}	
	
	protected override void Update ()
	{
	}

	public override void FlyInStopped ()
	{
		base.FlyInStopped();
		mission.SetActive();
	}
	
	public void Skip(){
		PersonInfo.FinishTutorial();
		FlyOut();
	}
}
