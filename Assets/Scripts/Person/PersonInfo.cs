using UnityEngine;
using System.Collections;

public class PersonInfo {
	private static string TAG = "personal_info_";
	
	public static float personLevel{
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
	
	public static string lastFactoryName{
		get {
            return PlayerPrefs.GetString(TAG+"lastFactory","1_Village");
        }
		set{
			PlayerPrefs.SetString(TAG+"lastFactory",value);
		}
	}
}
