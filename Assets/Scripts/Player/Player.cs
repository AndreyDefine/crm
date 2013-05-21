using UnityEngine;
using System.Collections;

public class Player : SpriteTouch,AccelerometerTargetedDelegate {
	public GameObject Character;
	public GameObject WhereToLook;
	public float startVelocity;
	public float maxVelocity;
	public float acceleration;
	public float mnoshOfForce;
	public float meshPath;
	public int pathNumberLeftRight;
	public float whereToLookParalax;
	public int typeOfControl;
	public GameObject MainCamera;
	
	public float roadChangeForce;
	
	protected int accelPriority;
	protected bool swallowAcceles;

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
	
	private Vector3 firstWhereToLookLocalPos;
	
	private int PathNumber,prevPathNumber;
	
	private bool PathChanging;
	
	private float VelocityVodka;
	private float VelocityMushroom;
	
	private ParticleEmitter HeadStarsParticleEmitter;
	
	private Vector3 centerXandYandAngle;
	
	private float xSmexcontrol1;
	
	private float oldMetersz,allMeters,oneMeterz;
	
	private CharacterMarioC characterMarioC;
	
	private float posx;
	
	private bool magnitFlag=false;
	
	private GameObject walkingBear;
	
	public bool GetMagnitFlag()
	{
		return magnitFlag;
	}
	
	public Transform GetWalkingBear()
	{
		return walkingBear.transform;
	}
	
	private GuiLayerInitializer guiLayer;
	// Use this for initialization
	void Start () {
		characterMarioC=Character.GetComponent<CharacterMarioC>();
		PlayerFirstPos=singleTransform.position;
		CameraFirstPos=MainCamera.transform.position;
		CameraFirstRotation=MainCamera.transform.rotation;
		CharacterFirstPos=Character.transform.localPosition;
		firstWhereToLookLocalPos=WhereToLook.transform.localPosition;
		raznFromWhereToLookAndCharacter=firstWhereToLookLocalPos-CharacterFirstPos;
		
		walkingBear=Character.transform.FindChild("WalkingBear").gameObject;
		
		
		HeadStarsParticleEmitter=GameObject.Find("/ScreenGame/Player/BearToControl/HeadBoomParticle").GetComponent<ParticleEmitter>();
		
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
		VelocityMushroom=1;
		xSmexcontrol1=0;
		oldMetersz=0;
		oneMeterz=0;
		allMeters=0;
		
		posx=0;
		
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
		GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.FORWARD;
	}
	
	public void Restart()
	{
		PathChanging=false;
		force=0;
		PathNumber=0;
		MoveCharacterControllerLeftRight(0);
		prevPathNumber=0;
		xSmexcontrol1=0;
		oldMetersz=0;
		oneMeterz=0;
		allMeters=0;
		posx=0;
		UnMakePropeller();
		UnMakeMagnit();
		UnMakeVodka();
		UnMakeMushrooms();
		UnMakeHeadStars();
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
	}
	
	public void ResumeGame()
	{
		GlobalOptions.gameState=GameStates.GAME;
		bearAnimation.ResumeAnimation();
	}
	
	public bool isVodka()
	{
		if(VelocityVodka>1)
		{
			return true;
		}
		
		return false;
	}
	
