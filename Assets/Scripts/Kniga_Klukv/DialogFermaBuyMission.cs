using UnityEngine;
using System.Collections;

public class DialogFermaBuyMission : SpriteTouch {
	
	public CrmFont priceValue;
	public CrmFont goldValue;
	public CrmFont rewardCoinsValue;
	public CrmFont rewardGoldValue;
	public TwoLineText missionName;
	public GameObject buyButton;
	private Mission mission;
	
	private FermaMission fermaMission;
	
	public void SetFermaMisision(FermaMission fermaMission){
		this.fermaMission = fermaMission;
	}
	
	public FermaMission GetFermaMission(){
		return fermaMission;
	}
	
	protected override void InitTouchZone ()
	{
		touchZone = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	public void SetMissionToBuy (Mission mission)
	{
		this.mission = mission;
		missionName.text = mission.missionName;
		priceValue.text = string.Format("{0}",mission.coinPrice);
		goldValue.text = string.Format("{0}",mission.goldPrice);
		rewardCoinsValue.text = string.Format("{0}",mission.coinAward);
		rewardGoldValue.text = string.Format("{0}",mission.goldAward);
	}
	
	public Mission GetMissionToBuy(){
		return mission;
	}
	
	public void Show(){	
		singleTransform.localScale = new Vector3(0f,0f,0f);
		AnimationFactory.ScaleInXYZ(this,new Vector3(1f,1f,1f),0.5f,"scaleIn","ScaleInEnd");
	}
	
	public void ScaleInEnd(){
		float radius = 0.005f;
		AnimationFactory.MoveRound(this,2.5f,0.015f,"MoveRound");
	}
		
	public void CloseDialog(){
		AnimationFactory.ScaleOutXYZ(this,new Vector3(0f,0f,0f),0.5f,"ScaleOut", "ScaleOutEnd");
	}	
	
	public void ScaleOutEnd(){
		//GetFermaLocationPlace().DialogClosed();
		Destroy(gameObject);
	}
}
