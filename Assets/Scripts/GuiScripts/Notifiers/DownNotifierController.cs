using UnityEngine;
using System.Collections;

public class DownNotifierController : BaseNotifierController {
	int MAX_NOTIFIERS_TO_SHOW = 1;
	float NOTIFIER_HEIGHT = 0.396f;
	float Z_INDEX_PER_POSITION = 0.15f;
	
	//mission notifier
	public MetersNotifier metersNotifierPrefab;
	
	private MetersNotifier GetMetersNotifier(){
		MetersNotifier notifier = Instantiate(metersNotifierPrefab) as MetersNotifier;	
		return notifier;
	}
	public void AddMetersNotifier(string text){
		MetersNotifier notifier = GetMetersNotifier();
		notifier.SetText(text);
		AddNotifier(notifier);
	}
	
	protected override Vector3 GetOutNotifierPlace(int position){
		Vector3 outPosition;
		outPosition = new Vector3 (GlobalOptions.Vsizex/2, -5, -position*Z_INDEX_PER_POSITION);
		outPosition = GlobalOptions.NormalisePos (outPosition);
		outPosition = Cameras.GetGUICamera ().ScreenToWorldPoint (outPosition);
		
		outPosition.y -= NOTIFIER_HEIGHT/2;
		return outPosition;
	}
	
	protected override Vector3 GetInNotifierPlace(int position){
		Vector3 inPostion;
		
		inPostion = new Vector3 (GlobalOptions.Vsizex/2, GlobalOptions.Vsizey-760, -position*Z_INDEX_PER_POSITION);
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
