using UnityEngine;
using System.Collections;

public class MissionEmmiter : MonoBehaviour {
	
	public GameObject []MissionTags;
	private MissionStack missionStack;
	float curTime;

	// Use this for initialization
	void Start () {
		curTime=Time.time;
		missionStack=MissionStack.sharedMissionStack();
	}
	
	// Update is called once per frame
	void Update () {
		AddOneMissionObject();
	}
	
	private void AddOneMissionObject()
	{
		if(Time.time-curTime>40)
		{
			curTime=Time.time;
			int randomIndex=Random.Range(0,MissionTags.Length);
			MissionTag missionTag=(Instantiate (MissionTags[randomIndex]) as GameObject).GetComponent<MissionTag>();
			missionStack.Push(missionTag);
			
			Debug.Log (missionTag.mainName+" - "+missionTag.Description);
		}
	}
}
