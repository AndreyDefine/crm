using UnityEngine;
using System.Collections;


public class TutorialFinihsedNotifier : BaseNotifier
{
	private float flyOutTime;
		
	protected virtual void Update(){
		if(state==NotifierStates.SHOWN&&Time.time-flyOutTime>SHOW_TIME){
			FlyOut();	
		}
	}
	
	public override void FlyInStopped(){
		FlyInEnd();
		flyOutTime = Time.time;
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
}