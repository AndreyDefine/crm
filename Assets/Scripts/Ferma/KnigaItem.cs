using UnityEngine;
using System.Collections;

public class KnigaItem : SpriteTouch {
	public GameObject itemToShow;
	public GameObject itemShining;
	private KnigaKlukvControllerScript knigaKlukvControllerScript;
	
	private GameObject curItemToShow=null;
	// Use this for initialization
	protected override void Start () {
		base.Start();
		GetKnigaKlukvControllerScript();
		swallowTouches=true;
        init();
	}
	
	private void GetKnigaKlukvControllerScript(){
		knigaKlukvControllerScript=(GameObject.Find("/ScreenKniga/KnigaKlukvController") as GameObject).GetComponent("KnigaKlukvControllerScript") as KnigaKlukvControllerScript;
	}
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			if(!curItemToShow){
				ShowItem();
			}
			else
			{
				HideItem();
			}
		}
		return isTouchHandled;
	}

	public void ShowItem(){
		knigaKlukvControllerScript.HideCurKnigaItem();
		curItemToShow=Instantiate(itemToShow) as GameObject;
		if(itemShining){
			itemShining.SetActive(true);
		}
		knigaKlukvControllerScript.curKnigaItem=this;
	}
	
	public void HideItem(){
		if(curItemToShow)
		{
			alreadyTouched=false;
			(curItemToShow.GetComponent("SpriteTouch") as SpriteTouch).removeFromDispatcher();
			curItemToShow.SetActive(false);
			if(itemShining){
				itemShining.SetActive(false);
			}
			Destroy(curItemToShow);
			curItemToShow=null;
			knigaKlukvControllerScript.curKnigaItem=null;
		}
	}
}
