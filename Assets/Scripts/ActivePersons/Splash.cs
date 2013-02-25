using UnityEngine;
using System.Collections;

public class Splash : Abstract {
	
	public float timeToSplash;
	private float curTime;
	
	public GameObject MakeSplash;

	// Use this for initialization
	void Start () {
		curTime=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time-curTime>timeToSplash){
			MakeSplash.active=!MakeSplash.active;
			curTime=Time.time;
		}
	}
}
