using UnityEngine;
using System.Collections;

public class ZoomMapKnigaKlukv : ZoomMap {	
	private KnigaKlukvControllerScript knigaKlukvControllerScript; 
	
	void GetKnigaKlukvControllerScript()
	{
		knigaKlukvControllerScript=(GameObject.Find("/ScreenKniga/KnigaKlukvController") as GameObject).GetComponent("KnigaKlukvControllerScript") as KnigaKlukvControllerScript;
	}
	
	public void HideCurKnigaItem()
	{
		if(!knigaKlukvControllerScript){
			GetKnigaKlukvControllerScript();
		}
		
		knigaKlukvControllerScript.HideCurKnigaItem();
	}
	
	public override void TouchMoved(Vector2 position,int fingerId) {
		HideCurKnigaItem();
		
		base.TouchMoved(position,fingerId);
	}
}
