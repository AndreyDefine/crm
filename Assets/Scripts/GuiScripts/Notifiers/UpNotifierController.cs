using UnityEngine;
using System.Collections;

public class UpNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 1;
	float NOTIFIER_HEIGHT = 0.396f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	public override void Restart ()
	{
		base.Restart ();
		SetCurrentMissions();
	}
	
	private void SetCurrentMissions(){
		ArrayList missions = GlobalOptions.GetMissionEmmitters().GetCurrentMissions();
		for(int i=0;i<missions.Count;i++){
			Mission mission = (Mission)missions[i];
			if(mission.GetState()==MissionStates.NOT_ACTIVE){
				AddMissionNotifier(mission);
			}
		}
	}
	
	//mission notifier
	public MissionNotifier missionNotifierPrefab;
	
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
