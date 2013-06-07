using UnityEngine;
using System.Collections;

public enum FermaMissionStates{CLOSED, OPENED, FORBUY}

public class FermaMission : GuiButtonBase {
	
	private FermaMissionStates state = FermaMissionStates.CLOSED;
	private Slot slot;
	
	
	public tk2dSprite plus;
	public CrmFont emmitTime;
	public Abstract missionIcoPlace;
	private bool hasMission = true;
	public DialogFermaBuySlot dialogFermaBuySlotPrefab;
	DialogFermaBuySlot dialog = null;
	
	public DialogFermaMissionInfo dialogFermaMissionInfo;
	DialogFermaMissionInfo dialogInfo = null;
	MissionIco missionIco = null;
	
	Mission mission;
	
	public void SetClosed ()
	{
		RemoveIcon();
		state = FermaMissionStates.CLOSED;
		getTouches = false;
		GetComponent<tk2dSprite>().color = new Color(0.5f,0.5f,0.5f,1f);
		emmitTime.gameObject.SetActive(false);
		plus.gameObject.SetActive(false);
	}
	
	public void SetOpened ()
	{
		hasMission = false;
		RemoveIcon();
		state = FermaMissionStates.OPENED;
		getTouches = true;
		GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		emmitTime.gameObject.SetActive(false);
		plus.gameObject.SetActive(false);
	}
	
	public void SetForBuy(){
		RemoveIcon();
		state = FermaMissionStates.FORBUY;
		getTouches = true;
		GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		emmitTime.gameObject.SetActive(false);
		plus.gameObject.SetActive(true);
	}
	
	public void SetSlot(Slot slot){
		this.slot = slot;
	}
	
	public void SetMission(Mission mission){
		this.mission = mission;
		hasMission = true;
		emmitTime.gameObject.SetActive(false);
		GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		plus.gameObject.SetActive(false);
		missionIco = Instantiate(mission.iconPrefab) as MissionIco;
		missionIco.singleTransform.parent = missionIcoPlace.singleTransform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.1f);
	}
	
	private void RemoveIcon(){
		if(missionIco!=null){
			Destroy(missionIco.gameObject);
			missionIco = null;
		}
	}
	
	void Update(){
		if(state==FermaMissionStates.OPENED){
			if(!hasMission){
				emmitTime.gameObject.SetActive(true);
				long secondsLeft = GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().GetNextEmmitInSeconds();
				emmitTime.text = string.Format("{0:00}:{1:00}",secondsLeft/60,secondsLeft%60);
			}
			//SetMission ((Mission)GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().GetCurrentMissions()[0]);
		}
			
			//Debug.LogWarning(curTime-fermaMissions.GetFermaLocationPlace().missionEmmitter.lastMissionEmmitTime);
			/*long secondsRun = curTime-fermaMissions.GetFermaLocationPlace().missionEmmitter.lastMissionEmmitTime;
			if(secondsRun>emmitPeriod){
				emmitTime.gameObject.SetActiveRecursively(false);
				getTouches = true;
				plus.gameObject.active = true;
			}else{
				long secondsLeft = emmitPeriod-secondsRun;
				emmitTime.gameObject.SetActiveRecursively(true);
				emmitTime.text = string.Format("{0:00}:{1:00}",secondsLeft/60,secondsLeft%60);
				getTouches = false;
				plus.gameObject.active = false;
			}*/
	}
	
	protected override void MakeOnTouch ()
	{
		if(state==FermaMissionStates.FORBUY){
			dialog = Instantiate(dialogFermaBuySlotPrefab) as DialogFermaBuySlot;
			//ArrayList missionsToBuy = fermaMissions.GetFermaLocationPlace().missionEmmitter.GetAvailableNotBoughtMissionsPrefabs();
			
			dialog.SetFermaMisision(this);
			dialog.SetSlotToBuy(slot);
			dialog.singleTransform.parent = singleTransform;
			dialog.singleTransform.localPosition = new Vector3(0f,0f,-0.5f);
			dialog.singleTransform.position = new Vector3(0f,0f,dialog.singleTransform.position.z);
			dialog.Show();
		}
		if(state == FermaMissionStates.OPENED&&hasMission){
			dialogInfo = Instantiate(dialogFermaMissionInfo) as DialogFermaMissionInfo;
			//ArrayList missionsToBuy = fermaMissions.GetFermaLocationPlace().missionEmmitter.GetAvailableNotBoughtMissionsPrefabs();
			dialogInfo.SetMission(mission);
			dialogInfo.singleTransform.parent = singleTransform;
			dialogInfo.singleTransform.localPosition = new Vector3(0f,0f,-0.5f);
			dialogInfo.singleTransform.position = new Vector3(0f,0f,dialogInfo.singleTransform.position.z);
			dialogInfo.Show();
		}
	}
	
	public void BuySlot(){
		slot.BuySlot();
		GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().EmmitMissions(true);
	}
}
