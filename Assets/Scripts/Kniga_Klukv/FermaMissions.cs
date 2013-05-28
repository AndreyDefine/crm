using UnityEngine;
using System.Collections;

public class FermaMissions : Abstract {
	
	public FermaMission [] fermaMissions;
	public int emmitPeriod = 60*15;
	
	public void SetFermaLocationPlace (FermaLocationPlace place)
	{
		if(System.DateTime.UtcNow.ToBinary()-place.lastMissionEmmitTime>emmitPeriod){
			//Debug.LogError(System.DateTime.UtcNow.ToBinary()-place.lastMissionEmmitTime);
		}
		fermaMissions[0].SetActive(true);
		fermaMissions[1].SetActive(false);
		fermaMissions[2].SetActive(false);
	}
}
