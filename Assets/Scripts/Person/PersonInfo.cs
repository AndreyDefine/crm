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
            return PlayerPrefs.GetInt(TAG+"coins",1000);
        }
    }
	
	public static void AddCoins(int addCoins){
		PlayerPrefs.SetInt(TAG+"coins",coins+addCoins);
	}
	
	public static string lastFactoryName{
		get {
            return PlayerPrefs.GetString(TAG+"lastFactory","Post");
        }
		set{
			PlayerPrefs.SetString(TAG+"lastFactory",value);
		}
	}
}
