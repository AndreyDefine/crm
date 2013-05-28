using UnityEngine;
using System.Collections;

public class ZoomMap : SpriteTouch {
	
	public float maxScale;
	private float minScale;
	private Vector3 posLeftTop;
	private Vector3 posRightBottom;
	
	private ArrayList particleChangeScaleList=new ArrayList();
	
	private int numberOfFingers;
	private int []fingers;
	private Vector2 []fingerPos;
	private Vector2 []initFingerPos;
	
	private Vector2 []prevFingerPos = new Vector2[2];
	float prevScale = 1f;
	Vector3 prevPos;
	
	//private Vector2 previouseCenterPos = Vector2.zero;
	//private float previousScale = 0;
	public static ZoomMap instance = null;
	protected override void InitTouchZone ()
	{
		touchZone = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	public void AddToParticleChangeScaleList(ParticleChangeScale inobject)
	{
		particleChangeScaleList.Add(inobject);
	}
	
	private void ChangeParticleScales()
	{
		for (int i=0;i<particleChangeScaleList.Count;i++)
		{
			(particleChangeScaleList[i]as ParticleChangeScale).ChangeScale(singleTransform.localScale.x);
		}
	}
	
	private float curScale;
	
	public bool MapIsMovingTwoFingers(){
		return numberOfFingers == 2;
	}
	
	// Use this for initialization
	void Start () {
		ZoomMap.instance = this;
		swallowTouches=true;
        init();
		
		scale=1;	
		numberOfFingers=0;
		fingers=new int[2];
		fingerPos=new Vector2[2];
		initFingerPos=new Vector2[2];
		
		float minScaleX = Screen.width/2/perPixel/singleRenderer.bounds.extents.x;
		float minScaleY = Screen.height/2/perPixel/singleRenderer.bounds.extents.y;
		minScale = minScaleX<minScaleY?minScaleY:minScaleX;
		if(singleTransform.localScale.x>minScale){
			singleTransform.localScale = new Vector3(minScale, minScale, minScale);
		}
		
		int padding = -1;
		posLeftTop=new Vector3(padding,GlobalOptions.Vsizey-padding,1f);
		posLeftTop=GlobalOptions.NormalisePos(posLeftTop);
		posLeftTop=Cameras.GetGUICamera().ScreenToWorldPoint(posLeftTop);
	
		posRightBottom=new Vector3(GlobalOptions.Vsizex-padding,padding,1f);
		posRightBottom=GlobalOptions.NormalisePos(posRightBottom);
		posRightBottom=Cameras.GetGUICamera().ScreenToWorldPoint(posRightBottom);		
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
			if(numberOfFingers==2){
				prevScale = singleTransform.localScale.x;		
				prevPos = singleTransform.localPosition;
				prevFingerPos[0] = initFingerPos[0];
				prevFingerPos[1] = initFingerPos[1];
			}
		}
		return isTouchHandled;
	}
	
	public void SetPos(Vector3 pos){
		singleTransform.position = Norm(pos);
	}
	
