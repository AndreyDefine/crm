using UnityEngine;
using System.Collections;

public class DialogGoToCart: SpriteTouch {
	
	public CrmFont needMoney;
	public CrmFont needGold;
	
	protected override void InitTouchZone ()
	{
		touchZone = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	public void SetNeedMoneyGold (int money, int gold)
	{
		needMoney.text = string.Format("{0}",money);
		needGold.text = string.Format("{0}",gold);
	}
	
	public void Show(){	
		gameObject.SetActive(true);
		singleTransform.localPosition = new Vector3(0f,0f,-0.5f);
		singleTransform.localScale = new Vector3(0f,0f,0f);
		AnimationFactory.ScaleInXYZ(this,new Vector3(1f,1f,1f),0.5f,"scaleIn","ScaleInEnd");
	}
	
	public void ScaleInEnd(){
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
