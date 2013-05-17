using UnityEngine;
using System.Collections;
using  System.Globalization;

public class SpriteTouch : Abstract,TouchTargetedDelegate {
	public string pathTouch="";
	public bool multiTouch=false;
	public bool invisibleTouch=false;
	public int touchPriority=0;
	public bool swallowTouches=true;
	
	public float ExtraTouchScale=1;
	
	public bool flagFitToWidth=false; 
	public bool flagAllWidthHeight=false; 
	public bool getTouches=true; 
	public bool notTouchObject=false; 
	
    protected Vector2 firstTouchLocation;
	protected Rect touchZone;
	protected bool alreadyTouched;
	
	protected Vector2 []Path;
	protected Vector2 size;
	protected float scale;
	protected Vector2 pos;
	protected float perPixel;
	
	Camera GUIcamera;
	
	TouchDispatcher sharedTouchDispatcher;
	
    protected virtual void InitTouchZone() {
        touchZone = new Rect (0, 0, Screen.width, Screen.height);
		if(renderer)
		{
	        touchZone = new Rect(pos.x-renderer.bounds.extents.x*perPixel*scale, 
				pos.y -  renderer.bounds.extents.y*perPixel*scale,
				renderer.bounds.size.x*perPixel*scale, 
				renderer.bounds.size.y*perPixel*scale);
		}
    }
    
    private void Start() {
		scale=1;
		init();
    }
	
	private void OnDestroy()
	{
		if(sharedTouchDispatcher&&!notTouchObject)
		{
			sharedTouchDispatcher.removeDelegate(this);
		}
	}
	
	protected void GetSharedTouchDispatcher(){
		//find with no parents
		sharedTouchDispatcher = TouchDispatcher.GetSharedTouchDispatcher();
	}
	
	protected virtual void init(){
		GUIcamera=Cameras.GetGUICamera();
		perPixel = Cameras.GuiCameraPixelPerUnit ();
		
		//TouchDispatcher.sharedTouchDispatcher.addTargetedDelegate(this);
		MakeScaling();
		//у объекта есть отображение
		GetSizePos();
		if(!notTouchObject){
			GetSharedTouchDispatcher();
			sharedTouchDispatcher.addTargetedDelegate(this,touchPriority,swallowTouches);
		}
		InitTouchZone ();
		alreadyTouched=false;
	}
	
	public void removeFromDispatcher(){
		sharedTouchDispatcher.removeDelegate(this);
	}
	
