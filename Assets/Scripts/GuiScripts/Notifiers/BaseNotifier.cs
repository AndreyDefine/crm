using UnityEngine;
using System.Collections;

public enum NotifierStates{HIDE,FLYING_IN,SHOWN,FLYING_OUT};

public abstract class BaseNotifier : Abstract
{
	protected float SHOW_TIME=2.2f;
	protected float FLY_ANIMATION_TIME = 0.8f;
	protected NotifierStates state = NotifierStates.HIDE;
	private BaseNotifierController notifierController;
	public bool oneDfly = false;
	private int animationCounter = 0;
	
	bool needOtherPosition = false;
	Vector3 targetPosition;
	
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
		if(needOtherPosition){
			AnimationFactory.FlyOutXYZ(this, targetPosition ,FLY_ANIMATION_TIME,"flyOut"+(++animationCounter).ToString());		
			needOtherPosition = false;
		}
	}
	
	protected void FlyOutEnd(){
		state = NotifierStates.HIDE;
		notifierController.NotifierFlyOutEnd(this);
	}
	
	public virtual void FlyIn(Vector3 inPostion){
		FlyInBegin();
		AnimationFactory.FlyInXYZ(this, inPostion ,FLY_ANIMATION_TIME,"flyIn"+(++animationCounter).ToString(), "FlyInStopped");
	}
	
	public virtual void FlyOut(Vector3 outPosition){
		FlyOutBegin();
		AnimationFactory.FlyOutXYZ(this, outPosition ,FLY_ANIMATION_TIME,"flyOut"+(++animationCounter).ToString(),"FlyOutStopped");	
	}
	
	public virtual void FlyPlace(Vector3 position){
		if(state==NotifierStates.FLYING_OUT){
			return;
		}
		if(state==NotifierStates.FLYING_IN){
			if(oneDfly){
				AnimationFactory.FlyInXYZ(this, position ,FLY_ANIMATION_TIME,"flyIn"+(++animationCounter).ToString(), "FlyInStopped");
			}else{
				targetPosition = position;
				needOtherPosition = true;
			}
		}else{
			AnimationFactory.FlyOutXYZ(this, position ,FLY_ANIMATION_TIME,"flyPlace"+(++animationCounter).ToString());	
			needOtherPosition = false;
		}
	}
	
	public void FlyOut(){
		notifierController.NotifierWantsToFlyOut(this);
	}
	
	/// <summary>
	/// Удалить нотифайер и все связанные с ним данные
	/// </summary>
	public virtual void DestroyNotifier(){
		Destroy(gameObject);
	}
	
	public abstract void FlyInStopped();
	
	public abstract void FlyOutStopped();
}
