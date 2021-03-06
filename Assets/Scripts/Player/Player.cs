using UnityEngine;
using System.Collections;

public class Player : SpriteTouch {
	public GameObject Character;
	public GameObject WhereToLook;
	public GameObject Particles;
	
	public float startVelocity;
	public float maxVelocity;
	public float acceleration;
	public float mnoshOfForce;
	public float meshPath;
	public int pathNumberLeftRight;
	public float whereToLookParalax;
	public GameObject MainCamera;
	
	public GameObject PosilkaRight;
	
	public float roadChangeForce;
	
	protected int accelPriority;
	protected bool swallowAcceles;
	
	private bool flagYahoo=false;
	private float timerYahoo;
	private float timerAltWalk;
	
	private float curVelocityWithNoDeltaTime;
	
	private float force;
	
	private BearAnimation3D bearAnimation; 
	private AccelerometerDispatcher sharedAccelerometerDispatcher;
	private float PlayerTurnRotation,curRotation;
	private WorldFactory worldFactoryScript;
	
	private Vector3 PlayerFirstPos;
	private Vector3 CameraFirstPos;
	private Quaternion CameraFirstRotation;
	private Vector3 CharacterFirstPos;
	private Vector3 raznFromWhereToLookAndCharacter;
	private bool flyingPathChange=false;
	
	private GameObject posilka=null;
	
	private Vector3 firstWhereToLookLocalPos;
	
	private int PathNumber,prevPathNumber;
	
	private bool PathChanging;
	
	private float VelocityVodka;
	private float VelocityHeadStart;
	private float VelocityPosilka;
	
	private ParticleEmitter HeadStarsParticleEmitter;
	
	private Vector3 centerXandYandAngle;
		
	private float oldMetersz,allMeters,oneMeterz;
	
	private CharacterMarioC characterMarioC;
	
	private bool magnitFlag=false;
	
	private bool flagPosilka=false;
	
	float posilkaTimer;
	
	private Transform particlesTransform;
	
	private Transform whereToLookTransform,characterTransform,walkingBearTransform,mainCameraTransform,blobTransform;
	
	private GameObject walkingBear;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	public void MoveParticlesDown()
	{
		particlesTransform.localPosition=new Vector3(0,-0.9f,0);
	}
	
	public void MoveParticlesUp()
	{
		particlesTransform.localPosition=new Vector3(0,0,0);
	}
	
	public bool GetMagnitFlag()
	{
		return magnitFlag;
	}
	
	public Transform GetWalkingBear()
	{
		return walkingBearTransform;
	}
	
	private GuiLayerInitializer guiLayer;
	// Use this for initialization
	
	public BoostFX boostFx;
	public ParticleSystem moneyCollected;
	public ParticleSystem vodkaBoom;
	public ParticleSystem resBoom;
	
	public void MakeVodkaBoom()
	{
		vodkaBoom.Play();
	}
	
	protected override void Start () {
		//transforms
		whereToLookTransform=WhereToLook.transform;
		characterTransform=Character.transform;	
		mainCameraTransform=MainCamera.transform;
		particlesTransform=Particles.transform;
		
		walkingBear=characterTransform.FindChild("WalkingBear").gameObject;
		walkingBearTransform=walkingBear.transform;
		////////////
		
		characterMarioC=Character.GetComponent<CharacterMarioC>();
		PlayerFirstPos=singleTransform.position;
		CameraFirstPos=mainCameraTransform.position;
		CameraFirstRotation=mainCameraTransform.rotation;
		CharacterFirstPos=characterTransform.localPosition;
		firstWhereToLookLocalPos=whereToLookTransform.localPosition;
		raznFromWhereToLookAndCharacter=firstWhereToLookLocalPos-CharacterFirstPos;
		
		
		HeadStarsParticleEmitter=GameObject.Find("HeadBoomParticle").GetComponent<ParticleEmitter>();
		
		GlobalOptions.playerVelocity=startVelocity;
		bearAnimation=Character.GetComponent("BearAnimation3D") as BearAnimation3D;
		bearAnimation.SetWalkSpeed(GetRealVelocityWithNoDeltaTime()/startVelocity);
		force=0;
		touchPriority=3;
		swallowTouches=false;
		PathNumber=0;
		prevPathNumber=0;
		PathChanging=false;
		VelocityVodka=1;
		VelocityHeadStart=1;
		VelocityPosilka=1;
		oldMetersz=0;
		oneMeterz=0;
		allMeters=0;
		flagPosilka=false;
		flagYahoo=false;
		timerAltWalk=Time.time;
		
		stopTime=0;
		startstopTime=0;
				
		accelPriority=1;
		swallowAcceles=false;
		
        init();

		guiLayer=GlobalOptions.GetGuiLayer();
		GlobalOptions.gameState=GameStates.GAME;
		GlobalOptions.playerStates=PlayerStates.WALK;
		GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.FORWARD;
	}
	
