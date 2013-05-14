using UnityEngine;
using System.Collections;

public class BoostNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 7;
	float NOTIFIER_HEIGHT = 0.386f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	//mission notifier
	public BoostNotifier boostNotifierPrefab;
	
	private BoostNotifier GetBoostNotifier(){
		BoostNotifier notifier = Instantiate(boostNotifierPrefab) as BoostNotifier;	
		return notifier;
	}
			
	public void AddBoostNotifier(Boost boostPrefab){
		for(int i=0;i<notifiersInProgress.Count;i++){
			Boost notifierBoost = ((BoostNotifier)notifiersInProgress[i]).GetBoost();
			if(notifierBoost.GetType()==boostPrefab.GetType()){
				notifierBoost.SetActive();
				return;
			}
		}
		BoostNotifier notifier = GetBoostNotifier();
		Boost boost = Instantiate(boostPrefab) as Boost;
		boost.SetActive();
		notifier.SetBoost(boost);
		AddNotifier(notifier);
	}
	
	protected override Vector3 GetOutNotifierPlace(int position){
		Vector3 outPosition;
		outPosition = new Vector3 (-5, 15, -position*Z_INDEX_PER_POSITION);
		outPosition = GlobalOptions.NormalisePos (outPosition);
		outPosition = Cameras.GetGUICamera ().ScreenToWorldPoint (outPosition);
		
		outPosition.x -= NOTIFIER_HEIGHT/2;
		outPosition.y += NOTIFIER_HEIGHT/2+NOTIFIER_HEIGHT*position;
		return outPosition;
	}
	
	protected override Vector3 GetInNotifierPlace(int position){
		Vector3 inPostion;
		
		inPostion = new Vector3 (15, 15, -position*Z_INDEX_PER_POSITION);
		inPostion = GlobalOptions.NormalisePos (inPostion);
		inPostion = Cameras.GetGUICamera ().ScreenToWorldPoint (inPostion);
			
		inPostion.y += NOTIFIER_HEIGHT/2+NOTIFIER_HEIGHT*position;
		inPostion.x += NOTIFIER_HEIGHT/2;
		
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
