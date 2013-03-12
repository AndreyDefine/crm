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
    }
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
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
		GlobalOptions.playerStates=PlayerStates.JUMP;
	}
	
	private void SwypeDown(){
		GlobalOptions.playerStates=PlayerStates.DOWN;
	}
	
	//right
	private void SwypeRight(){
		GlobalOptions.playerStates=PlayerStates.RIGHT;
	}
	
	//left
	private void SwypeLeft(){
		GlobalOptions.playerStates=PlayerStates.LEFT;
	}
	
	private void MakeDetectionSwype()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		//if(GlobalOptions.playerStates==PlayerStates.WALK)
		{
	        if(Input.GetKeyDown("space"))
				SwypeUp(); 
			
			if(Input.GetKeyDown(KeyCode.X))
				SwypeRight(); 
			if(Input.GetKeyDown(KeyCode.Z))
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
