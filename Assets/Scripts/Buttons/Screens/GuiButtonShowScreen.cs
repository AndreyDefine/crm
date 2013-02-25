using UnityEngine;
using System.Collections;

public class GuiButtonShowScreen : SpriteTouch {
	public string screenToShow="";
	
	protected ScreenLoader screenLoader;
	
	private void Start() {
        init();
		screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
	}
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			MakeOnTouch();
		}

		return isTouchHandled;
	}
	
	virtual protected void MakeOnTouch(){
		screenLoader.LoadScreenByName(screenToShow);
	}
	
}
