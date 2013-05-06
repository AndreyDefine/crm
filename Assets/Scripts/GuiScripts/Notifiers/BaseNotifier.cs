using UnityEngine;
using System.Collections;

public enum NotifierStates{HIDE,FLYING_IN,SHOWN,FLYING_OUT};

public abstract class BaseNotifier : Abstract
{
	protected float SHOW_TIME=2.2f;
	protected float FLY_ANIMATION_TIME = 0.8f;
	protected NotifierStates state = NotifierStates.HIDE;
	private BaseNotifierController notifierController;
	
	public NotifierStates GetState(){
		return state;
	}
	
	public void SetNotifierController(BaseNotifierController notifierController){
		this.notifierController = notifierController;	
	}
	
	protected void FlyInBegin(){
		state = NotifierStates.FLYING_IN;	
	}
	
	protected void FlyOutBegin(){
		state = NotifierStates.FLYING_OUT;
	}
	
	protected void FlyInEnd(){
		state = NotifierStates.SHOWN;	
	}
	
	protected void FlyOutEnd(){
		state = NotifierStates.HIDE;
		notifierController.NotifierFlyOutEnd(this);
	}
	
	public virtual void FlyIn(Vector3 inPostion){
		FlyInBegin();
		AnimationFactory.FlyInXYZ(this, inPostion ,FLY_ANIMATION_TIME,"flyIn", "FlyInStopped");
	}
	
	public virtual void FlyOut(Vector3 outPosition){
		FlyOutBegin();
		AnimationFactory.FlyOutXYZ(this, outPosition ,FLY_ANIMATION_TIME,"flyOut","FlyOutStopped");	
	}
	
	public virtual void FlyPlace(Vector3 position){
		if(state==NotifierStates.FLYING_OUT){
			return;
		}
		if(state==NotifierStates.FLYING_IN){
			AnimationFactory.FlyOutXYZ(this, position ,FLY_ANIMATION_TIME,"flyIn", "FlyInStopped");	
		}else{
			AnimationFactory.FlyOutXYZ(this, position ,FLY_ANIMATION_TIME,"flyPlace");		
		}
	}
	
	public void FlyOut(){
		notifierController.NotifierWantsToFlyOut(this);
	}
	
	public abstract void FlyInStopped();
	
	public abstract void FlyOutStopped();
}
