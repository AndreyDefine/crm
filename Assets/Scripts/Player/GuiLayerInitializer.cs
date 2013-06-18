using UnityEngine;
using System.Collections;

public class GuiLayerInitializer : Abstract {	
	//final
	public GuiHeadStart GUIHeadStart;
	
	public GameObject pause;
	
	public ResumeTimer resumeTimerPrefab;
	
	public int MaxLife;
	
	public Money money;
	
	public Points points;
	
	public X multiplier;
	
	public UpNotifierController upNotifierController;
	
	public DownNotifierController downNotifierController;
	
	public CurrentMissionsNotifierController currentMissionsNotifierController;
	
	public BoostNotifierController boostNotifierController;		
	
	private float Vsizex,Vsizey;
	private float nullTime;
	private int curLife;
	
	private Player playerScript;
	private bool flagHeadStars;
	private bool flagPostal,flagGameOver;
	private float ScoreScaleTime,scoreTime,headStarsTime,GameOverTime, addToLifeTime;
	private bool flagX2=false;
	private bool flagNotTwinkled=true;
	int curX2 = 0;
	
	private GuiHeadStart HeadStart;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	private ArrayList StarsList;
	
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
		
		playerScript=GlobalOptions.GetPlayerScript();

		InitSprites();
		Restart();
	}
	
	private void MakeHeadStart()
	{
		flagNotTwinkled=false;
		if(!PersonInfo.tutorial)
		{
			HeadStart.StartTwinkling();
		}
		else
		{
			HeadStart.StopTwinkling();
		}
	}
	
	public void MakeHeadStartButtonPushed(float indistance)
	{
		HeadStart.StopTwinkling();
		playerScript.MakeHeadStart();
	}
	
	public void Restart()
	{
		StarsList.Clear();
		stopTime=0;
		startstopTime=0;
		
		curLife=MaxLife;
		nullTime=0;
		flagHeadStars=false;
		flagGameOver=false;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		StopVodka();
		AddToLife(0,null);
	
		flagX2 = false;
		flagNotTwinkled=true;
		//simply set X
		SetMultiplier();
		
		SetMoney(GlobalOptions.GetLevelStartMoney());
		SetPoints (GlobalOptions.GetLevelStartPoints());
		pause.SetActive(true);
		
		GlobalOptions.GetMissionEmmitters().RestartActiveMissions();
		
		currentMissionsNotifierController.Restart();
		upNotifierController.Restart();
		boostNotifierController.Restart();
		downNotifierController.Restart();
		HeadStart.ResetTwinkling();
	}
	
	void Update () {
		
		if(GlobalOptions.gameState==GameStates.GAME){
			
			AddAllTimes();
			
			AddScoreForVelocity();
			AddLifeForVelocity();
			SetMultiplier();//Пока так.
					
			//уменьшаем звёздочки надо головой
			if(flagHeadStars)
			{
				MakeHeadStars();
			}
			
			if(flagNotTwinkled)
			{
				MakeHeadStart();
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
		pause.SetActive(true);
		GlobalOptions.GetPlayerScript().ResumeGame();		
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
		if(PersonInfo.tutorial){
			upNotifierController.AddTutorialMissionNotifier(mission);
		}else{
			upNotifierController.AddMissionNotifier(mission);
		}
	}
	
	public void AddCurrentMission(MissionNotifier missionNotifier){
		currentMissionsNotifierController.AddCurrentMissionNotifier(missionNotifier);
	}
	
	private void InitSprites()
	{	
		//Head Start
		HeadStart=(Instantiate(GUIHeadStart) as GuiHeadStart);
		
		//simply set score 
		SetPoints(0);
		
		//simply set money 
		SetMoney(0);
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
			AddPoints(GlobalOptions.GetScoreScale());
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
		GlobalOptions.GetMissionEmmitters().NotifyPointsAdded(addPoints);
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
	
	public void SetMultiplier()
	{
		int m = PersonInfo.personLevel;
		if(flagX2){
			m*=2;
			this.multiplier.setColor(new Color(1f,0f,0f,1f));
		}else{
			this.multiplier.setColor(new Color(1f,1f,1f,1f));
		}
		if(curX2!=m){
			curX2 = m;
			this.multiplier.SetValue(m);
			GlobalOptions.SetScoreScale(m);
		}
	}
	
	public void AddX2(Boost boostPrefab)
	{
		GlobalOptions.GetMissionEmmitters().NotifyX2Collected(1);
		boostNotifierController.AddBoostNotifier(boostPrefab);
		flagX2 = true;
		SetMultiplier();
	}
	
	public void StopX2(){
		flagX2 = false;
		SetMultiplier();
	}
	
	public void AddVodka(Boost boostPrefab)
	{
		GlobalOptions.GetMissionEmmitters().NotifyVodkaCollected(1);
		boostNotifierController.AddBoostNotifier(boostPrefab);
		playerScript.MakeVodka();
	}
	
	public void AddPosilka()
	{
		playerScript.MakePosilka();
	}
	
	public void StopVodka(){
		playerScript.UnMakeVodka();
	}
	
	public void AddMagnit(Boost boostPrefab)
	{
		GlobalOptions.GetMissionEmmitters().NotifyMagnitCollected(1);
		boostNotifierController.AddBoostNotifier(boostPrefab);
		playerScript.MakeMagnit();
	}
	
	public void StopMagnit(){
		playerScript.UnMakeMagnit();	
	}
	
	public void AddPostal(){
		GlobalOptions.GetMissionEmmitters().NotifyPostCollected(1);
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
		pause.SetActive(false);
	}
	
	private void MakeGameOver()
	{
		HeadStart.StopTwinkling();
		//gameover
		if(Time.time-GameOverTime>2)
		{
			flagGameOver=false;
			ScreenLoader screenLoader;
			screenLoader=GlobalOptions.GetScreenLoader();
			//есть почта
			if(PersonInfo.post<=0)
			{
				screenLoader.LoadScreenByName("ScreenGameOver");
			}
			else
			{
				screenLoader.LoadScreenByName("ScreenPost");
			}
		}
	}
	
	public void ShowYouWon()
	{
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			GlobalOptions.playerStates=PlayerStates.IDLE;
			GlobalOptions.gameState=GameStates.LEVEL_COPLETE;
			ScreenLoader screenLoader;
			screenLoader=GlobalOptions.GetScreenLoader();
			screenLoader.LoadScreenByName("ScreenYouWon");
		}
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
		if(inlife<0&&playerScript.isVodka()){
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
			FlurryPlugin.FlurryLogEvent(DeadEvent);
		}
		curLife=curLife<0?0:curLife;
		curLife=curLife>MaxLife?MaxLife:curLife;
	}
	
	public void AddHeadStars()
	{
		playerScript.MakeHeadStars();
		flagHeadStars=true;
		headStarsTime=Time.time;
	}
}
