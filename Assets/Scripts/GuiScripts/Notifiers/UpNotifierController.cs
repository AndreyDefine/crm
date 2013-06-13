using UnityEngine;
using System.Collections;

public class UpNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 1;
	float NOTIFIER_HEIGHT = 0.396f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	//mission notifier
	public MissionNotifier missionNotifierPrefab;
	public TutorialMissionNotifier tutorialMissionNotifierPrefab;
	public TutorialFinihsedNotifier tutorialFinihsedNotifierPrefab;
	
	
	public override void Restart ()
	{
		base.Restart ();
		SetCurrentMissions();
	}
	
	public void SetCurrentMissions(){
		ArrayList missions;
		missions = GlobalOptions.GetMissionEmmitters().GetCurrentMissions();
		for(int i=0;i<missions.Count;i++){
			Mission mission = (Mission)missions[i];
			if(PersonInfo.tutorial){
				AddTutorialMissionNotifier(mission);
			}else{
				if(mission.GetState()==MissionStates.NOT_ACTIVE){
					AddMissionNotifier(mission);
				}
			}
		}
	}
	
	private TutorialFinihsedNotifier GetTutorialFinihsedNotifier(){
		TutorialFinihsedNotifier notifier = Instantiate(tutorialFinihsedNotifierPrefab) as TutorialFinihsedNotifier;	
		return notifier;
	}
	
	public void AddTutorialFinihsedNotifier(){
		TutorialFinihsedNotifier notifier = GetTutorialFinihsedNotifier();
		AddNotifier(notifier);
	}
	
	private TutorialMissionNotifier GetTutorialMissionNotifier(){
		TutorialMissionNotifier notifier = Instantiate(tutorialMissionNotifierPrefab) as TutorialMissionNotifier;	
		return notifier;
	}
	
	public void AddTutorialMissionNotifier(Mission mission){
		TutorialMissionNotifier notifier = GetTutorialMissionNotifier();
		notifier.SetMission(mission);
		AddNotifier(notifier);
	}
	
	private MissionNotifier GetMissionNotifier(){
		MissionNotifier notifier = Instantiate(missionNotifierPrefab) as MissionNotifier;	
		return notifier;
	}
	
	public void AddMissionNotifier(Mission mission){
		MissionNotifier notifier = GetMissionNotifier();
		notifier.SetMission(mission);
		AddNotifier(notifier);
	}
	
	protected override Vector3 GetOutNotifierPlace(int position){
		Vector3 outPosition;
		outPosition = new Vector3 (GlobalOptions.Vsizex/2, GlobalOptions.Vsizey+5, -position*Z_INDEX_PER_POSITION);
		outPosition = GlobalOptions.NormalisePos (outPosition);
		outPosition = Cameras.GetGUICamera ().ScreenToWorldPoint (outPosition);
		
		outPosition.y += NOTIFIER_HEIGHT/2;
		return outPosition;
	}
	
	public override void NotifierWantsToFlyOut (BaseNotifier notifier)
	{
		int position = notifiersInProgress.IndexOf(notifier)-1;
		notifiersInProgress.Remove(notifier);
		notifier.singleTransform.localPosition = new Vector3(notifier.singleTransform.localPosition.x, notifier.singleTransform.localPosition.y, -position*Z_INDEX_PER_POSITION);
		notifier.FlyOut(GetOutNotifierPlace(position));
		ChangePlaces();
		//base.NotifierWantsToFlyOut (notifier);
		//
	}
	
	protected override Vector3 GetInNotifierPlace(int position){
		Vector3 inPostion;
		
		inPostion = new Vector3 (GlobalOptions.Vsizex/2, GlobalOptions.Vsizey-15, -position*Z_INDEX_PER_POSITION);
		inPostion = GlobalOptions.NormalisePos (inPostion);
		inPostion = Cameras.GetGUICamera ().ScreenToWorldPoint (inPostion);
			
		inPostion.y -= NOTIFIER_HEIGHT/2+NOTIFIER_HEIGHT*position+0.02f*position;
		
		return inPostion;
	}
	
	protected override int GetMaxNotifiersToShow ()
	{
		return MAX_NOTIFIERS_TO_SHOW;
	}
	
}
