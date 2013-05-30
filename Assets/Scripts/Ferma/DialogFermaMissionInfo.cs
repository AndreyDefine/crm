using UnityEngine;
using System.Collections;

public class DialogFermaMissionInfo: SpriteTouch {
	
	public CrmFont priceValue;
	public CrmFont goldValue;
	public TwoLineText missionName;
	
	protected override void InitTouchZone ()
	{
		touchZone = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	public void SetMission (Mission mission)
	{
		priceValue.text = string.Format("{0}",mission.coinAward);
		goldValue.text = string.Format("{0}",mission.goldAward);
		missionName.text = mission.missionName;
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
	
	public void Cancel(){
		CloseDialog();
	}
	
	public void Submit(){
		CloseDialog();
	}
	
	public void ScaleOutEnd(){
		Destroy(gameObject);
	}
}
