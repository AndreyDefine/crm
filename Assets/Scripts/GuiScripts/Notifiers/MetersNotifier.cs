using UnityEngine;
using System.Collections;


public class MetersNotifier : BaseNotifier
{
	public CrmFont crmFont;
	
	private float flyOutTime;
	
	public void SetText (string text){
		crmFont.text = text+" meters";
	}
	
	void Update(){
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
