using UnityEngine;
using System.Collections;

public class MissionEmmitters : Abstract, IMissionEmmitter
{
	public SimpleMissionEmmitter[] missionEmmitters;//TODO переделать потом по-другому
	
	void Awake ()
	{
		PlayerPrefs.DeleteAll();Debug.LogWarning("PlayerPrefs.DeleteAll()");//TODO: delete this
	}
	
	
	public void NotifyCoinsCollected(int coins){
		for(int i=0;i<missionEmmitters.Length;i++){
			missionEmmitters[i].NotifyCoinsCollected(coins);
		}
	}
	
	public ArrayList GetCurrentMissions(){
		ArrayList currentMissions = new ArrayList();
		for(int i=0;i<missionEmmitters.Length;i++){
			currentMissions.AddRange(missionEmmitters[i].GetCurrentMissions());
		}
		return currentMissions;
	}
	
	public void NotifyMetersRunned(int meter){
		for(int i=0;i<missionEmmitters.Length;i++){
			missionEmmitters[i].NotifyMetersRunned(meter);
		}
	}
}
