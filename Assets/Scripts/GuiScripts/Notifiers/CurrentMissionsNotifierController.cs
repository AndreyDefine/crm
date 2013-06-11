using UnityEngine;
using System.Collections;

public class CurrentMissionsNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 5;
	float NOTIFIER_HEIGHT = 0.35f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	public override void Restart ()
	{
		base.Restart ();
		SetCurrentMissions();
	}
	
	private void SetCurrentMissions(){
		if(PersonInfo.tutorial){
			return;
		}
		ArrayList missions = GlobalOptions.GetMissionEmmitters().GetCurrentMissions();
		for(int i=0;i<missions.Count;i++){
			Mission mission = (Mission)missions[i];
			if(mission.GetState()==MissionStates.ACTIVE){
				AddCurrentMissionNotifier(mission);
			}
		}
	}
	
	public MissionFlyingIco missionFlyingIcoPrefab;
	
	//mission notifier
	public CurrentMissionNotifier currentMissionNotifierPrefab;
	
	private CurrentMissionNotifier GetCurrentMissionNotifier(){
		CurrentMissionNotifier notifier = Instantiate(currentMissionNotifierPrefab) as CurrentMissionNotifier;	
		return notifier;
	}
			
	public void AddCurrentMissionNotifier(Mission mission){
		CurrentMissionNotifier notifier = GetCurrentMissionNotifier();
		notifier.SetMission(mission);
		notifier.SetMissionIco(Instantiate(mission.iconPrefab) as MissionIco);
		AddNotifier(notifier);
	}
			
	public void AddCurrentMissionNotifier(MissionNotifier missionNotifier){
		CurrentMissionNotifier notifier = GetCurrentMissionNotifier();
		Mission mission = missionNotifier.GetMission();
		notifier.SetMission(mission);
		MissionIco missionIco = missionNotifier.GetMissionIco();
		MissionFlyingIco missionFlyingIco = Instantiate(missionFlyingIcoPrefab) as MissionFlyingIco;
		missionFlyingIco.singleTransform.parent = singleTransform;
		missionFlyingIco.singleTransform.position = missionIco.singleTransform.position;
		missionFlyingIco.SetMissionIco(missionIco);
		
		notifier.SetMissionFlyingIco(missionFlyingIco);
		AddNotifier(notifier);
	}
	
	protected override Vector3 GetOutNotifierPlace(int position){
		Vector3 outPosition;
		outPosition = new Vector3 (GlobalOptions.Vsizex+5, GlobalOptions.Vsizey-120, -position*Z_INDEX_PER_POSITION);
		outPosition = GlobalOptions.NormalisePos (outPosition);
		outPosition = Cameras.GetGUICamera ().ScreenToWorldPoint (outPosition);
		
		outPosition.x += NOTIFIER_HEIGHT/2;
		outPosition.y -= NOTIFIER_HEIGHT/2+NOTIFIER_HEIGHT*position+0.025f*position;
		return outPosition;
	}
	
	protected override Vector3 GetInNotifierPlace(int position){
		Vector3 inPostion;
		
		inPostion = new Vector3 (GlobalOptions.Vsizex-15, GlobalOptions.Vsizey-120, -position*Z_INDEX_PER_POSITION);
		inPostion = GlobalOptions.NormalisePos (inPostion);
		inPostion = Cameras.GetGUICamera ().ScreenToWorldPoint (inPostion);
			
		inPostion.y -= NOTIFIER_HEIGHT/2+NOTIFIER_HEIGHT*position+0.025f*position;
		inPostion.x -= NOTIFIER_HEIGHT/2;
		
		return inPostion;
	}
	
	protected override int GetMaxNotifiersToShow ()
	{
		return MAX_NOTIFIERS_TO_SHOW;
	}
	
	public override void NotifierWantsToFlyOut(BaseNotifier notifier){
		int position = notifiersInProgress.IndexOf(notifier);
		notifier.FlyOut(GetOutNotifierPlace(position));
	}
	
	public override void NotifierFlyOutEnd(BaseNotifier notifier){
		notifiersInProgress.Remove(notifier);
		base.NotifierFlyOutEnd(notifier);
		ChangePlaces();
	}
	
}
