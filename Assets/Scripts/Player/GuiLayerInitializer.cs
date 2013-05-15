using UnityEngine;
using System.Collections;

public class GuiLayerInitializer : Abstract {
	public GameObject GuiTimer;
	public GameObject GuiStar;
	public GameObject GuiChangeControl;
	
	public GameObject GuiBearFace;
	public GameObject []GuiBottle;
	public GameObject GuiLifeHeart;
	public GameObject GuiScoreScale;
	
	//final
	public GameObject pause;
	
	public ResumeTimer resumeTimerPrefab;
	
	public Money money;
	
	public Points points;
	
	public UpNotifierController upNotifierController;
	
	public DownNotifierController downNotifierController;
	
	public CurrentMissionsNotifierController currentMissionsNotifierController;
	
	public BoostNotifierController boostNotifierController;
	
	private ArrayList FinalMissionPlashkaArray=new ArrayList();

	
	public int MaxLife;
	
	private GameObject Timer;
	private GameObject ChangeControl;
	
	private GameObject BearFace;
	private GameObject RightHUD;
	private GameObject []LifeHeart;
	private GameObject ScoreScale;

	private GameObject Question;
	private GameObject Star;
	
	private float Vsizex,Vsizey;
	private float nullTime;
	private int curLife;
	
	private GameObject curStrobile;
	private float curStarPos=250;
	
	private Player playerScript;
	private int scoreScale;
	private bool flagHeadStars, flagVodka;
	private bool flagPostal,flagGameOver;
	private float ScoreScaleTime,scoreTime,headStarsTime,GameOverTime, addToLifeTime;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	private float zindex=8;
	private ArrayList StarsList;
	Camera GUIcamera;
	
	private void AddAllTimes()
	{
		if(startstopTime!=0)
		{
			ScoreScaleTime+=stopTime;
			scoreTime+=stopTime;
			headStarsTime+=stopTime;
			GameOverTime+=stopTime;
			addToLifeTime+=stopTime;
			stopTime=0;
			startstopTime=0;
		}
	}
	// Use this for initialization
	void Start() {
		StarsList=new ArrayList();
		LifeHeart=new GameObject[5];
		curStrobile=null;
		GUIcamera = Cameras.GetGUICamera(); 
		
		playerScript=GlobalOptions.GetPlayer().GetComponent("Player")as Player;

		scoreScale=GlobalOptions.GetScoreScale();
		
		InitSprites();
		Restart();
	}
	
	public void Restart()
	{
		for(int i=0;i<StarsList.Count;i++)
		{
			Debug.Log ("Destroy");
			Destroy(StarsList[i] as GameObject);
		}
		StarsList.Clear();
		curStarPos=250;
		stopTime=0;
		startstopTime=0;
		
		curStrobile=null;
		curLife=MaxLife;
		nullTime=0;
		flagHeadStars=false;
		flagGameOver=false;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		StopVodka();
		AddToLife(0,null);
		GlobalOptions.SetScoreScale(1);
		
		SetMoney(GlobalOptions.GetLevelStartMoney());
		SetPoints (GlobalOptions.GetLevelStartPoints());
	}
	
	void Update () {
		
		if(GlobalOptions.gameState==GameStates.GAME){
			
			AddAllTimes();
			
			AddScoreForVelocity();
			AddLifeForVelocity();
					
			//уменьшаем звёздочки надо головой
			if(flagHeadStars)
			{
				MakeHeadStars();
			}

		}
		else
		{
			if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
			{
				if(startstopTime==0)
				{
					startstopTime=Time.time;
				}

				stopTime=Time.time-startstopTime;
			}
		}
		
		if(GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			if(flagGameOver)
			{
				MakeGameOver();
			}
		}
	}
	
	public void ResumeTimer(){
		ResumeTimer resumeTimer = Instantiate(resumeTimerPrefab) as ResumeTimer;
		resumeTimer.StartTimer();
	}
	
	public void Resume(){
		pause.active = true;
		GlobalOptions.GetPlayerScript().ResumeGame();		
	}
	
	public void AddCap()
	{
		playerScript.ShowCap();
	}
	
