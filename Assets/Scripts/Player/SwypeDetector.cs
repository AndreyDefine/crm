using UnityEngine;
using System.Collections;

public class SwypeDetector : SpriteTouch {	
	private float Epsilonxx,Epsilonxy,Epsilonyx,Epsilonyy;
	
	protected Player playerScript; 
	
	private bool alreadySwyped;
    
    protected override void Start () {
		playerScript = GlobalOptions.GetPlayerScript();
		
		touchPriority=3;
		swallowTouches=false;
        init();
		
		//1 дюйм = 2.54 см
		//хотим чтобы свайп был от 6,35 миллиметра
		if (Screen.dpi != 0f) {
			Epsilonxx = Epsilonyy = Screen.dpi*0.25f;
        }
		else{
			Epsilonxx=30*GlobalOptions.scaleFactorx;
			Epsilonyy=30*GlobalOptions.scaleFactory;
			
			if(GlobalOptions.scaleFactorx<0.5)
			{
				Epsilonxx=30*GlobalOptions.scaleFactorx*2;
				Epsilonyy=30*GlobalOptions.scaleFactory*2;
			}
		}
    }
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		if(GlobalOptions.gameState!=GameStates.GAME)
		{
			return false;
		}
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			//do nothing
		}
		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position,int fingerId) {
		Vector2 currentTouchLocation;
		currentTouchLocation=position;
		
		if(alreadySwyped){
			return;
		}
		
		//только если управление 1
		//right
		if(currentTouchLocation.x>(firstTouchLocation.x+Epsilonxx))
		{
			alreadySwyped=true;
			firstTouchLocation=currentTouchLocation;
			SwypeRight(); 
		}
		//left
		if(currentTouchLocation.x<(firstTouchLocation.x-Epsilonxx))
		{
			alreadySwyped=true;
			firstTouchLocation=currentTouchLocation;
			SwypeLeft(); 
		}
		
		//up
		if(currentTouchLocation.y>(firstTouchLocation.y+Epsilonyy))
		{
			alreadySwyped=true;
			firstTouchLocation=currentTouchLocation;
			SwypeUp(); 
		}
		
		if(currentTouchLocation.y<(firstTouchLocation.y-Epsilonyy))
		{
			alreadySwyped=true;
			firstTouchLocation=currentTouchLocation;
			SwypeDown(); 
		}
	}
	
	public override void TouchEnded(Vector2 position,int fingerId) {
		base.TouchEnded(position,fingerId);
		alreadySwyped=false;
	}
	
	public override  void TouchCanceled(Vector2 position,int fingerId) {
		//usually the same as end
		base.TouchCanceled(position,fingerId);
	}
	
	private void SwypeUp(){
		//Debug.Log ("up");
		if(GlobalOptions.playerStates!=PlayerStates.FLY&&GlobalOptions.playerStates!=PlayerStates.JUMP&&GlobalOptions.playerStates!=PlayerStates.DIE)
		{
			GlobalOptions.playerStates=PlayerStates.JUMP;
		}
	}
	
	private void SwypeDown(){
		if(GlobalOptions.playerStates!=PlayerStates.DOWN&&GlobalOptions.playerStates!=PlayerStates.FLY&&GlobalOptions.playerStates!=PlayerStates.DIE)
		{
			GlobalOptions.playerStates=PlayerStates.DOWN;
		}
	}
	
	//right
	private void SwypeRight(){
		playerScript.ChangePath(true);
	}
	
	//left
	private void SwypeLeft(){
		playerScript.ChangePath(false);
	}
	
	private void MakeDetectionSwype()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		//if(GlobalOptions.playerStates==PlayerStates.WALK)
		{
	        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
				SwypeUp(); 
			if(Input.GetKeyDown(KeyCode.Space))
				SwypeUp();
			if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
				SwypeDown(); 
			
			if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
				SwypeRight(); 
			if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
				SwypeLeft(); 
		}
#endif
	}

	
    private void Update() { 
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			MakeDetectionSwype();
		}
	}
}
