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
		for(int i=0;i<place.missions.Length;i++){
			//GuiFermaMission guiMission = Instantiate(guiFermaMissinPrefab) as GuiFermaMission;
			//guiMission.singleTransform.parent = singleTransform;
			//guiMission.singleTransform.localPosition = new Vector3(0f,-i*0.3f,-0.01f);
			//guiMission.SetMission(place.missions[i]);
		}
	}
}
