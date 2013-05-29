using UnityEngine;
using System.Collections;

public class FermaMissions : Abstract {
	
	public FermaMission[] fermaMissions;
	private FermaLocationPlace place;
	
	void Start(){
		for(int i=0;i<3;i++){
			fermaMissions[i].SetFermaMissions(this);
		}
	}
	
	public FermaLocationPlace GetFermaLocationPlace(){
		return place;
	}
	
	public void UpdateData(){
		ArrayList availableMissionsPrefabs = place.missionEmmitter.GetAvailableMissionsPrefabs();
		ArrayList missionsToBuyPrefabs = place.missionEmmitter.GetAvailableNotBoughtMissionsPrefabs();
		
		Debug.LogWarning(availableMissionsPrefabs.Count);
		Debug.LogWarning(missionsToBuyPrefabs.Count);
		
		int i=0;
		for(;i<availableMissionsPrefabs.Count;i++){
			fermaMissions[i].SetMission(availableMissionsPrefabs[i] as Mission);
		}
		if(i<3){
			if(missionsToBuyPrefabs.Count>0){
				fermaMissions[i].SetActive(true);
				i++;
			}
			for(int j=i;j<3;j++){
				fermaMissions[j].SetActive(false);		
			}
		}
	}
	
	public void SetFermaLocationPlace (FermaLocationPlace place)
	{	
		this.place = place;
		UpdateData();
	}
}
