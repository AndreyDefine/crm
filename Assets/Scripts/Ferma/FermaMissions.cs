using UnityEngine;
using System.Collections;

public class FermaMissions : Abstract, IFermaMissionEmmitterListener {
	
	public FermaMission[] fermaMissions;
	private FermaLocationPlace place;
	private FermaMissionEmmitter fermaMissionEmmitter;
	
	void Start(){
		for(int i=0;i<3;i++){
			fermaMissions[i].SetFermaMissions(this);
		}
	}
	
	public FermaLocationPlace GetFermaLocationPlace(){
		return place;
	}
	
	public void UpdateData(){
		Slot[] slots = fermaMissionEmmitter.slots;
		bool buy = false;
		for(int i=0;i<slots.Length;i++){
			Slot slot = slots[i];
			fermaMissions[i].SetSlot(slot);
			if(slot.bought){
				fermaMissions[i].SetOpened();
				if(i==0){
					ArrayList currentMissions = fermaMissionEmmitter.GetCurrentMissions();
					if(currentMissions.Count>0){
						fermaMissions[i].SetMission(currentMissions[0] as Mission);
					}
				}else{
					ArrayList emmittedMissions = fermaMissionEmmitter.GetEmmittedMissionsPrefabs();
					if(emmittedMissions.Count>i-1){
						fermaMissions[i].SetMission(emmittedMissions[i-1] as Mission);
					}
				}
			}else{
				if(!buy){
					buy=true;
					fermaMissions[i].SetForBuy();
				}else{
					fermaMissions[i].SetClosed();
				}
			}
		}
	}
	
	public void SetFermaLocationPlace (FermaLocationPlace place)
	{	
		this.place = place;
		fermaMissionEmmitter = GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter();
		fermaMissionEmmitter.AddFermaMissionEmmitterListener(this);
		UpdateData();
	}
	
	void OnDestroy(){
		fermaMissionEmmitter.RemoveFermaMissionEmmitterListener(this);
	}
	
	public void MissionsUpdated (FermaMissionEmmitter fermaMissionEmmitter)
	{
		UpdateData();
	}
}
