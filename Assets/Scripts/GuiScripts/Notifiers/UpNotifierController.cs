using UnityEngine;
using System.Collections;

public class UpNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 2;
	float NOTIFIER_HEIGHT = 0.52f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	//mission notifier
	public MissionNotifier missionNotifierPrefab;
	
	public MissionFinishedNotifier missionFinishedNotifierPrefab;
	
	private MissionNotifier GetMissionNotifier(){
		MissionNotifier notifier = Instantiate(missionNotifierPrefab) as MissionNotifier;	
		return notifier;
	}
	
	private MissionFinishedNotifier GetMissionFinishedNotifier(){
		MissionFinishedNotifier notifier = Instantiate(missionFinishedNotifierPrefab) as MissionFinishedNotifier;	
		return notifier;
	}
	
	public void AddMissionNotifier(Mission mission){
		MissionNotifier notifier = GetMissionNotifier();
		notifier.SetMission(mission);
		AddNotifier(notifier);
	}
	
	public void AddMissionFinishedNotifier(Mission mission){
		MissionFinishedNotifier notifier = GetMissionFinishedNotifier();
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
