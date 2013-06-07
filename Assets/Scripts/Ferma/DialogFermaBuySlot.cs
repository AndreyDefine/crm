using UnityEngine;
using System.Collections;

public class DialogFermaBuySlot : SpriteTouch {
	
	public CrmFont priceValue;
	public CrmFont goldValue;
	private Slot slot;
	
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
	
	public void SetSlotToBuy (Slot slot)
	{
		this.slot = slot;
		priceValue.text = string.Format("{0}",slot.coin);
		goldValue.text = string.Format("{0}",slot.gold);
	}
	
	public Slot GetSlotToBuy(){
		return slot;
	}
	
	public void Show(){	
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
		fermaMission.BuySlot();
	}
	
	public void ScaleOutEnd(){
		//GetFermaLocationPlace().DialogClosed();
		Destroy(gameObject);
	}
}
