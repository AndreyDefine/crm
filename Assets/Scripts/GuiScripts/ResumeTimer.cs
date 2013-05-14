using UnityEngine;
using System.Collections;

public class ResumeTimer : Abstract {
	
	public CrmFont crmFont;
	private int startTime = 3;
	private int curTime;
	
	void Start(){
		StartTimer();
	}
	
	public void StartTimer(){
		curTime = startTime;
		crmFont.text = curTime.ToString();
		AnimationFactory.Bounce2(this,1f,1f,1.07f,"Bounce","BounceStopped");
	}
	
	public void BounceStopped(){
		curTime--;
		if(curTime>0){
			crmFont.text = curTime.ToString();
			animation.Rewind();
			animation.Play("Bounce");
		}else{
			gameObject.SetActiveRecursively(false);
			Destroy(gameObject);
		}
	}


}