	public void Restart()
	{
		PathChanging=false;
		force=0;
		PathNumber=0;
		MoveCharacterControllerLeftRight(0);
		prevPathNumber=0;
		oldMetersz=0;
		oneMeterz=0;
		allMeters=0;
		flagPosilka=false;
		flagYahoo=false;
		timerAltWalk=Time.time;
		
		stopTime=0;
		startstopTime=0;
		
		VelocityPosilka=1;
		VelocityHeadStart=1;
		
		UnMakeHeadStars();
		UnMakePropeller();
		UnMakeMagnit();
		UnMakeVodka();
		GlobalOptions.playerVelocity=startVelocity;
		GlobalOptions.gameState=GameStates.GAME;
		
		GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.FORWARD;
		GlobalOptions.playerStates=PlayerStates.WALK;

		BearRespawn();
		guiLayer.Restart();
		bearAnimation.Restart();
		worldFactoryScript.ReStart();
	}
	
	public void PauseGame()
	{
		GlobalOptions.gameState=GameStates.PAUSE_MENU;
		bearAnimation.StopAnimation();
		if(posilka)
		{
			posilka.animation["FlyXYZRotateXYZ"].speed=0;
		}
		
		if(startstopTime==0)
		{
			startstopTime=Time.time;
		}
	}
	
	public void ResumeGame()
	{
		stopTime=Time.time-startstopTime;
		
		AddAllTimes();
		
		GlobalOptions.gameState=GameStates.GAME;
		bearAnimation.ResumeAnimation();
		if(posilka)
		{
			posilka.animation["FlyXYZRotateXYZ"].speed=1;
		}
	}
	
	public bool isVodka()
	{
		if(VelocityVodka>1||VelocityHeadStart>1)
		{
			return true;
		}
		return false;
	}
	
	public void MakePosilka(Vector3 moveTo)
	{
		
		GlobalOptions.GetMissionEmmitters().NotifyPostDropped(1);
		if(moveTo.x>0)
		{
			DropPosilkaDynamic(moveTo);
			bearAnimation.Posilka_Right();	
		}
		
		if(moveTo.x<0)
		{
			DropPosilkaDynamic(moveTo);
			bearAnimation.Posilka_Left();
		}
	}
	
	public void RemovePosilka()
	{
		posilka=null;
	}
	
	private void DropPosilkaDynamic(Vector3 moveTo)
	{
		flagPosilka=true;
		posilkaTimer=Time.time;
		if(VelocityHeadStart>1)
		{
			VelocityPosilka=0.6f;
		}
		else
		{
			VelocityPosilka=0.2f;
		}
		
		SetRealVelocityWithNoDeltaTime();
		posilka=Instantiate(PosilkaRight) as GameObject;
		
		Abstract posilkaAbstract = posilka.GetComponent<Abstract>();
		posilkaAbstract.singleTransform.position=characterTransform.position+new Vector3(0,2,2);
		float flyTime=0.3f/(GetRealVelocityWithNoDeltaTime()/startVelocity);
		if(moveTo.x>0)
		{
			AnimationFactory.FlyXYZRotateXYZ(posilkaAbstract,moveTo,new Vector3(0f,180f,0f), flyTime,"FlyXYZRotateXYZ","FlyXYZRotateXYZStop");
		}
		else
		{
			AnimationFactory.FlyXYZRotateXYZ(posilkaAbstract,moveTo,new Vector3(0f,-180f,0f), flyTime,"FlyXYZRotateXYZ","FlyXYZRotateXYZStop");
		}
	}
	
	public void MakeVodka()
	{
		VelocityVodka=1.1f;
		//(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=true;
	}
	
	public void UnMakeVodka()
	{
		VelocityVodka=1;
		//(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=false;
	}
	
	public void MakeHeadStart()
	{
		VelocityHeadStart=1.5f;
	}
	
	public void MakeRess()
	{
		VelocityHeadStart=1.2f;
		GlobalOptions.playerStates=PlayerStates.WALK;
		GlobalOptions.gameState=GameStates.GAME;
		
		resBoom.Play();
		
		characterMarioC.Respawn();
	}
	
