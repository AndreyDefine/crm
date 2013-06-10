using UnityEngine;
using System.Collections;

public class PersonInfo {
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
            return PlayerPrefs.GetInt(TAG+"coins",2000);
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
            return PlayerPrefs.GetInt(TAG+"gold",1);
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
            return PlayerPrefs.GetInt(TAG+"post",0);
        }
    }
	
	public static void AddPost(int addPost){
		PlayerPrefs.SetInt(TAG+"post",post+addPost);
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