	public void MakeVodka()
	{
		VelocityVodka=1.2f;
		(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=true;
	}
	
	public void UnMakeVodka()
	{
		VelocityVodka=1;
		(MainCamera.GetComponent("MotionBlur") as MotionBlur).enabled=false;
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
	

	// Update is called once per frame
	void Update () {
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
		
		PlaceBearToControl(posx);
		
		bearAnimation.SetWalkSpeed(GetRealVelocityWithNoDeltaTime()/startVelocity);
		
		SwitchAnimation();
	}
	
	private void SwitchAnimation()
	{
		if(GlobalOptions.playerStates==PlayerStates.WALK||GlobalOptions.playerStates==PlayerStates.FLY){
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.FORWARD)
			{
				bearAnimation.Walk();
			}
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.LEFT)
			{
				bearAnimation.Left();
			}
			
			if(GlobalOptions.playerStatesPathChanging==PlayerStatesPathChanging.RIGHT)
			{
				bearAnimation.Right();
			}
		}
		if(GlobalOptions.playerStates==PlayerStates.IDLE)
		{
			bearAnimation.Idle();
		}
		if(GlobalOptions.playerStates==PlayerStates.JUMP&&!characterMarioC.isGliding())
		{
			bearAnimation.Jump();
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
	
	protected void GetSharedAccelerateDispatcher(){
		sharedAccelerometerDispatcher = AccelerometerDispatcher.GetSharedAccelerateDispatcher();
	}
	
	protected virtual void initaccel(){
		GetSharedAccelerateDispatcher();
		sharedAccelerometerDispatcher.addTargetedDelegate(this,accelPriority,swallowAcceles);
	}
	
	public Vector3 GetCharacterPosition()
	{
		return Character.transform.position;
	}
	
	public void ChangePath(bool toRight)
	{
		//смена дорожки
		if(!toRight){
			PathChanging=true;
			GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.LEFT;
			prevPathNumber=PathNumber;
			PathNumber--;
			PathNumber=PathNumber<-pathNumberLeftRight?-pathNumberLeftRight:PathNumber;
		}
		
		if(toRight){
			PathChanging=true;
			GlobalOptions.playerStatesPathChanging=PlayerStatesPathChanging.RIGHT;
			prevPathNumber=PathNumber;
			PathNumber++;
			PathNumber=PathNumber>pathNumberLeftRight?pathNumberLeftRight:PathNumber;
		}
	}
	
	private void MoveLeftRight(float inmoveForce)
	{
		//forward
		if(maxVelocity>GlobalOptions.playerVelocity){
			GlobalOptions.playerVelocity+=acceleration*Time.deltaTime;
		}
		Vector3 smex=new Vector3(0,0,GetRealVelocity());
		/*Vector3 oldPos=Vector3.zero;
		if(GlobalOptions.flagOnlyFizik)
		{
			oldPos=Character.transform.position;
		}
		else
		{
			oldPos=singleTransform.position;
		}*/
		//centerXandYandAngle=worldFactoryScript.GetXandYandAngleSmexForZ(smex);	
		
		Transform curtransform=Character.transform;
		
		posx=curtransform.localPosition.x;
		
		float centerx=0;
		
		//Ezxtra test out of bounds
		if(typeOfControl==0||typeOfControl==1)
		{		
			float forcex=(-Character.transform.localPosition.x+PathNumber*meshPath)*roadChangeForce;
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
		}	
		
		
		//accelerometer
		/*if(typeOfControl==2)
		{
			xSmexcontrol1+=inmoveForce*mnoshOfForce*Time.deltaTime;
			posx=centerx;
			xSmexcontrol1=xSmexcontrol1>=meshPath?meshPath:xSmexcontrol1;
			xSmexcontrol1=xSmexcontrol1<=-meshPath?-meshPath:xSmexcontrol1;
			posx+=xSmexcontrol1;
		}*/
			
			
		///////////
		//noangle
		/*
		if(typeOfControl==0||typeOfControl==1||typeOfControl==2)
		{
			float angle=GlobalOptions.GetAngleOfRotation(oldPos,centerXandYandAngle);
			
			RotatePlayer(angle);
		}*/
		
		if(GlobalOptions.flagOnlyFizik)
		{
			MakeMovingCharacterControllerForward();
		}
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
	
	public void PlaceBearToControl(float inposx)
	{
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
		{
			return;
		}
		Vector3 walkbearpos=walkingBear.transform.localPosition;
		Vector3 charpos=Character.transform.localPosition;
		if(characterMarioC.isJumping()&&GlobalOptions.gameState!=GameStates.GAME_OVER)
		{
			WhereToLook.transform.localPosition=new Vector3(charpos.x*whereToLookParalax,WhereToLook.transform.localPosition.y,raznFromWhereToLookAndCharacter.z+charpos.z);
		}
		else
		{
			float heightDamping=2f;
			if(characterMarioC.isFlying()||characterMarioC.ismovingToFlyGround())
			{
				heightDamping=10f;
			}			
			
			float raznost=raznFromWhereToLookAndCharacter.y;
			if(characterMarioC.isGliding()&&GlobalOptions.playerStates!=PlayerStates.DIE)
			{
				raznost-=3f;
			}
			if(GlobalOptions.playerStates==PlayerStates.DIE)
			{
				raznost-=2;
				heightDamping=2f;
			}
			float currentHeight = Mathf.Lerp (WhereToLook.transform.localPosition.y, charpos.y+walkbearpos.y+raznost, heightDamping * Time.deltaTime);
			WhereToLook.transform.localPosition=new Vector3(charpos.x*whereToLookParalax,currentHeight,raznFromWhereToLookAndCharacter.z+charpos.z);
		}
	}
	
	public void PlaceCharacter(Vector3 inpos)
	{
		singleTransform.position=inpos;
	}
	
	private void BearRespawn(){
		bearAnimation.ResumeAnimation();
		GlobalOptions.whereToBuild=new Vector3(0,0,1);
		RotatePlayer(0);	
		singleTransform.position=PlayerFirstPos;
		
		Character.transform.localPosition=new Vector3(0,0,0);
		walkingBear.transform.localPosition=new Vector3(0,0,0);
		PlaceBearToControl(0);

		CharacterControllerRespawn();
		
		//MainCamera
		MainCamera.transform.position=CameraFirstPos;
		MainCamera.transform.rotation=CameraFirstRotation;
	}
	
	
	private void MovingButtons(){
		if(Input.GetAxis ("Horizontal")!=0){
			force=Input.GetAxis ("Horizontal");
		}
	}
	
	public virtual bool Accelerate(Vector3 acceleration,int infingerId) {
		force = acceleration.x*2f;
		if(typeOfControl==1){
			float epsilonForse=0.6f;
			//right??
			if(force>epsilonForse&&PathNumber<=0)
			{
				
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				prevPathNumber=PathNumber;
				PathNumber=1;
			}			
			if(force<-epsilonForse&&PathNumber>=0)
			{
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				prevPathNumber=PathNumber;
				PathNumber=-1;
			}
			if(force>=-epsilonForse/3&&force<=epsilonForse/3&&PathNumber!=0){
				PathChanging=true;
				GlobalOptions.playerStates=PlayerStates.WALK;
				prevPathNumber=PathNumber;
				PathNumber=0;
			}
		}
		return true;
	}
	
	public float GetVelocityCurMnoshitel()
	{
		return GetRealVelocityWithNoDeltaTime()/startVelocity;
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
		Debug.Log ("GameOver");
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
		//Vector3 curpos=Character.transform.position;
		float forward=GetRealVelocityWithNoDeltaTime();
		characterMarioC.SetMovement(forward);
		
		if(Character.transform.position.z>worldFactoryScript.GetCurTerrainEnd().z+20)
		{
			worldFactoryScript.TryAddTerrrain();
		}
	}
	
	private void MakeRotationCharacterController(float inangle)
	{
		//Debug.Log (inangle);
		Character.transform.rotation=Quaternion.Euler(0, inangle, 0);
		WhereToLook.transform.rotation=Quaternion.Euler(0, inangle, 0);
	}
	
	private void CharacterControllerRespawn(){
		characterMarioC.Respawn();
	}
	
	private void TestIsFallen(){
		if(Character.transform.position.y+10<worldFactoryScript.GetCurTerrainCenter())
		{
			guiLayer.ShowGameOver();
		}
	}
	
	public void Stumble(Transform inTransform)
	{
		MoveCharacterControllerLeftRight(0);
		PathNumber=prevPathNumber;
		PathChanging=true;
		guiLayer.AddToLife(-3,inTransform);
		guiLayer.AddHeadStars();
	}
	
	public void StumbleTrigger()
	{
		bearAnimation.Stumble();
	}
	
	public void PlaceCharacterFirstly(Vector3 inpos)
	{
		Character.transform.position=inpos;
		Vector3 walkbearpos=walkingBear.transform.localPosition;
		Vector3 charpos=Character.transform.localPosition;
		WhereToLook.transform.localPosition=new Vector3(charpos.x*whereToLookParalax,charpos.y+walkbearpos.y+raznFromWhereToLookAndCharacter.y,raznFromWhereToLookAndCharacter.z+charpos.z);
	}
}
