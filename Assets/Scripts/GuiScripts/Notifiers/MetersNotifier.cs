using UnityEngine;
using System.Collections;


public class MetersNotifier : BaseNotifier
{
	public tk2dTextMesh tk2dTextMeshTitle;
	
	private float flyOutTime;
	
	public void SetText (string text){
		tk2dTextMeshTitle.text = text;
		tk2dTextMeshTitle.Commit();
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
