using UnityEngine;
using System.Collections;

public enum GameStates{
    IN_SWYPE,
    MENU_FADE_OUT,
    SUB_MENU,
    MAIN_MENU,
    PRELOADER,
    GAME,
    PAUSE_MENU,
    GAME_OVER,
    LEVEL_COPLETE
}

public enum PlayerStates
{
	DOWN,
	JUMP,
	IDLE,
	DIE,
	WALK,
	FLY
}

public enum PlayerStatesPathChanging
{
	LEFT,
	RIGHT,
	FORWARD
}

public class GlobalOptions {	
	public static bool flagOnlyFizik=true;
	
	public static bool qualitySeted = false;
	
	public static string loadingLevel="MainMenu";
	
	public static bool UnityPro=true;
	
	public static float playerVelocity;
	
	public static float globalPerspective=200;
	
	public static TerrainTagNextGoingTo terrainTagNextGoingTo;
	public static int countsOfTerrains=0;
	public static int neededCountOfTerrains=22;
	
	public static Vector3 whereToBuild=new Vector3(0,0,1);
	
	public static GameStates gameState;
	
	public static PlayerStates playerStates;
	public static PlayerStatesPathChanging playerStatesPathChanging;
	
	public static float Vsizex=768;
	public static float Vsizey=1024;
	
	public static bool UsingAcceleration=true;
	
	private static float _scaleFactorx = 0;
    public static float scaleFactorx {
        get {
            if (_scaleFactorx == 0) {
                _scaleFactorx = Screen.width/GlobalOptions.Vsizex;
            }
            return _scaleFactorx;
        }
    }
	
	private static float _scaleFactory = 0;
    public static float scaleFactory {
        get {
            if (_scaleFactory == 0) {
                _scaleFactory = Screen.height/GlobalOptions.Vsizey;
            }
            return _scaleFactory;
        }
    }
	
	private static ScreenLoader screenLoader;
	
	private static GameObject player=null;
	private static Player playerScript;
	
	private static GuiLayerInitializer guiLayerInitializer;
	private static MissionEmmitters missionEmmitters;
	
	public static Music MainThemeMusicScript;
	public static float startMusicPitch;
	
	private static GameObject worldFactory=null;
	
	public static Vector3 NormalisePos(Vector3 inpos){
		Vector3 Position=new Vector3(inpos.x*scaleFactorx,inpos.y*scaleFactory,inpos.z);
		return Position;
	}
	
	public static Vector3 NormalisePosRight(Vector3 inpos){
		Vector3 Position=new Vector3(Screen.width-(Vsizex-inpos.x)*scaleFactory,inpos.y*scaleFactory,inpos.z);
		return Position;
	}
	
	public static Vector3 NormaliseScale(){
		Vector3 Scale=new Vector3(1f,1f,1f);
		return Scale;
	}
	
	public static Vector3 TurnLeftRightVector(Vector3 invector, bool toLeft){
		Vector3 result=new Vector3(0,0,0);
		//to left
		
		if(toLeft)
		{
			if(invector.z>0){
				result.x=-1;
			}
			
			if(invector.z<0){
				result.x=1;
			}
			
			if(invector.x>0){
				result.z=1;
			}
			
			if(invector.x<0){
				result.z=-1;
			}
		}
		else
		{
			if(invector.z>0){
				result.x=1;
			}
			
			if(invector.z<0){
				result.x=-1;
			}
			
			if(invector.x>0){
				result.z=-1;
			}
			
			if(invector.x<0){
				result.z=1;
			}
		}
		
		return result;
	}
	
	public static void rotateTransformForWhere(Transform intransform, Vector3 inwhere){
		
		float rotation=0;
		
		if(inwhere.z>0){
			rotation=0;
		}
		
		if(inwhere.z<0){
			rotation=180;
		}
		
		if(inwhere.x>0){
			rotation=90;
		}
		
		if(inwhere.x<0){
			rotation=-90;
		}

		intransform.eulerAngles=new Vector3(0,rotation,0);
	}	
	