	public void UnMakeHeadStart()
	{
		VelocityHeadStart=1;
	}
	
	public void MakeMagnit()
	{
		magnitFlag=true;
	}
	
	public void UnMakeMagnit()
	{
		magnitFlag=false;
	}
	
	public void MakePropeller()
	{
		GlobalOptions.playerStates=PlayerStates.FLY;
		MakeFlyingCharacterController(true);
	}
	
	public void UnMakePropeller()
	{
		MakeFlyingCharacterController(false);
	}
	
	public void UnMakeEmmidiately()
	{
		MakeFlyingUnCharacterControllerEmmidiately(false);
	}
	
	public void MakeHeadStars()
	{
		if(GlobalOptions.gameState!=GameStates.GAME_OVER)
		{
			HeadStarsParticleEmitter.emit=true;
		}
	}
	
	public void UnMakeHeadStars()
	{
		HeadStarsParticleEmitter.emit=false;
	}
	
	private void MakeYahoo()
	{
		if(Time.time-timerYahoo>4)
		{
			flagYahoo=false;
		}
		else
		{
			if(GlobalOptions.gameState==GameStates.GAME&&GlobalOptions.playerStates==PlayerStates.WALK)
			{
				bearAnimation.Yahoo();
				flagYahoo=false;
			}
		}
	}
	
	private void AddAllTimes()
	{
		if(startstopTime!=0)
		{
			posilkaTimer+=stopTime;
			stopTime=0;
			startstopTime=0;
		}
	}
	

	// Update is called once per frame
	void Update () {
		
		SetRealVelocityWithNoDeltaTime();
		if(!worldFactoryScript)
		{
			//Get world factory script
			GameObject worldFactory=null;
			worldFactory=GlobalOptions.GetWorldFactory();
			if(worldFactory)
			{
				worldFactoryScript=worldFactory.GetComponent<WorldFactory>();
			}
		}
		
		if(flagYahoo)
		{
			MakeYahoo();
		}
		
		if(!worldFactoryScript)
			return;
		
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			MoveLeftRight(force);
			MovingButtons();
			MakeMovingCharacterController();
			TestIsFallen();
			//MakeMusicSpeed();
		}
		
		bearAnimation.SetWalkSpeed(GetRealVelocityWithNoDeltaTime()/startVelocity);
		