	public void ShowQuestion(GameObject inobj)
	{
		curStrobile=inobj;
		Question.active=true;
	}
	
	public void HideQuestion()
	{
		Question.active=false;
	}
	
	public void RemoveStrobile()
	{
		if(curStrobile){
			(curStrobile.GetComponent("StrobileEnemy") as StrobileEnemy).MakeInactive();
			Question.active=false;
			curStrobile=null;
		}
	}
	
	public int GetPoints()
	{
		return points.GetPoints();
	}
	
	public int GetMoney()
	{
		return money.GetMoney();
	}
	
	public int GetHP()
	{
		return curLife;
	}
	
	public float GetTime()
	{
		return nullTime;
	}
	
	public void AddMission(Mission mission){
		upNotifierController.AddMissionNotifier(mission);
	}
	
	public void AddCurrentMission(MissionNotifier missionNotifier){
		currentMissionsNotifierController.AddCurrentMissionNotifier(missionNotifier);
	}
	
	private void InitSprites()
	{
		Vector3 pos;	
		
		SetLife(MaxLife);
		
		//simply set score 
		SetPoints(0);
		
		//simply set money 
		SetMoney(0);
		
		//ScoreScale
		ScoreScale=(GameObject)Instantiate(GuiScoreScale);
		PosScoreScale();
		ScoreScale.transform.parent=transform;
				
		//timer left coner up
		/*Timer=(GameObject)Instantiate(GuiTimer);
		//Timer.transform.localScale=GlobalOptions.NormaliseScale();
		
		pos=new Vector3(8,GlobalOptions.Vsizey-152,zindex-10);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.y-=Timer.renderer.bounds.extents.y;
		
		pos.x=bearFaceRight;
		
		Timer.transform.position=pos;
		Timer.transform.parent=transform;*/
	}
	
	private void MakeHeadStars()
	{
		if(Time.time-headStarsTime>0.6)
		{
			flagHeadStars=false; 
			playerScript.UnMakeHeadStars();
		}
	}
			
	public void AddScoreForVelocity()
	{
		if(Time.time-scoreTime>=playerScript.GetRealVelocityWithNoDeltaTime()/1000)
		{
			//так редко меняем счёт
			AddPoints(11*scoreScale);
			scoreTime=Time.time;
		}
	}
	
	public void AddLifeForVelocity()
	{
		if(Time.time-addToLifeTime>=playerScript.GetRealVelocityWithNoDeltaTime())
		{
			//так редко меняем счёт
			AddToLife(1,null);
			addToLifeTime=Time.time;
		}
	}
	
	public void AddPoints(int addPoints)
	{
		this.points.AddPoints(addPoints);
	}
	
	public void AddMoney(int addMoney)
	{
		GlobalOptions.GetMissionEmmitters().NotifyCoinsCollected(addMoney);
		this.money.AddMoney(addMoney);
	}
	
	public void SetPoints(int points)
	{
		this.points.SetPoints(points);
	}
	
	public void SetMoney(int money)
	{
		this.money.SetMoney(money);
	}
	
	public void AddX2(Boost boostPrefab)
	{
		boostNotifierController.AddBoostNotifier(boostPrefab);
		//PosScoreScale();
		GlobalOptions.SetScoreScale(2);
	}
	
	public void StopX2(){
		GlobalOptions.SetScoreScale(1);
	}
	
	public void AddVodka(Boost boostPrefab)
	{
		boostNotifierController.AddBoostNotifier(boostPrefab);
		playerScript.MakeVodka();
		flagVodka = true;
	}
	
	public void StopVodka(){
		playerScript.UnMakeVodka();
		flagVodka = false;
	}
	
	public void AddMagnit(Boost boostPrefab)
	{
		boostNotifierController.AddBoostNotifier(boostPrefab);
		playerScript.MakeMagnit();
	}
	
	public void StopMagnit(){
		playerScript.UnMakeMagnit();	
	}
	
	public void AddPostal(){
		ShowPostal();
	}
	
	private void ShowPostal()
	{
		//Postal.active=true;
	}
	
