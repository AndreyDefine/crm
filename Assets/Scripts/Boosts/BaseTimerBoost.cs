using UnityEngine;
using System.Collections;

public abstract class BaseTimerBoost : Boost {
	private float leftTime = 0;
	private float startTime;
	
	public abstract float GetMaxTime();
	
	public override void SetActive ()
	{
		base.SetActive ();
		SetTimeFull();
	}
	
	void Update(){	
		if(GetState()!=BoostStates.ACTIVE){
			return;
		}
		Debug.Log ("ACTIVE");
		leftTime = GetMaxTime()-(Time.time-startTime);
		if(leftTime<=0){
			BoostFinished();
		}
		BoostProgressChanged();
	}
	
	public void AddTime(float time){
		leftTime+=time;
		if(leftTime>=GetMaxTime()){
			SetTimeFull();
		}else{
			startTime+=time;
		}
	}
	
	public void SetTimeFull(){
		leftTime = GetMaxTime();
		startTime = Time.time;
	}
	
	public override float GetProgress ()
	{
		return leftTime/GetMaxTime();
	}
}
