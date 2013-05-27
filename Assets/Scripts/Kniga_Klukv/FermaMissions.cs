using UnityEngine;
using System.Collections;

public class FermaMissions : Abstract {
	
	public GuiFermaMission guiFermaMissinPrefab;
	
	public void SetFermaLocationPlace (FermaLocationPlace place)
	{
		for(int i=0;i<place.missions.Length;i++){
			//GuiFermaMission guiMission = Instantiate(guiFermaMissinPrefab) as GuiFermaMission;
			//guiMission.singleTransform.parent = singleTransform;
			//guiMission.singleTransform.localPosition = new Vector3(0f,-i*0.3f,-0.01f);
			//guiMission.SetMission(place.missions[i]);
		}
	}
}