		SwitchAnimation();
	}
	
	private void MakeAltWalk()
	{
		if(Time.time-timerAltWalk>10+Random.Range(0,5))
		//if(Time.time-timerAltWalk>2+Random.Range(1,2))
		{
			timerAltWalk=Time.time;
			if(GlobalOptions.gameState==GameStates.GAME&&GlobalOptions.playerStates==PlayerStates.WALK)
			{
				bearAnimation.WalkAlt();
			}
		}
	}
	
	private void SwitchAnimation()
	{
		//no altwalk
		//MakeAltWalk();
		if(flagPosilka&&GlobalOptions.gameState==GameStates.GAME)
		{
			if(Time.time-posilkaTimer>5/GetRealVelocityWithNoDeltaTime())
			{
				flagPosilka=false;
				VelocityPosilka=1;
			}
		}
		if(GlobalOptions.playerStates==PlayerStates.WALK||GlobalOptions.playerStates==PlayerStates.FLY){
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.FORWARD&&!flagPosilka)
			{
				bearAnimation.Walk();
			}
			else
			{
				if(flyingPathChange)
				{
					bearAnimation.Walk();
				}
					
			}
			
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.LEFT&&!flyingPathChange)
			{
				bearAnimation.Left();
				characterMarioC.PathChangeJump();
				
			}
			
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.RIGHT&&!flyingPathChange)
			{
				bearAnimation.Right();
				characterMarioC.PathChangeJump();
			}
		}
		if(GlobalOptions.playerStates==PlayerStates.IDLE)
		{
			bearAnimation.Idle();
		}
		if(GlobalOptions.playerStates==PlayerStates.JUMP)
		{
			bearAnimation.Jump();
			flyingPathChange=true;
		}
		if(GlobalOptions.playerStates==PlayerStates.DOWN&&!characterMarioC.isJumping())
		{
			bearAnimation.Down();
		}
		if(GlobalOptions.playerStates==PlayerStates.DIE)
			bearAnimation.Dead();
	}
	
	private void MakeMusicSpeed()
	{
		GlobalOptions.MainThemeMusicScript.SetMusicPitch(GetRealVelocityWithNoDeltaTime()/startVelocity*GlobalOptions.startMusicPitch);
	}
	
	public Vector3 GetCharacterPosition()
	{
		return characterTransform.position;
	}
	
	public void ChangePath(bool toRight)
	{
		//смена дорожки
		if(!toRight){
			flyingPathChange=false;
			PathChanging=true;
			GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.LEFT;
			prevPathNumber=PathNumber;
			PathNumber--;
			PathNumber=PathNumber<-pathNumberLeftRight?-pathNumberLeftRight:PathNumber;
			if(prevPathNumber!=PathNumber){
				GlobalOptions.GetMissionEmmitters().NotifyLeft(1);
			}
		}
		
		if(toRight){
			flyingPathChange=false;
			PathChanging=true;
			GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.RIGHT;
			prevPathNumber=PathNumber;
			PathNumber++;
			PathNumber=PathNumber>pathNumberLeftRight?pathNumberLeftRight:PathNumber;
			if(prevPathNumber!=PathNumber){
				GlobalOptions.GetMissionEmmitters().NotifyRight(1);
			}
		}
	}
	
	private void MoveLeftRight(float inmoveForce)
	{
		//forward
		if(maxVelocity>GlobalOptions.playerVelocity){
			GlobalOptions.playerVelocity+=acceleration*Time.deltaTime;
		}
		Vector3 smex=new Vector3(0,0,GetRealVelocity());
		
		//Ezxtra test out of bounds	
		float forcex=(-characterTransform.localPosition.x+PathNumber*meshPath)*roadChangeForce;
		//меняем дорожку
		if(PathChanging){	
			if(Mathf.Abs (forcex)<0.09*roadChangeForce)
			{
				MoveCharacterControllerLeftRight(0);
				PathChanging=false;
				GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.FORWARD;
			}
			else{
				MoveCharacterControllerLeftRight(forcex);
			}
		}
		else
		{
			MoveCharacterControllerLeftRight(forcex);
		}
		
		
		if(GlobalOptions.flagOnlyFizik)
		{
			MakeMovingCharacterControllerForward();
		}
		else
		{
			PlaceCharacter(new Vector3(centerXandYandAngle.x,PlayerFirstPos.y,centerXandYandAngle.z));
		}
		
		oneMeterz+=smex.z/2;
		
		if(oneMeterz>1){//TODO: check this
			oneMeterz-=1f;
			GlobalOptions.GetMissionEmmitters().NotifyMetersRunned(1);
		}
		//meters;
		oldMetersz+=smex.z;
		if(oldMetersz>200*2)
		{
			oldMetersz=0;
			allMeters+=200;
			guiLayer.AddMeters(allMeters);
		}
	}
	
	private void RotatePlayer(float inangle)
	{
		if(GlobalOptions.flagOnlyFizik)
		{
			//no rotation
			//MakeRotationCharacterController(inangle);
		}else
		{
			singleTransform.rotation=Quaternion.Euler(singleTransform.rotation.x, inangle, singleTransform.rotation.z);
		}
	}
	
	public float PlaceBearToControl()
	{
		float dumping=10;
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU||!characterMarioC)
		{
			return dumping;
		}
		Vector3 charpos=characterTransform.localPosition;
		float raznost=raznFromWhereToLookAndCharacter.y;
		float heightDamping=2f;
				
		if((characterMarioC.isJumping())&&GlobalOptions.gameState!=GameStates.GAME_OVER)
		{
			//Debug.Log ("characterMarioC");
			float currentHeight = Mathf.Lerp (whereToLookTransform.localPosition.y, raznost, heightDamping * Time.deltaTime);

			whereToLookTransform.localPosition=new Vector3(charpos.x*whereToLookParalax, currentHeight,whereToLookTransform.localPosition.z);
			
			dumping=0;
		}
		else
		{
			heightDamping=2f;
			if(characterMarioC.isFlying()||characterMarioC.ismovingToFlyGround())
			{
				heightDamping=10f;
			}			
			if(characterMarioC.isGliding()&&GlobalOptions.playerStates!=PlayerStates.DIE)
			{
				raznost-=1f;
			}
			if(GlobalOptions.playerStates==PlayerStates.DIE)
			{
				raznost-=2;
				heightDamping=2f;
			}
			float currentHeight = Mathf.Lerp (whereToLookTransform.localPosition.y, raznost, heightDamping * Time.deltaTime);
			whereToLookTransform.localPosition=new Vector3(charpos.x*whereToLookParalax,currentHeight,whereToLookTransform.localPosition.z);
		}
		
		return dumping;
	}
	
	public void PlaceCharacter(Vector3 inpos)
	{
		//Debug.Log ("PlaceCharacter(Vector3 inpos)");
		singleTransform.position=inpos;
	}
	
	private void BearRespawn(){
		bearAnimation.ResumeAnimation();
		GlobalOptions.whereToBuild=new Vector3(0,0,1);
		RotatePlayer(0);	
		singleTransform.position=PlayerFirstPos;		
		characterTransform.localPosition=new Vector3(0,0,20);
		walkingBearTransform.localPosition=new Vector3(0,0,0);
		//PlaceBearToControl();

		CharacterControllerRespawn();
		
		//MainCamera
		mainCameraTransform.position=CameraFirstPos;
		mainCameraTransform.rotation=CameraFirstRotation;
	}
	
	
	private void MovingButtons(){
		if(Input.GetAxis ("Horizontal")!=0){
			force=Input.GetAxis ("Horizontal");
		}
	}
	
	public float GetVelocityCurMnoshitel()
	{
		return GetRealVelocityWithNoDeltaTime()/startVelocity;
	}
	
	public float GetRealVelocity()
	{
		return GetRealVelocityWithNoDeltaTime()*Time.deltaTime;
	}
	
	public void SetRealVelocityWithNoDeltaTime()
	{
		curVelocityWithNoDeltaTime=GlobalOptions.playerVelocity*VelocityVodka*VelocityHeadStart*VelocityPosilka;
	}
	
	public float GetRealVelocityWithNoDeltaTime()
	{
		return curVelocityWithNoDeltaTime;
	}
	
	/*public float GetRealVelocityWithDeltaTimeAndNoAcceleration()
	{
		return startVelocity*VelocityVodka*VelocityHeadStart*VelocityPosilka*Time.deltaTime;;
	}*/
		
	
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
		UnMakeHeadStars();
		characterMarioC.Freeze();
	}
	
	private void MakeMovingCharacterController(){
		if(GlobalOptions.playerStates==PlayerStates.JUMP)
		{
			characterMarioC.Jump();
		}
		
		if(GlobalOptions.playerStates==PlayerStates.DOWN)
		{
			characterMarioC.Down();
		}
	}
	
	public void MakeFlyingCharacterController(bool inflag){
		characterMarioC.Fly(inflag);
	}
	
	public void MakeFlyingUnCharacterControllerEmmidiately(bool inflag)
	{
		characterMarioC.Fly(inflag);
		characterMarioC.MoveToGroundEmmidiately();
	}
	
	private void MoveCharacterControllerLeftRight(float inx)
	{
		characterMarioC.LeftRight(inx);
	}
	
	private void MakeMovingCharacterControllerForward(){
		float forward=GetRealVelocityWithNoDeltaTime();
		characterMarioC.SetMovement(forward);
		
		if(characterTransform.position.z>worldFactoryScript.GetCurTerrainEnd().z+20)
		{
			worldFactoryScript.TryAddTerrrain();
		}
	}
	
	private void MakeRotationCharacterController(float inangle)
	{
		//Debug.Log (inangle);
		characterTransform.rotation=Quaternion.Euler(0, inangle, 0);
		whereToLookTransform.rotation=Quaternion.Euler(0, inangle, 0);
	}
	
	private void CharacterControllerRespawn(){
		characterMarioC.Respawn();
	}
	
	private void TestIsFallen(){
		if(characterTransform.position.y+10<worldFactoryScript.GetCurTerrainCenter())
		{
			guiLayer.GameOver();
		}
	}
	
	public void Yahoo()
	{
		flagYahoo=true;
		timerYahoo=Time.time;
	}
	
	public void Stumble(Transform inTransform)
	{
		MoveCharacterControllerLeftRight(0);
		if(!isVodka())
		{
			PathNumber=prevPathNumber;
			PathChanging=true;
			guiLayer.AddToLife(-3,inTransform);
			guiLayer.AddHeadStars();
		}
	}
	
	public void StumbleTrigger()
	{
		bearAnimation.Stumble();
		guiLayer.AddHeadStars();
	}
	
	public void PlaceCharacterFirstly(Vector3 inpos)
	{
		characterTransform.position=inpos;
		Vector3 walkbearpos=walkingBearTransform.localPosition;
		Vector3 charpos=characterTransform.localPosition;
		whereToLookTransform.localPosition=new Vector3(charpos.x*whereToLookParalax,charpos.y+walkbearpos.y+raznFromWhereToLookAndCharacter.y,raznFromWhereToLookAndCharacter.z+charpos.z);
	}
}
