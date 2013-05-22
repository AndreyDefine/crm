using UnityEngine;
using System.Collections;

public class Factory : SpriteTouch {
	
	private Vector3 initScale;
	public DialogFerma dialogFermaPrefab;
	public float smX = 0f;
	public float smY = 0.5f;
	
	protected override void Start(){
		base.Start();
		initScale = singleTransform.localScale;
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
		DialogFerma dialogFerma = Instantiate(dialogFermaPrefab) as DialogFerma;
		dialogFerma.singleTransform.parent = singleTransform;
		dialogFerma.ShowForPosition(new Vector3(singleTransform.position.x+smX, singleTransform.position.y+smY, singleTransform.position.z-0.01f));
	}
}
