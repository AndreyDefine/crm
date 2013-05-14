using UnityEngine;
using System.Collections;

public class ZoomMap : SpriteTouch {
	
	public float minScale=1, maxScale=2;
	public Vector2 minPos,maxPos;
	
	private int numberOfFingers;
	private int []fingers;
	private Vector2 []fingerPos;
	private Vector2 []initFingerPos;
	
	private float curScale;

	// Use this for initialization
	void Start () {
		swallowTouches=true;
        init();
		
		scale=1;	
		numberOfFingers=0;
		fingers=new int[2];
		fingerPos=new Vector2[2];
		initFingerPos=new Vector2[2];
	}

	public override bool TouchBegan(Vector2 position,int fingerId) {
		if(numberOfFingers>=2){
			return false;
		}
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			fingers[numberOfFingers]=fingerId;
			initFingerPos[numberOfFingers]=position;
			fingerPos[numberOfFingers]=position;
			numberOfFingers++;
		}
		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position,int fingerId) {
		int fingerNumber=0;
		//палец 0
		if(fingerId==fingers[1])
		{
			fingerNumber=1;
		}
		
		fingerPos[fingerNumber]=position;
		
		if(numberOfFingers==1){
			//двигаем карту влево вправо
			Vector2 moveBy=position-initFingerPos[0];
			initFingerPos[0]=position;
			
			moveBy.x/=perPixel;
			moveBy.y/=perPixel;
			
			Vector3 newPos=new Vector3(singleTransform.localPosition.x+moveBy.x,singleTransform.localPosition.y+moveBy.y,singleTransform.localPosition.z);
			
			newPos.x=newPos.x>maxPos.x?maxPos.x:newPos.x;
			newPos.y=newPos.y>maxPos.y?maxPos.y:newPos.y;
			
			newPos.x=newPos.x<minPos.x?minPos.x:newPos.x;
			newPos.y=newPos.y<minPos.y?minPos.y:newPos.y;
			
			singleTransform.localPosition=newPos;
		}
		
		//scale
		if(numberOfFingers==2)
		{
			float rasst,initrasst;
			float newscale;
			
			Vector2 gipinit,gip;
				
			//считаем расстояние
			gipinit=new Vector2(initFingerPos[0].x-initFingerPos[1].x,initFingerPos[0].y-initFingerPos[1].y);
			gip=new Vector2(fingerPos[1].x-fingerPos[0].x,fingerPos[1].y-fingerPos[0].y);
			
			Vector2 moveBy=new Vector2(fingerPos[0].x+gip.x/2-singleTransform.localPosition.x,fingerPos[0].y+gip.y/2-singleTransform.localPosition.y);
			Vector2 moveByOld=new Vector2(moveBy.x*singleTransform.localScale.x,moveBy.y*singleTransform.localScale.y);
			
			initrasst=Mathf.Sqrt(gipinit.x*gipinit.x+gipinit.y*gipinit.y);
			rasst=Mathf.Sqrt(gip.x*gip.x+gip.y*gip.y);
			
			newscale=(rasst-initrasst)/Screen.width;
			
			curScale=scale+newscale;
			
			
			//scale
			curScale=curScale>maxScale?maxScale:curScale;
			curScale=curScale<minScale?minScale:curScale;
		
			singleTransform.localScale=new Vector3(curScale,curScale,1);
			
			Vector2 moveByNew=new Vector2(moveBy.x*curScale,moveBy.y*curScale);
			
			moveBy =moveByOld-moveByNew;
			moveBy/=perPixel;
			//position
			Vector3 newPos=new Vector3(singleTransform.localPosition.x+moveBy.x,singleTransform.localPosition.y+moveBy.y,singleTransform.localPosition.z);
			singleTransform.localPosition=newPos;
			Debug.Log ("moveByx="+moveBy.x+" moveByy="+moveBy.y+" moveByOld="+moveByOld+" moveByNew="+moveByNew+" curScale="+singleTransform.localScale);
			
		}
	}
	
	public override void TouchEnded(Vector2 position,int fingerId) {
		if(numberOfFingers==1)
		{
			 //return;
		}
		base.TouchEnded(position,fingerId);
		if(fingerId==fingers[0]&&numberOfFingers==2)
		{
			fingers[0]=fingers[1];
			initFingerPos[0]=fingerPos[1];
			fingerPos[0]=fingerPos[1];
		}
		numberOfFingers--;
		scale=curScale;
	}
}
