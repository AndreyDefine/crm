using UnityEngine;
using System.Collections;

public class GuiLayerInitializer : Abstract {
	public GameObject GuiTimer;
	public GameObject GuiQuestion;
	public GameObject GuiStar;
	public GameObject GuiChangeControl;
	
	public GameObject GuiBearFace;
	public GameObject []GuiBottle;
	public GameObject GuiRightHUD;
	public GameObject GuiLifeHeart;
	public GameObject GuiScore;
	public GameObject GuiMoney;
	public GameObject GuiScoreScale;
	public GameObject GuiPostal;
	public GameObject GuiMeters;
	
	public int MaxLife;
	
	private GameObject Timer;
	private GameObject ChangeControl;
	
	private GameObject BearFace;
	private GameObject []Bottle;
	private GameObject RightHUD;
	private GameObject []LifeHeart;
	private GameObject Score;
	private GameObject Money;
	private GameObject ScoreScale;
	private GameObject Postal;
	private GameObject Meters;

	private GameObject Question;
	private GameObject Star;
	
	private float Vsizex,Vsizey;
	private float nullTime;
	private float oldTime;
	private int curLife;
	
	private GameObject curStrobile;
	private float curStarPos=250;
	
	private Player playerScript;
	private int score,money,scoreScale,vodkaLevel;
	private bool flagVodka,flagMushroom,flagScoreScale,flagHeadStars,flagMagnit,flagPropeller;
	private bool flagPostal,flagGameOver,flagMeters;
	private float VodkaTime,ShroomTime,ScoreScaleTime,scoreTime,headStarsTime,postalTime,GameOverTime,metersTime,magnitTime,propellerTime,addToLifeTime;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	private float zindex=8;
	private ArrayList StarsList;
	Camera GUIcamera;
	
	private void AddAllTimes()
	{
		if(startstopTime!=0)
		{
			VodkaTime+=stopTime;
			ShroomTime+=stopTime;
			ScoreScaleTime+=stopTime;
			scoreTime+=stopTime;
			headStarsTime+=stopTime;
			postalTime+=stopTime;
			GameOverTime+=stopTime;
			metersTime+=stopTime;
			magnitTime+=stopTime;
			propellerTime+=stopTime;
			addToLifeTime+=stopTime;
			stopTime=0;
			startstopTime=0;
		}
	}
	// Use this for initialization
	void Start() {
		StarsList=new ArrayList();
		Bottle=new GameObject[4];
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
		flagGameOver=false;
		flagMagnit=false;
		flagPropeller=false;
		oldTime=nullTime;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		score=GlobalOptions.GetLevelStartScore();
		money=GlobalOptions.GetLevelStartMoney();
		vodkaLevel=0;
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
		vodkaLevel=0;
		flagVodka=false;
		flagMushroom=false;
		flagScoreScale=false;
		flagHeadStars=false;
		flagPostal=false;
		flagMeters=false;
		flagGameOver=false;
		flagMagnit=false;
		flagPropeller=false;
		scoreTime=Time.time;
		addToLifeTime=Time.time;
		AddTimer(0);
		AddVodka(0);
		AddToLife(0);
		AddScoreScale(-10);
		HideQuestion();
		HidePostal();
		HideMeters();
		
		score=GlobalOptions.GetLevelStartScore();
		money=GlobalOptions.GetLevelStartMoney();
		AddScore (0);
		AddMoney (0);
	}
	