	public static Vector3 NormalizeVector3Smex(Vector3 invector,Vector3 normal){
		Vector3 result=new Vector3(0,invector.y,0);
		if(normal.x>0){
			result.z=-invector.x;
			result.x=invector.z;
		}
		if(normal.x<0){
			result.z=invector.x;
			result.x=-invector.z;
		}
		
		if(normal.z>0){
			result.z=invector.z;
			result.x=invector.x;
		}
		if(normal.z<0){
			result.z=-invector.z;
			result.x=-invector.x;
		}
		return result;
	}
	
	public static ScreenLoader GetScreenLoader()
	{
		if(!screenLoader)
		{
			screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
		}
		return screenLoader;
	}
	
	private static GameObject GetPlayer(){
		if(!player)
		{
			player=GameObject.Find("/ScreenGame/Player");
		}
		return player;
	}
	
	public static Player GetPlayerScript(){
		if(!playerScript)
		{
			playerScript=GetPlayer().GetComponent("Player")as Player;
		}
		return playerScript;
	}
	
	public static GameObject GetWorldFactory(){
		if(!worldFactory)
		{
			worldFactory=GameObject.Find("/WorldFactory");
		}
		return worldFactory;
	}
	
	public static GuiLayerInitializer GetGuiLayer(){
		if(!guiLayerInitializer)
		{
			GameObject vsp = GameObject.Find("/ScreenGame/GUILayer");
			if(vsp==null){
				return null;
			}
			guiLayerInitializer=vsp.GetComponent<GuiLayerInitializer>();
		}
		return guiLayerInitializer;
	}
	
	public static MissionEmmitters GetMissionEmmitters(){
		if(!missionEmmitters&&GameObject.Find("/MissionEmmitters"))
		{
			missionEmmitters=GameObject.Find("/MissionEmmitters").GetComponent<MissionEmmitters>();
		}
		return missionEmmitters;
	}
	
	public static int GetCurLevelOpened()
	{
		return PlayerPrefs.GetInt("OpenedLevel",0);
	}
	
	public static void SetCurLevelOpened(int inLevel)
	{
		int curLevelOpened=PlayerPrefs.GetInt("OpenedLevel",0);
		if(curLevelOpened<inLevel){
			PlayerPrefs.SetInt("OpenedLevel",inLevel);
		}
	}
	
	//score
	public static int GetScore()
	{
		return PlayerPrefs.GetInt("Score",0);
	}
	
	//Money
	public static int GetMoney()
	{
		return PlayerPrefs.GetInt("Money",0);
	}
	
	public static void SetMoney(int inScore)
	{
		PlayerPrefs.SetInt("Money",inScore);
	}
	
	
	//Level Start Points
	public static int GetLevelStartPoints()
	{
		return 0;
	}
	
	//Money Start Score
	public static int GetLevelStartMoney()
	{
		return 0;
	}
	
	public static void SetLevelStartMoney(int inScore)
	{
		SetMoney(GetMoney()+inScore);
	}
	
	//ScoreScale
	private static int ScoreScale=1;
	public static int GetScoreScale()
	{
		return ScoreScale;
	}
	
	public static void SetScoreScale(int inScore)
	{
		ScoreScale=inScore;
	}
	
	public static float GetAngleOfRotation(Vector3 p1, Vector3 p2)
	{
		float angle=0;
		
		if(p1.z-p2.z<0&&p1.x-p2.x<=0)
		{
			angle=Mathf.Atan ((p1.x-p2.x)/(p1.z-p2.z));
		}
		else
		if(p1.z-p2.z>=0&&p1.x-p2.x<=0)
		{
			angle=3.141592653589f+Mathf.Atan ((p1.x-p2.x)/(p1.z-p2.z));
		}
		else
		if(p1.z-p2.z<0&&p1.x-p2.x>0)
		{
			angle=3.141592653589f*2+Mathf.Atan ((p1.x-p2.x)/(p1.z-p2.z));
		}
		else
		if(p1.z-p2.z>=0&&p1.x-p2.x>0)
		{
			angle=3.141592653589f+Mathf.Atan ((p1.x-p2.x)/(p1.z-p2.z));
		}
			
		angle*=180/3.141592653589f;
		
		return angle;
	}
	
	public static long GetLongFromDateTime(System.DateTime dateTime){
		System.TimeSpan curTimeSpan = new System.TimeSpan(dateTime.Ticks);
		return long.Parse(string.Format("{0:0}",curTimeSpan.TotalSeconds));
	}
}