	//TouchDelegateMethods
	public virtual bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=MakeDetection(position);
		if(alreadyTouched){
			return false;
		}
		if(isTouchHandled){	
			if(!multiTouch)
			{
				alreadyTouched=true;
			}
			firstTouchLocation=position;
		}
		return isTouchHandled;
	}
	
	public virtual void TouchMoved(Vector2 position,int fingerId) {
		//do nothing
	}
	
	public virtual void TouchEnded(Vector2 position,int fingerId) {
		alreadyTouched=false;
	}
	
	public virtual void TouchCanceled(Vector2 position,int fingerId) {
		//usually the same as end
		TouchEnded(position,fingerId);
	}
	
	//end TouchDelegateMethods
	
	protected virtual bool MakeDetection(Vector2 position)
	{
		bool isTouchHandled;
		
		if(!getTouches)
		{
			return false;
		}
		
		if(Path==null&&pathTouch!=""){
			ParsePath();
		}
		
		if(gameObject&&gameObject.active==false&&!invisibleTouch){
			return false;
		}
		
		GetSizePos();
		
		if(Path!=null)
		{
			isTouchHandled=MakeDetectionOnPath(position);
		}
		else
		{
			isTouchHandled=MakeDetectionOnRect(position);
		}
		return isTouchHandled;
	}
	
	protected virtual bool MakeDetectionOnRect(Vector2 position)
	{
		bool isTouchHandled;
		//need to write about position changing on rect colliders
		isTouchHandled=touchZone.Contains(position);
		
		return isTouchHandled;
	}
	
	protected virtual bool MakeDetectionOnPath(Vector2 position)
	{
		int result=0;
		//float biggerSize=size.x>size.y?size.x:size.y;
		float xx,yy;
		int num=Path.Length;
		Vector2 myoffset=pos;
		float rot=0;
		CrossResultRec CrossResult;
		//vertwxhelpertouch
        Vector2 p11,p12,p21,p22;
        p11.x=position.x;
        p11.y=position.y;
        p12.x=position.x;
        p12.y=position.y+GlobalOptions.Vsizey*30;
		
		
        xx=Path[num-1].x*scale*ExtraTouchScale*GlobalOptions.scaleFactory;
        yy=Path[num-1].y*scale*ExtraTouchScale*GlobalOptions.scaleFactory;
        p21.x=myoffset.x+(xx*Mathf.Cos(rot)+yy*Mathf.Sin(rot));
        p21.y=myoffset.y+(-xx*Mathf.Sin(rot)+yy*Mathf.Cos(rot));
        for(int i=0;i<num;i++)
        {
            xx=Path[i].x*scale*ExtraTouchScale*GlobalOptions.scaleFactory;
            yy=Path[i].y*scale*ExtraTouchScale*GlobalOptions.scaleFactory;
            p22.x=myoffset.x+(xx*Mathf.Cos(rot)+yy*Mathf.Sin(rot));
            p22.y=myoffset.y+(-xx*Mathf.Sin(rot)+yy*Mathf.Cos(rot));
			
            CrossResult=Crossing.GetCrossing(p11,p12,p21,p22);
            if (CrossResult.type==enumCrossType.ctInBounds||CrossResult.type==enumCrossType.ctOnBounds)
            {
                result++;
            }
            p21.x=p22.x;
            p21.y=p22.y;
        }
		return (result&1)!=0;
	}
	
	private void ParsePath()
	{
		//получили массив пути
		char []separator={',','\n'};
		string []numbers=pathTouch.Split(separator);
		Path= new Vector2[numbers.Length/2];
		for (int i=0;i+1<numbers.Length;i+=2){
			//normalized path
			Path[i/2]=new Vector2(float.Parse(numbers[i],NumberStyles.Currency),float.Parse(numbers[i+1],NumberStyles.Currency));
		}
	}
	
	protected virtual void GetSizePos() {
		Vector3 p1 = GUIcamera.WorldToScreenPoint (new Vector3 (singleTransform.position.x, singleTransform.position.y, singleTransform.position.z));  
		
		scale=singleTransform.lossyScale.x;
		if(renderer)
		{
			size=new Vector2(renderer.bounds.size.x*perPixel,renderer.bounds.size.y*perPixel);
		}
		else{
			size=new Vector2(GlobalOptions.Vsizex*perPixel,GlobalOptions.Vsizey*perPixel);
		}
		pos=new Vector2(p1.x,p1.y);
	}
	
	private void MakeScaling(){
		if(flagFitToWidth||flagAllWidthHeight)
		{
			float newscale=1;
			if(flagFitToWidth){
				//пропорция
				newscale=(float)Screen.width/Screen.height*(GlobalOptions.Vsizey/GlobalOptions.Vsizex);
				if(newscale>1)
				{
					newscale=1;
					//newscale=(float)Screen.height/Screen.width*(GlobalOptions.Vsizex/GlobalOptions.Vsizey);
				}
			}
			if(flagAllWidthHeight){
				if(renderer)
				{
					float vspnewscale=1;
					newscale=(float)Screen.width/(renderer.bounds.size.x*perPixel);
					vspnewscale=(float)Screen.height/(renderer.bounds.size.y*perPixel);
					newscale=newscale>vspnewscale?newscale:vspnewscale;
				}
			}
			singleTransform.localScale=new Vector3(newscale,newscale,1);
		}
	}
}