	public Vector3 Norm(Vector3 pos){
		Vector2 extends = new Vector2(singleRenderer.bounds.extents.x,singleRenderer.bounds.extents.y);
		bool topMax = false;
		bool rightMax = false;
		//right	
		if(pos.x+extends.x<posRightBottom.x){
			pos.x = posRightBottom.x-extends.x;
			rightMax = true;
		}
		//left
		if(pos.x-extends.x>posLeftTop.x){
			if(rightMax){
				pos.x = 0f;
			}else
			{
				pos.x = posLeftTop.x+extends.x;
			}
		}
		//top
		if(pos.y+extends.y<posLeftTop.y){
			pos.y = posLeftTop.y-extends.y;
			topMax = true;
		}
		//bottom
		if(pos.y-extends.y>posRightBottom.y){
			if(topMax){
				pos.y = 0f;
			}else{
				pos.y = posRightBottom.y+extends.y;
			}
		}
		return pos;
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
			
			Vector3 pos=new Vector3(singleTransform.localPosition.x+moveBy.x,singleTransform.localPosition.y+moveBy.y,singleTransform.localPosition.z);
			singleTransform.localPosition=Norm(pos);
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
			ChangeParticleScales();
			
			
			Vector2 moveByNew=new Vector2(moveBy.x*curScale,moveBy.y*curScale);
			
			moveBy =moveByOld-moveByNew;
			moveBy/=perPixel;
			//position		
			
			
			Vector2 centerFingerPrev = new Vector2(prevFingerPos[0].x-(prevFingerPos[0].x-prevFingerPos[1].x)/2, prevFingerPos[0].y-(prevFingerPos[0].y-prevFingerPos[1].y)/2);
			Vector2 centerFingerCurrent = new Vector2(fingerPos[0].x-(fingerPos[0].x-fingerPos[1].x)/2, fingerPos[0].y-(fingerPos[0].y-fingerPos[1].y)/2);
			
			//Debug.Log(centerFingerPrev.x+ " "+ centerFingerPrev.y+ " "+ centerFingerCurrent.x+ " "+ centerFingerCurrent.y);
			
			
			Camera guiCamera = Cameras.GetGUICamera();
			
			Vector3 wordCenterFingerCurrent = guiCamera.ScreenToWorldPoint(new Vector3(centerFingerCurrent.x, centerFingerCurrent.y,1f));
			Vector2 smCurrent = curScale*wordCenterFingerCurrent;
			Vector2 smPrevNewCenter = prevScale*wordCenterFingerCurrent;
			
			
			Vector3 wordCenterFingerPrev = guiCamera.ScreenToWorldPoint(new Vector3(centerFingerPrev.x, centerFingerPrev.y,1f));
			Vector2 smPrev = prevScale*wordCenterFingerPrev;
			Vector2 smPrevNewScale = curScale*wordCenterFingerPrev;
			
			//Debug.LogWarning(wordCenterFingerPrev.x+ " "+wordCenterFingerPrev.y+ " " +wordCenterFingerCurrent.x+" "+wordCenterFingerCurrent.y);
				
			//точка в старом масштабе сместилась в место между пальцами.
			
			
			
			Vector3 pos = new Vector3(prevPos.x-smPrev.x+smPrevNewCenter.x,
				prevPos.y-smPrev.y+smPrevNewCenter.y,
				prevPos.z);
			
			pos = new Vector3(pos.x-(wordCenterFingerCurrent.x-pos.x)*(curScale-prevScale),
				pos.y-(wordCenterFingerCurrent.y-pos.y)*(curScale-prevScale),
				pos.z);
			
			singleTransform.localPosition = Norm(pos);
			
			
			prevScale = singleTransform.localScale.x;		
			prevPos = singleTransform.localPosition;
			prevFingerPos[0] = fingerPos[0];
			prevFingerPos[1] = fingerPos[1];
			
			
			
			//singleTransform.localPosition=newPos;
			//Debug.Log ("moveByx="+moveBy.x+" moveByy="+moveBy.y+" moveByOld="+moveByOld+" moveByNew="+moveByNew+" curScale="+singleTransform.localScale);
			
		}
	}
	
	public override void TouchEnded(Vector2 position,int fingerId) {
		if(numberOfFingers==1)
		{
			 //return;
		}
		base.TouchEnded(position,fingerId);
		if(numberOfFingers==2){
			initFingerPos[0]=fingerPos[0];
			initFingerPos[1]=fingerPos[1];
		}
		if(fingerId==fingers[0]&&numberOfFingers==2)
		{
			fingers[0]=fingers[1];
			initFingerPos[0]=initFingerPos[1];
			fingerPos[0]=fingerPos[1];
		}
		numberOfFingers--;
		scale=curScale;
	}
}
