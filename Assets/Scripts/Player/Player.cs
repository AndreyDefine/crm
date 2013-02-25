using UnityEngine;
using System.Collections;

public class Player : SpriteTouch,AccelerometerTargetedDelegate {
	public GameObject Character;
	public GameObject WhereToLook;
	public float maxX,minX;
	public float startVelocity;
	public float maxVelocity;
	public float acceleration;
	public float mnoshOfForce;
	public float meshPath;
	public int pathNumberLeftRight;
	public float whereToLookParalax;
	public int typeOfControl;
	public GameObject MainCamera;
	
	public float jumpForce;
	public float jumpLong;
	
	protected int accelPriority;
	protected bool swallowAcceles;

	private float force;
	
	private BearAnimation3D bearAnimation; 
	private AccelerometerDispatcher sharedAccelerometerDispatcher;
	private float PlayerTurnRotation,curRotation;
	private WorldFactory worldFactoryScript;
	
	private float localTime;
	
	private Vector3 PlayerFirstPos;
	private Vector3 CharacterFirstPos;
	private Vector3 raznFromWhereToLookAndCharacter;
	
	private Vector3 firstWhereToLookLocalPos;
	
	private int PathNumber;
	
	private bool PathChanging;
	private bool Jumping;
	
	private float VelocityVodka;
	private float VelocityMushroom;
	
	private ParticleEmitter HeadStarsParticleEmitter;
	
	private Vector3 centerXandYandAngle;
	
	private float xSmexcontrol1;
	
	private float oldMetersz;
	
	float FloatPathNumber;
	
	private GuiLayerInitializer guiLayer;
	// Use this for initialization
	void Start () {
		PlayerFirstPos=singleTransform.position;
		CharacterFirstPos=Character.transform.localPosition;
		firstWhereToLookLocalPos=WhereToLook.transform.localPosition;
		raznFromWhereToLookAndCharacter=firstWhereToLookLocalPos-CharacterFirstPos;
		
		
		HeadStarsParticleEmitter=GameObject.Find("/ScreenGame/Player/BearToControl/HeadBoomParticle").GetComponent<ParticleEmitter>();
		
		GlobalOptions.playerVelocity=startVelocity;
		bearAnimation=Character.GetComponent("BearAnimation3D") as BearAnimation3D;
		bearAnimation.SetWalkSpeed(GetRealVelocityWithNoDeltaTime()/startVelocity);
		rotatePersone();
		force=0;
		localTime=0;
		touchPriority=3;
		swallowTouches=false;
		PathNumber=0;
		PathChanging=false;
		FloatPathNumber=0;
		Jumping=false;
		VelocityVodka=1;
		VelocityMushroom=1;
		xSmexcontrol1=0;
		oldMetersz=0;
		
		accelPriority=1;
		swallowAcceles=false;
		
        init();
		#if UNITY_IPHONE || UNITY_ANDROID
        if(GlobalOptions.UsingAcceleration)
			initaccel ();
		#endif
		guiLayer=GlobalOptions.GetGuiLayer();
		GlobalOptions.gameState=GameStates.GAME;
		GlobalOptions.playerStates=PlayerStates.WALK;
		
		//Get world factory script
		GameObject worldFactory;
		//find with no parents
		worldFactory=GlobalOptions.GetWorldFactory();
		
		worldFactoryScript=worldFactory.GetComponent<WorldFactory>();
	}
	
	public void rotatePersone()
	{
		GlobalOptions.rotateTransformForWhere(singleTransform,GlobalOptions.whereToGo);
		GlobalOptions.rotateTransformForWhere(MainCamera.transform,GlobalOptions.whereToGo);
	}
	
	public void Restart()
	{
		Jumping=false;
		PathChanging=false;
		FloatPathNumber=0;
		force=0;
		PathNumber=0;
		localTime=0;
		xSmexcontrol1=0;
		oldMetersz=0;
		UnMakeVodka();
		UnMakeMushrooms();
		UnMakeHeadStars();
		GlobalOptions.playerVelocity=startVelocity;
		GlobalOptions.gameState=GameStates.GAME;
		GlobalOptions.playerStates=PlayerStates.WALK;
		BearRespawn();
		guiLayer.Restart();
		bearAnimation.Restart();
		worldFactoryScript.ReStart();
	}
	
	public void PauseGame()
	{
		Debug.Log ("idle");
		GlobalOptions.gameState=GameStates.PAUSE_MENU;
		//сделать анимацию IDLE
		GlobalOptions.playerStates=PlayerStates.IDLE;
	}
	
	public void ResumeGame()
	{
		Debug.Log ("Game Resumed");
		GlobalOptions.gameState=GameStates.GAME;
		//сделать анимацию Бега
		GlobalOptions.playerStates=PlayerStates.WALK;
	}
	
