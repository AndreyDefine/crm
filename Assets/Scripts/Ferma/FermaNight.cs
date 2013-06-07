using UnityEngine;
using System.Collections;

public class FermaNight : SpriteTouch {
	
	bool flagMapWasMoving = false;
	private FermaLocationPlace fermaLocationPlace;
	
	private float nightAlpha = 0.85f;
	private float curAlpha;
	private float turnOffTime = 2f;
	private bool turningOff = false;
	float epsilon;
	
	
	public void SetFermaLocationPlace(FermaLocationPlace fermaLocationPlace){
		this.fermaLocationPlace = fermaLocationPlace;
	}
	
	public void SetActive(bool a){
		getTouches = a;
		gameObject.SetActive(a);
	}
	
	public void TurnOffNight(){
		turningOff = true;
		getTouches = false;
		curAlpha = nightAlpha;
	}
	
	void Update(){
		if(turningOff){
			curAlpha -= nightAlpha*Time.deltaTime/turnOffTime;
			if(curAlpha<=0f){
				turningOff = false;
				SetActive(false);
				fermaLocationPlace.NightOff();
			}else{
				SetColor(curAlpha);
			}
		}
	}
	
	protected override void Start(){
		base.Start();
		if (Screen.dpi != 0f) {
			epsilon = Screen.dpi*0.25f;
        }
		else{
			epsilon=30*GlobalOptions.scaleFactorx;
		}
		SetColor(nightAlpha);
	}
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
		}

		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position, int fingerId)
	{
		if(flagMapWasMoving){
			return;
		}
		if(ZoomMap.instance.MapIsMovingTwoFingers()){
			flagMapWasMoving = true;
			return;
		}
		if(Mathf.Abs(position.x-firstTouchLocation.x)>epsilon||Mathf.Abs(position.y-firstTouchLocation.y)>epsilon){
			flagMapWasMoving = true;
			return;
		}		
		
		base.TouchMoved (position, fingerId);
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
		}
		else
		{
		}
	}
	
	public override void TouchEnded (Vector2 position, int fingerId)
	{
		base.TouchEnded (position, fingerId);
		if(flagMapWasMoving==true){
			flagMapWasMoving = false;
			return;
		}
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			MakeOnTouch();
		}
	}
	
	virtual protected void MakeOnTouch(){
		fermaLocationPlace.ShowBuyDialog();
	}
	
	void SetColor(float alpha){
		GetComponent<tk2dSprite>().color = new Color(0f,0f,0f,alpha);
	}
}
