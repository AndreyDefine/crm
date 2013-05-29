using UnityEngine;
using System.Collections;

public class FermaMission : GuiButtonBase {
	
	public long emmitPeriod = 15;
	public tk2dSprite plus;
	public CrmFont emmitTime;
	public Abstract missionIcoPlace;
	private bool isActive = false;
	public DialogFermaBuyMission dialogFermaBuyMissionPrefab;
	DialogFermaBuyMission dialog = null;
	
	private FermaMissions fermaMissions;
	
	public void SetFermaMissions(FermaMissions fermaMissions){
		this.fermaMissions = fermaMissions;
	}
	
	public void SetActive (bool b)
	{
		isActive = b;
		if(b){
			GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		}else{
			emmitTime.gameObject.SetActiveRecursively(false);
			GetComponent<tk2dSprite>().color = new Color(0.5f,0.5f,0.5f,1f);
			plus.gameObject.active = false;
		}
	}
	
	public void SetMission(Mission mission){
		isActive = false;
		emmitTime.gameObject.SetActiveRecursively(false);
		GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		plus.gameObject.active = false;
		MissionIco missionIco = Instantiate(mission.iconPrefab) as MissionIco;
		missionIco.singleTransform.parent = missionIcoPlace.singleTransform;
		missionIco.singleTransform.localPosition = new Vector3(0f,0f,-0.1f);
	}
	
	void Update(){
		if(isActive){
			long curTime = GlobalOptions.GetLongFromDateTime(System.DateTime.UtcNow);
			//Debug.LogWarning(curTime-fermaMissions.GetFermaLocationPlace().missionEmmitter.lastMissionEmmitTime);
			long secondsRun = curTime-fermaMissions.GetFermaLocationPlace().missionEmmitter.lastMissionEmmitTime;
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
			}
		}
	}
	
	protected override void MakeOnTouch ()
	{
		if(!isActive){
			return;	
		}
		dialog = Instantiate(dialogFermaBuyMissionPrefab) as DialogFermaBuyMission;
		ArrayList missionsToBuy = fermaMissions.GetFermaLocationPlace().missionEmmitter.GetAvailableNotBoughtMissionsPrefabs();
		
		dialog.SetFermaMisision(this);
		dialog.SetMissionToBuy(missionsToBuy[0] as Mission);
		dialog.singleTransform.parent = singleTransform;
		dialog.singleTransform.localPosition = new Vector3(0f,0f,-0.5f);
		dialog.singleTransform.position = new Vector3(0f,0f,dialog.singleTransform.position.z);
		dialog.Show();
	}
	
	public void Cancel(){
		dialog.CloseDialog();
	}
	
	public void Subbmit(){
		fermaMissions.GetFermaLocationPlace().missionEmmitter.BuyMission(dialog.GetMissionToBuy());
		fermaMissions.UpdateData();
		dialog.CloseDialog();
	}
}