	public void MakeVodka()
	{
		VelocityVodka=1.5f;
		(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=true;
	}
	
	public void UnMakeVodka()
	{
		VelocityVodka=1;
		(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=false;
	}
	
	public void MakeMushrooms()
	{
		VelocityMushroom=0.7f;
		(MainCamera.GetComponent("ColorCorrectionEffect") as ColorCorrectionEffect).enabled=true;
	}
	
	public void UnMakeMushrooms()
	{
		VelocityMushroom=1;
		(MainCamera.GetComponent("ColorCorrectionEffect") as ColorCorrectionEffect).enabled=false;
	}
	
	public void MakeHeadStars()
	{
		HeadStarsParticleEmitter.emit=true;
	}
	
	public void UnMakeHeadStars()
	{
		HeadStarsParticleEmitter.emit=false;
	}
	
	public void PlaceCharacter(Vector3 inpos)
	{
		
		Character.transform.localPosition=inpos;
		WhereToLook.transform.localPosition=new Vector3(inpos.x*whereToLookParalax,inpos.y+raznFromWhereToLookAndCharacter.y,firstWhereToLookLocalPos.z);
	}
	
	public void ShowCap()
	{
		bearAnimation.ShowCap();
		guiLayer.RemoveStrobile();
	}

	// Update is called once per frame
	void Update () {		
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			//move straight
			MoveForNewPos();
			//Moving left-right
			MovingButtons();
			MoveLeftRight(force);
			MakeMovingUp();
			if(singleTransform.position.z-oldMetersz>200)
			{
				oldMetersz=singleTransform.position.z;
				int okrugl=(int)singleTransform.position.z/200;
				guiLayer.AddMeters(okrugl*200);
			}
			//MakeMusicSpeed();
		}
		
		bearAnimation.SetWalkSpeed(GetRealVelocityWithNoDeltaTime()/startVelocity);
		
		if(GlobalOptions.playerStates==PlayerStates.WALK){
			bearAnimation.Walk();
		}
		if(GlobalOptions.playerStates==PlayerStates.IDLE)
		{
			bearAnimation.Idle();
		}
		if(GlobalOptions.playerStates==PlayerStates.JUMP)
		{
			bearAnimation.Jump();
		}
		if(GlobalOptions.playerStates==PlayerStates.DIE)
			bearAnimation.Dead();
	}
	
	private void MakeMusicSpeed()
	{
		GlobalOptions.MainThemeMusicScript.SetMusicPitch(GetRealVelocityWithNoDeltaTime()/startVelocity*GlobalOptions.startMusicPitch);
	}
	
	protected void GetSharedAccelerateDispatcher(){
		sharedAccelerometerDispatcher = GlobalOptions.GetSharedAccelerateDispatcher();
	}
	
	protected virtual void initaccel(){
		GetSharedAccelerateDispatcher();
		sharedAccelerometerDispatcher.addTargetedDelegate(this,accelPriority,swallowAcceles);
	}
	
	public Vector3 GetCharacterPosition()
	{
		return Character.transform.position;
	}
	
	private void MoveLeftRight(float inmoveForce)
	{
		Transform curtransform=Character.transform;
		
		centerXandYandAngle=worldFactoryScript.GetXandYandAngleSmexForZ(curtransform.position.z);
		if(GlobalOptions.whereToGo.z<0)
		{ 
			centerXandYandAngle.x=-centerXandYandAngle.x;
		}
		
		
		float posx,posy,posz;
		posx=curtransform.localPosition.x;
		posy=CharacterFirstPos.y+centerXandYandAngle.y;
		posz=curtransform.localPosition.z;
		
		//смена дорожки
		if(GlobalOptions.playerStates==PlayerStates.LEFT&&!PathChanging){
			PathChanging=true;
			GlobalOptions.playerStates=PlayerStates.WALK;
			FloatPathNumber=PathNumber;
			PathNumber--;
			PathNumber=PathNumber<-pathNumberLeftRight?-pathNumberLeftRight:PathNumber;
		}
		
		if(GlobalOptions.playerStates==PlayerStates.RIGHT&&!PathChanging){
			PathChanging=true;
			GlobalOptions.playerStates=PlayerStates.WALK;
			FloatPathNumber=PathNumber;
			PathNumber++;
			PathNumber=PathNumber>pathNumberLeftRight?pathNumberLeftRight:PathNumber;
		}
		
		float centerx=centerXandYandAngle.x;
		
		//Ezxtra test out of bounds
		if(typeOfControl==0||typeOfControl==2)
		{
			posx=posx>centerx?centerx:posx;
			posx=posx<centerx?centerx:posx;
			
			//меняем дорожку
			if(PathChanging){	
				FloatPathNumber+=((PathNumber-FloatPathNumber))*GetRealVelocityWithDeltaTimeAndNoAcceleration(); 
				if(Mathf.Abs(FloatPathNumber-PathNumber)<0.1*GetRealVelocityWithDeltaTimeAndNoAcceleration())
				{
					FloatPathNumber=PathNumber;
					PathChanging=false;
				}
				posx+=FloatPathNumber*meshPath;
			}
			else{
				posx+=PathNumber*meshPath;
			}
		}	
		
		if(typeOfControl==1)
		{
			xSmexcontrol1+=inmoveForce*mnoshOfForce*Time.deltaTime;
			posx=centerx;
			xSmexcontrol1=xSmexcontrol1>=maxX?maxX:xSmexcontrol1;
			xSmexcontrol1=xSmexcontrol1<=minX?minX:xSmexcontrol1;
			posx+=xSmexcontrol1;
		}	
		
		if(typeOfControl==0||typeOfControl==1||typeOfControl==2)
		{
			float angle;
			
			// dx/dy
			angle=centerXandYandAngle.z/2f;
			
			angle*=180/3.141592653589f;
			//angle=0;
			//extra rotation
			Character.transform.rotation=Quaternion.Euler(18, angle+singleTransform.eulerAngles.y, Character.transform.rotation.z);
			WhereToLook.transform.rotation=Quaternion.Euler(0, angle+singleTransform.eulerAngles.y, WhereToLook.transform.rotation.z);
		}
		
		
		PlaceCharacter(new Vector3(posx,posy,posz));
	}
	
	private void BearRespawn(){
		GlobalOptions.playerStates=PlayerStates.WALK;
		Transform curtransform;
		curtransform=Character.transform;
		
		singleTransform.position=PlayerFirstPos;
		
		PlaceCharacter(new Vector3(0,0,0));
		curtransform.localPosition=new Vector3(0,0,0);
		curtransform.position=new Vector3(curtransform.position.x,CharacterFirstPos.y,curtransform.position.z);
	}
	
	
	private void MovingButtons(){
		if(Input.GetAxis ("Horizontal")!=0){
			force=Input.GetAxis ("Horizontal");
		}
	}
	
	public virtual bool Accelerate(Vector3 acceleration,int infingerId) {
		force = acceleration.x*2f;
		if(typeOfControl==2){
			float epsilonForse=0.6f;
			//right??
			if(force>epsilonForse&&PathNumber<=0)
			{
				
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				FloatPathNumber=PathNumber;
				PathNumber=1;
			}			
			if(force<-epsilonForse&&PathNumber>=0)
			{
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				FloatPathNumber=PathNumber;
				PathNumber=-1;
			}
			if(force>=-epsilonForse/3&&force<=epsilonForse/3&&PathNumber!=0){
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				FloatPathNumber=PathNumber;
				PathNumber=0;
			}
		}
		return true;
	}
	
	private void MoveForNewPos(){
		if(maxVelocity>GlobalOptions.playerVelocity&&GlobalOptions.gameType==GameType.Runner){
			GlobalOptions.playerVelocity+=acceleration*Time.deltaTime;
		}
		Vector3 smex=new Vector3(0,0,GetRealVelocity());
		singleTransform.Translate(smex);
	}
	
	public float GetRealVelocity()
	{
		return GetRealVelocityWithNoDeltaTime()*Time.deltaTime;
	}
	
	public float GetRealVelocityWithNoDeltaTime()
	{
		return GlobalOptions.playerVelocity*VelocityVodka*VelocityMushroom;
	}
	
	public float GetRealVelocityWithDeltaTimeAndNoAcceleration()
	{
		return startVelocity*VelocityVodka*VelocityMushroom*Time.deltaTime;;
	}
		
	
	protected override void InitTouchZone() {
        touchZone = new Rect (0, 0, Screen.width, Screen.height);
    }
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			//do nothing
		}
		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position,int fingerId) {
		if(!GlobalOptions.UsingAcceleration&&GlobalOptions.gameState==GameStates.GAME)
		{
			float maxabs=1.5f;
			Vector2 currentTouchLocation;
			currentTouchLocation=position;
			force=(currentTouchLocation.x-firstTouchLocation.x)*GlobalOptions.scaleFactorx*0.01f;
			force=force<-maxabs?-maxabs:force;
			force=force>maxabs?maxabs:force;
		}
	}
	
	public override void TouchEnded(Vector2 position,int fingerId) {
		if(!GlobalOptions.UsingAcceleration)
		{
			force=0;
		}
		base.TouchEnded(position,fingerId);
	}
	

	public void GameOver(){
		guiLayer.ShowGameOver();
	}
	
	private void Jump(float inmoveForce)
	{
		Transform curtransform;
		float posx,posy,posz;
		curtransform=Character.transform;
		posx=curtransform.localPosition.x;
		posy=CharacterFirstPos.y;
		posz=curtransform.localPosition.z;
		posy+=inmoveForce+centerXandYandAngle.y;	
		
		PlaceCharacter(new Vector3(posx,posy,posz));
	}
	
	private void MakeMovingUp(){
		if(GlobalOptions.playerStates==PlayerStates.JUMP||Jumping){
			Jumping=true;
			float moveForce;
	     	localTime+=jumpLong*GetRealVelocityWithDeltaTimeAndNoAcceleration()*0.5f;
	        moveForce=Mathf.Sin(localTime)*jumpForce;
	        if(localTime>Mathf.PI)
	        {
				Jumping=false;
	            localTime=0;
	            GlobalOptions.playerStates=PlayerStates.WALK;
				Jump(0);
	        }
			else
			{
	    		Jump(moveForce);
			}
		}
	}
	
}
