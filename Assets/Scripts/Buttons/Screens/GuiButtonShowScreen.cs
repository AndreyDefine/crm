using UnityEngine;
using System.Collections;

public class GuiButtonShowScreen : SpriteTouch {
	public string screenToShow="";
	public Vector3 initScale;
	
	protected ScreenLoader screenLoader;
	
	private void Start() {
        init();
		screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
	}
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			initScale = singleTransform.localScale;
			singleTransform.localScale = initScale*1.05f;
		}

		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position, int fingerId)
	{
		base.TouchMoved (position, fingerId);
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			singleTransform.localScale = initScale*1.05f;
		}
		else
		{
			singleTransform.localScale = initScale;
		}
	}
	
	public override void TouchEnded (Vector2 position, int fingerId)
	{
		singleTransform.localScale = initScale;
		base.TouchEnded (position, fingerId);
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			MakeOnTouch();
		}
	}
	
	virtual protected void MakeOnTouch(){
		if(screenToShow=="ScreenShop"||screenToShow=="ScreenSettings"||screenToShow=="ScreenMap"||screenToShow=="ScreenKniga")
		{
			FlurryPlugin.FlurryLogEvent(screenToShow);
		}
		screenLoader.LoadScreenByName(screenToShow);
	}
	
}