	void Update () {
		if(GlobalOptions.gameState==GameStates.GAME){
			
			AddAllTimes();
			
			AddTimer(0.5f);
			AddScoreForVelocity();
			AddLifeForVelocity();
			//уменьшаем водку
			if(flagVodka)
			{
				MakeVodka();
			}
			
			//уменьшаем magnit
			if(flagMagnit)
			{
				MakeMagnit();
			}
			
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
			
			//уменьшаем матрёшку
			if(flagScoreScale)
			{
				MakeScoreScale();
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
	
	public int GetScore()
	{
		return score;
	}
	
	public int GetMoney()
	{
		return money;
	}
	
	public int GetHP()
	{
		return curLife;
	}
	
	public float GetTime()
	{
		return nullTime;
	}
	
	private void InitSprites()
	{
		Vector3 pos;	
		
		
		// new HUD////////////////////////////////////////////////
		//Bottle
		float bottleRight;
		Bottle[0] = (GameObject)Instantiate(GuiBottle[0]);
		
		pos=new Vector3(8,GlobalOptions.Vsizey-10,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		bottleRight=pos.x+Bottle[0].renderer.bounds.size.x;
		
		pos.x+=Bottle[0].renderer.bounds.extents.x;
		pos.y-=Bottle[0].renderer.bounds.extents.y;
		
		Bottle[0].transform.position=pos;
		
		Bottle[0].transform.parent=transform;
		
		//other bottles
		for (int i=1;i<GuiBottle.Length;i++)
		{
			Bottle[i] = (GameObject)Instantiate(GuiBottle[i]);
			Bottle[i].transform.position=pos;
			Bottle[i].active=false;
			Bottle[i].transform.parent=transform;
		}
		
		//BearFace
		float bearFaceRight;
		float bearFaceBottom;
		BearFace = (GameObject)Instantiate(GuiBearFace);
		
		pos=new Vector3(0,GlobalOptions.Vsizey-3,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		bearFaceBottom=pos.y-BearFace.renderer.bounds.size.y;
		
		pos.x=BearFace.renderer.bounds.extents.x+bottleRight;
		pos.y-=BearFace.renderer.bounds.extents.y;
		
		bearFaceRight=pos.x+BearFace.renderer.bounds.extents.x;
		
		BearFace.transform.position=pos;
		BearFace.transform.parent=transform;
		
		//RightHUD
		RightHUD = (GameObject)Instantiate(GuiRightHUD);
		
		pos=new Vector3(GlobalOptions.Vsizex-4,GlobalOptions.Vsizey-10,zindex);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		pos.x-=RightHUD.renderer.bounds.extents.x;
		pos.y-=RightHUD.renderer.bounds.extents.y;
		
		RightHUD.transform.position=pos;
		RightHUD.transform.parent=transform;
		
		//lifeindicator LifeHeart
		//MakeStars
		for(int i=0;i<LifeHeart.Length;i++)
		{
			LifeHeart[i]=(GameObject)Instantiate(GuiLifeHeart);
			pos=new Vector3(0,0,zindex-10);
			pos=GlobalOptions.NormalisePos(pos);
			pos=GUIcamera.ScreenToWorldPoint(pos);
			
			
			pos.x=LifeHeart[i].renderer.bounds.extents.x+bottleRight+LifeHeart[i].renderer.bounds.size.x*i;
			pos.y=-LifeHeart[i].renderer.bounds.extents.y+bearFaceBottom;
			
			LifeHeart[i].transform.position=pos;
			LifeHeart[i].transform.parent=transform;
		}
		
		SetLife(MaxLife);
		
		//Score
		Score=(GameObject)Instantiate(GuiScore);
		PosScore();
		Score.transform.parent=transform;
		//simply set score 
		AddScore(0);
		
		//Money
		Money=(GameObject)Instantiate(GuiMoney);
		PosMoney();
		Money.transform.parent=transform;
		//simply set money 
		AddMoney(0);
		
		//ScoreScale
		ScoreScale=(GameObject)Instantiate(GuiScoreScale);
		PosScoreScale();
		ScoreScale.transform.parent=transform;
		//simply set money 
		AddScoreScale(0);
		
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
		
		pos=new Vector3(8,190,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.x+=ChangeControl.renderer.bounds.extents.x;
		pos.y-=ChangeControl.renderer.bounds.extents.y;
		
		ChangeControl.transform.position=pos;
		ChangeControl.transform.parent=transform;
		
		//timer left coner up
		Timer=(GameObject)Instantiate(GuiTimer);
		//Timer.transform.localScale=GlobalOptions.NormaliseScale();
		
		pos=new Vector3(8,GlobalOptions.Vsizey-152,zindex-10);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.y-=Timer.renderer.bounds.extents.y;
		
		pos.x=bearFaceRight;
		
		Timer.transform.position=pos;
		Timer.transform.parent=transform;
		
		//Postal
		Postal = (GameObject)Instantiate(GuiPostal);
		
		pos=new Vector3(GlobalOptions.Vsizex/2,GlobalOptions.Vsizey-760,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		//pos.x-=Postal.renderer.bounds.extents.x;
		pos.y-=Postal.renderer.bounds.extents.y;
		
		Postal.transform.position=pos;
		Postal.transform.parent=transform;
		Postal.active=false;
		
		
		//Meters
		Meters = (GameObject)Instantiate(GuiMeters);
		
		pos=new Vector3(GlobalOptions.Vsizex/2,GlobalOptions.Vsizey-760,zindex);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
	
		pos.y-=Meters.renderer.bounds.extents.y;
		
		Meters.transform.position=pos;
		Meters.transform.parent=transform;
		Meters.SetActiveRecursively(false);
	}
	
	private void MakeVodka()
	{
		if(Time.time-VodkaTime>3)
		{
			VodkaTime=Time.time;
			AddVodka(-1);
		}
		//stop vodka
		if(vodkaLevel==0)
		{
			flagVodka=false;
			playerScript.UnMakeVodka();
		}
	}
	
	private void MakeMagnit()
	{
		if(Time.time-magnitTime>15)
		{
			flagMagnit=false;
			playerScript.UnMakeMagnit();
		}
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
	
	private void MakeScoreScale()
	{
		//stop mushroom
		if(Time.time-ScoreScaleTime>10)
		{
			flagScoreScale=false;
			AddScoreScale(-1);
		}
	}
	
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
			HideMeters();
		}
	}
	
	
	public void AddTimer(float inshag)
	{
		nullTime+=Time.deltaTime;
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
		}
	}
	
	public void AddScoreForVelocity()
	{
		if(Time.time-scoreTime>=playerScript.GetRealVelocityWithNoDeltaTime()/1000)
		{
			//так редко меняем счёт
			AddScore(11*scoreScale);
			scoreTime=Time.time;
		}
	}
	
	public void AddLifeForVelocity()
	{
		if(Time.time-addToLifeTime>=playerScript.GetRealVelocityWithNoDeltaTime())
		{
			//так редко меняем счёт
			AddToLife(1);
			addToLifeTime=Time.time;
		}
	}
	
	public void AddScore(int inscore)
	{
		score+=inscore;
		tk2dTextMesh textMesh;
		textMesh = Score.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:000000000}", score);
		textMesh.Commit();
		//PosScore();
	}
	
	public void AddMoney(int inscore)
	{
		money+=inscore;
		tk2dTextMesh textMesh;
		textMesh = Money.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:00000000}", money);
		textMesh.Commit();
		//PosMoney();
	}
	
	public void AddScoreScale(int inscore)
	{
		if(inscore>0)
		{
			flagScoreScale=true;
			ScoreScaleTime=Time.time;
		}
		scoreScale+=inscore;
		scoreScale=scoreScale>2?2:scoreScale;
		scoreScale=scoreScale<1?1:scoreScale;
		tk2dTextMesh textMesh;
		textMesh = ScoreScale.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("X{0:0}", scoreScale);
		textMesh.Commit();
		//PosScoreScale();
		GlobalOptions.SetScoreScale(scoreScale);
	}
	
	public void AddVodka(int inscore)
	{
		if(vodkaLevel+inscore>=0&&vodkaLevel+inscore<Bottle.Length)
		{
			for (int i=0;i<Bottle.Length;i++){
				Bottle[i].active=false;
			}
			vodkaLevel+=inscore;
			Bottle[vodkaLevel].active=true;
			if(vodkaLevel==Bottle.Length-1&&!flagVodka)
			{
				playerScript.MakeVodka();
				flagVodka=true;
				VodkaTime=Time.time;
			}
		}
	}
	
	
	
	
	public void AddPostal(){
		ShowPostal();
		postalTime=Time.time;
		flagPostal=true;
	}
	
	private void ShowPostal()
	{
		Postal.active=true;
	}
	
	private void HidePostal()
	{
		Postal.active=false;
	}
	
	public void AddMeters(float inMeters){
		ShowMeters(inMeters);
		metersTime=Time.time;
		flagMeters=true;
	}
	
	private void ShowMeters(float inMeters)
	{
		Transform MetersScore=Meters.transform.FindChild("Score");
		
		tk2dTextMesh textMesh;
		textMesh = MetersScore.gameObject.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0}", inMeters);
		textMesh.Commit();
		
		Meters.SetActiveRecursively(true);
	}
	
	private void HideMeters()
	{
		Meters.SetActiveRecursively(false);
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
	
	private void PosScore(){
		Vector3 pos;
		pos=new Vector3(649,GlobalOptions.Vsizey-49,zindex-1);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		Score.transform.position=pos;
	}
	
	private void PosMoney(){
		Vector3 pos;
		pos=new Vector3(610,GlobalOptions.Vsizey-93,zindex-1);
		pos=GlobalOptions.NormalisePosRight(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		
		Money.transform.position=pos;
	}
	
	private void PosScoreScale(){
		Vector3 pos;
		pos=new Vector3(406,GlobalOptions.Vsizey-32,zindex-1);
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
	
	public void AddToLife(int inlife){
		
		int oldlife=curLife;
		//препятствие и водка
		if(inlife<0&&flagVodka){
			return;
		}
		curLife+=inlife;
		if(curLife<=0&&oldlife>0)
		{
			ShowGameOver();
		}
		curLife=curLife<0?0:curLife;
		curLife=curLife>MaxLife?MaxLife:curLife;
		
		SetLife(curLife);
	}
	
	private void SetLife(int inlife){
		int numberOfOnHearts=(int)((LifeHeart.Length+1)*((float)inlife/MaxLife));
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
		}
	}
	
	public void AddMushroom()
	{
		playerScript.MakeMushrooms();
		flagMushroom=true;
		ShroomTime=Time.time;
	}
	
	public void AddMagnit()
	{
		playerScript.MakeMagnit();
		flagMagnit=true;
		magnitTime=Time.time;
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
