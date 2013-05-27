using UnityEngine;
using System.Collections;

public class Factory : SpriteTouch {
	
	public FactoryBuilding factoryBuilding;
	bool flagMapWasMoving = false;
	private Vector3 initScale;
	private FermaLocationPlace fermaLocationPlace;
	
	
	public void SetFermaLocationPlace(FermaLocationPlace fermaLocationPlace){
		this.fermaLocationPlace = fermaLocationPlace;
	}
	
	public void SetActive(bool a){
		factoryBuilding.SetActive(a);	
		getTouches = a;
	}
	
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
		if(ZoomMap.instance.MapIsMovingTwoFingers()){
			flagMapWasMoving = true;
			return;
		}
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
		base.TouchEnded (position, fingerId);
		if(flagMapWasMoving==true){
			flagMapWasMoving = false;
			return;
		}
		singleTransform.localScale = initScale;
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			MakeOnTouch();
		}
	}
	
	virtual protected void MakeOnTouch(){
		fermaLocationPlace.ShowPlayDialog();
	}
}
