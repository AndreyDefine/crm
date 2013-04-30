using UnityEngine;
using System.Collections;

public class SwypeDetector : SpriteTouch {	
	private float Epsilonxx,Epsilonxy,Epsilonyx,Epsilonyy;
	
	protected GameObject Player;
	protected Player playerScript; 
	
	private bool alreadySwyped;
    
    private void Start() {
		
		Player=GlobalOptions.GetPlayer();
		if(Player)
			playerScript = Player.GetComponent<Player>();
		
		touchPriority=3;
		swallowTouches=false;
        init();
		Epsilonxx=40*GlobalOptions.scaleFactorx;
		//Epsilonxy=6*GlobalOptions.scaleFactory;
		
		//Epsilonyx=6*GlobalOptions.scaleFactorx;
		Epsilonyy=40*GlobalOptions.scaleFactory;
		
		if(GlobalOptions.scaleFactorx<0.5)
		{
			Epsilonxx=40*GlobalOptions.scaleFactorx*2;
			Epsilonyy=40*GlobalOptions.scaleFactory*2;
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
		if(playerScript.typeOfControl==0)
		{
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
		if(GlobalOptions.playerStates!=PlayerStates.DOWN&&GlobalOptions.playerStates!=PlayerStates.FLY&&GlobalOptions.playerStates!=PlayerStates.JUMP&&GlobalOptions.playerStates!=PlayerStates.DIE)
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
	        if(Input.GetAxis ("Vertical")>0)
				SwypeUp(); 
			if(Input.GetKeyDown("space"))
				SwypeUp();
			if(Input.GetAxis ("Vertical")<0)
				SwypeDown(); 
			
			if(Input.GetAxis ("Horizontal")>0)
				SwypeRight(); 
			if(Input.GetAxis ("Horizontal")<0)
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
