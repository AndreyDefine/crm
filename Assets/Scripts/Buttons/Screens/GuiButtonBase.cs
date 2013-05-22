using UnityEngine;
using System.Collections;

public class GuiButtonBase : SpriteTouch {
	private Vector3 initScale;
	
	protected virtual void Start() {
        init();
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

	}
	
}