	private void HidePostal()
	{
		//Postal.active=false;
	}
	
	public void AddMeters(float inMeters){
		ShowMeters(inMeters);
	}
	
	private void ShowMeters(float inMeters)
	{
		downNotifierController.AddMetersNotifier(string.Format ("{0}", inMeters));
	}
	
	public void ShowGameOver()
	{
		GlobalOptions.playerStates=PlayerStates.DIE;
		GlobalOptions.gameState=GameStates.GAME_OVER;
		playerScript.GameOver();
		flagGameOver=true;
		GameOverTime=Time.time;
	}
	
	private void MakeGameOver()
	{
		//gameover
		if(Time.time-GameOverTime>3)
		{
			flagGameOver=false;
			ScreenLoader screenLoader;
			screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
			screenLoader.LoadScreenByName("ScreenGameOver");
		}
	}
	
	public void ShowYouWon()
	{
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			GlobalOptions.playerStates=PlayerStates.IDLE;
			GlobalOptions.gameState=GameStates.LEVEL_COPLETE;
			ScreenLoader screenLoader;
			screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
			screenLoader.LoadScreenByName("ScreenYouWon");
		}
	}
	
	/*private void PosScore(){
		Vector3 pos;
		pos=new Vector3(GlobalOptions.Vsizex-20,GlobalOptions.Vsizey-60,zindex-1);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		Score.transform.position=pos;
	}*/
	
	/*private void PosMoney(){
		Vector3 pos;
		pos=new Vector3(GlobalOptions.Vsizex-20,GlobalOptions.Vsizey-135,zindex-1);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		Money.transform.position=pos;
	}*/
	
	private void PosScoreScale(){
		Vector3 pos;
		pos=new Vector3(406,GlobalOptions.Vsizey-32,zindex+50);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		ScoreScale.transform.position=pos;
	}
	
	private void PlusLife(){
		curLife++;
		curLife=curLife>MaxLife?MaxLife:curLife;
	}
	
	private void MinusLife(){
		curLife--;
		curLife=curLife<0?0:curLife;
	}
	
	public void AddToLife(int inlife,Transform inTransform){
		
		int oldlife=curLife;
		//препятствие и водка
		if(inlife<0&&flagVodka){
			return;
		}
		curLife+=inlife;
		if(curLife<=0&&oldlife>0)
		{
			ShowGameOver();
			string DeadEvent;
			if(inTransform)
			{
				DeadEvent="Dead ";
				while(inTransform)
				{
					DeadEvent+="\\"+inTransform.name;
					inTransform=inTransform.parent;
				}
			}
			else
			{
				DeadEvent="Dead";
			}
			Debug.Log(DeadEvent);
			FlurryPlugin.FlurryLogEvent(DeadEvent);
		}
		curLife=curLife<0?0:curLife;
		curLife=curLife>MaxLife?MaxLife:curLife;
		
		SetLife(curLife);
	}
	
	private void SetLife(int inlife){
		/*int numberOfOnHearts=(int)((LifeHeart.Length+1)*((float)inlife/MaxLife));
		for(int i=0;i<LifeHeart.Length;i++)
		{
			if(i>numberOfOnHearts-1)
			{
				LifeHeart[i].active=false;
			}
			else
			{
				LifeHeart[i].active=true;
			}
		}*/
	}
		
	public void AddHeadStars()
	{
		playerScript.MakeHeadStars();
		flagHeadStars=true;
		headStarsTime=Time.time;
	}
	
	public void ShowStar(){
		if(StarsList.Count<7)
		{
			Debug.Log(StarsList.Count);
			Vector3 pos;
			//Star
			Star = (GameObject)Instantiate(GuiStar);
			
			pos=new Vector3(8,curStarPos,zindex-1);
			pos=GlobalOptions.NormalisePos(pos);
			pos=GUIcamera.ScreenToWorldPoint(pos);
			pos.x+=Star.renderer.bounds.extents.x;
			pos.y+=Star.renderer.bounds.extents.y;
			
			curStarPos+=75;
			
			Star.transform.position=pos;
			
			StarsList.Add(Star);
		}
	}
}
