using UnityEngine;
using System.Collections;

public class PersonInfo {
	//кэш
	private static int _post=-100;
	
	private static string TAG = "personal_info_";
	
	public static int personLevel{
		get {
            return GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions()/3+1;
        }
	}
	
	public static float currentLevelProgress{
		get {
            return (GlobalOptions.GetMissionEmmitters().GetCountFinishedMissions()-(personLevel-1)*3)/3f;
        }
	}
	
	public static int coins {
        get {
            return PlayerPrefs.GetInt(TAG+"coins",20000);
        }
    }
	
	public static void AddCoins(int addCoins){
		PlayerPrefs.SetInt(TAG+"coins",coins+addCoins);
		GlobalMoney[] globalMoney = GameObject.FindObjectsOfType(typeof(GlobalMoney)) as GlobalMoney[];
		for(int i=0;i<globalMoney.Length;i++){
			globalMoney[i].SetMoney(coins);	
		}
	}
	
	public static int gold {
        get {
            return PlayerPrefs.GetInt(TAG+"gold",10);
        }
    }
	
	public static void AddGold(int addGold){
		PlayerPrefs.SetInt(TAG+"gold",gold+addGold);
		GlobalGold[] globalGold = GameObject.FindObjectsOfType(typeof(GlobalGold)) as GlobalGold[];
		for(int i=0;i<globalGold.Length;i++){
			globalGold[i].SetGold(gold);	
		}
	}
	
	public static int post {
        get {
			
			if(_post<0){
            _post=PlayerPrefs.GetInt(TAG+"post",0);}
			return _post;
        }
    }
	
	public static void AddPost(int addPost){
		_post=post+addPost;
		PlayerPrefs.SetInt(TAG+"post",_post);
	}
	
	public static string lastFactoryName{
		get {
            return PlayerPrefs.GetString(TAG+"lastFactory","1_Village");
        }
		set{
			PlayerPrefs.SetString(TAG+"lastFactory",value);
		}
	}
	
	public static bool tutorial {
        get {
            return PlayerPrefs.GetInt(TAG+"tutorial",0)==0?true:false;
        }
    }
	
	public static void FinishTutorial(){
		PlayerPrefs.SetInt(TAG+"tutorial",1);
		MissionEmmitters missionEmmitters = GlobalOptions.GetMissionEmmitters();
		if(missionEmmitters!=null){
			missionEmmitters.Init();
		}
		GuiLayerInitializer guiLayer = GlobalOptions.GetGuiLayer();
		if(guiLayer!=null){
			guiLayer.upNotifierController.AddTutorialFinihsedNotifier();
			guiLayer.upNotifierController.SetCurrentMissions();
		}
	}
	
	public static bool TryToBuy(int needCoins, int needGold){
		int needMoreCoins = needCoins-coins;
		int needMoreGold = needGold - gold;
		if(needMoreCoins>0||needMoreGold>0){
			GameObject dialogGoToCartGameObj = GameObject.Instantiate(Resources.Load("Screens/DialogGoToCart")) as GameObject;
			DialogGoToCart dialogGoToCart = dialogGoToCartGameObj.GetComponent<DialogGoToCart>();
			dialogGoToCart.SetNeedMoneyGold(needMoreCoins>0?needMoreCoins:0,needMoreGold>0?needMoreGold:0);
			dialogGoToCart.Show();
			return false;
		}
		AddCoins(-needCoins);
		AddGold(-needGold);
		return true;
	}
	
}
