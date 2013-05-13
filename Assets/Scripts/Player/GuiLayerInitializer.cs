using UnityEngine;
using System.Collections;

public class GuiLayerInitializer : Abstract {
	public GameObject GuiTimer;
	public GameObject GuiQuestion;
	public GameObject GuiStar;
	public GameObject GuiChangeControl;
	
	public GameObject GuiBearFace;
	public GameObject []GuiBottle;
	public GameObject GuiLifeHeart;
	public GameObject GuiScoreScale;
	
	//final
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
	private float oldTime;
	private int curLife;
	
	private GameObject curStrobile;
	private float curStarPos=250;
	
	private Player playerScript;
	private int scoreScale;
	private bool flagVodka,flagMushroom,flagScoreScale,flagHeadStars,flagMagnit,flagPropeller;
	private bool flagPostal,flagGameOver,flagMeters,flagMission;
	private float ShroomTime,ScoreScaleTime,scoreTime,headStarsTime,postalTime,GameOverTime,metersTime,missionTime,propellerTime,addToLifeTime;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	private float zindex=8;
	private ArrayList StarsList;
	Camera GUIcamera;
	
	private void AddAllTimes()
	{
		if(startstopTime!=0)
		{
			ShroomTime+=stopTime;
			ScoreScaleTime+=stopTime;
			scoreTime+=stopTime;
			headStarsTime+=stopTime;
			postalTime+=stopTime;
			GameOverTime+=stopTime;
			metersTime+=stopTime;
			propellerTime+=stopTime;
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

		GlobalOptions.scaleFactory=Screen.height/GlobalOptions.Vsizey;
		GlobalOptions.scaleFactorx=Screen.width/GlobalOptions.Vsizex;
		
		nullTime=0;
		flagVodka=false;
		flagMushroom=false;
		flagScoreScale=false;
		flagHeadStars=false;
		flagPostal=false;
		flagMeters=false;
		flagMission=false;
		flagGameOver=false;
		flagMagnit=false;
		flagPropeller=false;
		oldTime=nullTime;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		scoreScale=GlobalOptions.GetScoreScale();
		curLife=MaxLife;
		
		InitSprites();
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
		oldTime=nullTime;
		flagVodka=false;
		flagMushroom=false;
		flagScoreScale=false;
		flagHeadStars=false;
		flagPostal=false;
		flagMeters=false;
		flagMission=false;
		flagGameOver=false;
		flagMagnit=false;
		flagPropeller=false;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		AddTimer(0);
		StopVodka();
		AddToLife(0);
		GlobalOptions.SetScoreScale(1);
		HideQuestion();
		HidePostal();
		
		SetMoney(GlobalOptions.GetLevelStartMoney());
		SetPoints (GlobalOptions.GetLevelStartPoints());
	}
	
	void Update () {
		if(GlobalOptions.gameState==GameStates.GAME){
			
			AddAllTimes();
			
			AddTimer(0.5f);
			AddScoreForVelocity();
			AddLifeForVelocity();
			
			//уменьшаем propeler
			if(flagPropeller)
			{
				MakePropeller();
			}
			//уменьшаем грибы
			if(flagMushroom)
			{
				MakeMushroom();
			}
			
			//уменьшаем звёздочки надо головой
			if(flagHeadStars)
			{
				MakeHeadStars();
			}
			
			//показать окно, что мы что-то нашли
			if(flagPostal)
			{
				MakePostal();
			}
			
			//показать окно, что мы что-то нашли
			if(flagMeters)
			{
				MakeMeters();
			}
			
			//показать окно, что мы выполнили миссию
			if(flagMission)
			{
				MakeMission();
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
	
	public void AddMissionFinished(Mission mission){
		upNotifierController.AddMissionFinishedNotifier(mission);
	}
	
	public void AddCurrentMission(MissionNotifier missionNotifier){
		currentMissionsNotifierController.AddCurrentMissionNotifier(missionNotifier);
	}
	
	private void InitSprites()
	{
		Vector3 pos;	
		
		//FinalGUI	
		
		//mission plashka
		//AddOneMissionObject();
		//////////////////////////////////////////////////////////
		
		
		// new HUD////////////////////////////////////////////////
		
		//BearFace
		/*float bearFaceRight;
		float bearFaceBottom;
		BearFace = (GameObject)Instantiate(GuiBearFace);
		
		pos=new Vector3(0,GlobalOptions.Vsizey-3,zindex+50);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		bearFaceBottom=pos.y-BearFace.renderer.bounds.size.y;
		
		pos.x=BearFace.renderer.bounds.extents.x+bottleRight;
		pos.y-=BearFace.renderer.bounds.extents.y;
		
		bearFaceRight=pos.x+BearFace.renderer.bounds.extents.x;
		
		BearFace.transform.position=pos;
		BearFace.transform.parent=transform;*/
		
		//lifeindicator LifeHeart
		//MakeStars
		/*for(int i=0;i<LifeHeart.Length;i++)
		{
			LifeHeart[i]=(GameObject)Instantiate(GuiLifeHeart);
			pos=new Vector3(0,0,zindex-10);
			pos=GlobalOptions.NormalisePos(pos);
			pos=GUIcamera.ScreenToWorldPoint(pos);
			
			
			pos.x=LifeHeart[i].renderer.bounds.extents.x+bottleRight+LifeHeart[i].renderer.bounds.size.x*i;
			pos.y=-LifeHeart[i].renderer.bounds.extents.y+bearFaceBottom;
			
			LifeHeart[i].transform.position=pos;
			LifeHeart[i].transform.parent=transform;
		}*/
		
		SetLife(MaxLife);
		
		//simply set score 
		SetPoints(0);
		
		//simply set money 
		SetMoney(0);
		
		//ScoreScale
		ScoreScale=(GameObject)Instantiate(GuiScoreScale);
		PosScoreScale();
		ScoreScale.transform.parent=transform;
		
		//Question
		Question = (GameObject)Instantiate(GuiQuestion);
		
		pos=new Vector3(GlobalOptions.Vsizex-8,190,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.x-=Question.renderer.bounds.extents.x;
		pos.y-=Question.renderer.bounds.extents.y;
		
		Question.transform.position=pos;
		Question.active=false;
		Question.transform.parent=transform;
		
		//Change Control
		ChangeControl = (GameObject)Instantiate(GuiChangeControl);
		
		pos=new Vector3(8,190,zindex+50);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.x+=ChangeControl.renderer.bounds.extents.x;
		pos.y-=ChangeControl.renderer.bounds.extents.y;
		
		ChangeControl.transform.position=pos;
		ChangeControl.transform.parent=transform;
		
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
	
	private void MakePropeller()
	{
		if(Time.time-propellerTime>10)
		{
			flagPropeller=false;
			playerScript.UnMakePropeller();
		}
	}
	
	private void MakeHeadStars()
	{
		if(Time.time-headStarsTime>0.6)
		{
			flagHeadStars=false;
			playerScript.UnMakeHeadStars();
		}
	}
	
	private void MakeMushroom()
	{
		//stop mushroom
		if(Time.time-ShroomTime>4)
		{
			flagMushroom=false;
			playerScript.UnMakeMushrooms();
		}
	}
	
	/*private void MakeScoreScale()
	{
		//stop mushroom
		if(Time.time-ScoreScaleTime>10)
		{
			flagScoreScale=false;
			AddScoreScale(-1);
		}
	}*/
	
	private void MakePostal()
	{
		//stop postal
		if(Time.time-postalTime>2)
		{
			flagPostal=false;
			HidePostal();
		}
	}
	
	private void MakeMeters()
	{
		//stop mushroom
		if(Time.time-metersTime>2)
		{
			flagMeters=false;
		}
	}
	
	private void MakeMission()
	{
		//stop mushroom
		if(Time.time-missionTime>3)
		{
			flagMission=false;
		}
	}
	
	
	public void AddTimer(float inshag)
	{
		/*nullTime+=Time.deltaTime;
		if(nullTime-oldTime>=inshag)
		{
			tk2dTextMesh textMesh;
			textMesh = Timer.GetComponent<tk2dTextMesh>();
			int min,sec;
			min=(int)nullTime/60;
			sec=(int)nullTime % 60;
			textMesh.text = string.Format ("{0:00}:{1:00}", min,sec);
			textMesh.Commit();
			oldTime=nullTime;
		}*/
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
		flagVodka=true;
	}
	
	public void StopVodka(){
		flagVodka=false;
		playerScript.UnMakeVodka();	
	}
	
	public void AddMagnit(Boost boostPrefab)
	{
		boostNotifierController.AddBoostNotifier(boostPrefab);
		playerScript.MakeMagnit();
		flagMagnit=true;
	}
	
	public void StopMagnit(){
		flagMagnit=false;
		playerScript.UnMakeMagnit();	
	}
	
	public void AddPostal(){
		ShowPostal();
		postalTime=Time.time;
		flagPostal=true;
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
		metersTime=Time.time;
		flagMeters=true;
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
	
	public void AddMushroom()
	{
		playerScript.MakeMushrooms();
		flagMushroom=true;
		ShroomTime=Time.time;
	}
	
	
	public void AddPropeller()
	{
		playerScript.MakePropeller();
		flagPropeller=true;
		propellerTime=Time.time;
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
