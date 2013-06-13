using UnityEngine;
using System.Collections;

public class ButtonScreenPosition : Abstract {

	public bool top;
	public bool bottom;
	public bool right;
	public bool left;
	public float paddingTop;
	public float paddingBottom;
	public float paddingRight;
	public float paddingLeft;
	// Use this for initialization
	public override void Awake () {
		base.Awake();
		if(top&&bottom||right&&left){
			Debug.LogError("check "+gameObject.name);
		}
		if(top&&left){
			Vector3 pos=new Vector3(paddingLeft,GlobalOptions.Vsizey-paddingTop,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x+=singleRenderer.bounds.extents.x;
			pos.y-=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;
		}
		if(top&&right){
			Vector3 pos=new Vector3(GlobalOptions.Vsizex-paddingRight,GlobalOptions.Vsizey-paddingTop,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x-=singleRenderer.bounds.extents.x;
			pos.y-=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;
		}
		if(bottom&&left){
			Vector3 pos=new Vector3(paddingLeft,paddingBottom,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x+=singleRenderer.bounds.extents.x;
			pos.y+=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;
		}
		if(bottom&&right){
			Vector3 pos=new Vector3(GlobalOptions.Vsizex-paddingRight,paddingBottom,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x-=singleRenderer.bounds.extents.x;
			pos.y+=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;
		}
		
		if(left){
			Vector3 pos=new Vector3(paddingLeft,0f,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x+=singleRenderer.bounds.extents.x;
			pos.y = 0f;
			
			singleTransform.position=pos;	
			return;	
		}
		
		if(right){
			Vector3 pos=new Vector3(GlobalOptions.Vsizex-paddingRight,0f,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x-=singleRenderer.bounds.extents.x;
			pos.y = 0f;
			
			singleTransform.position=pos;	
			return;	
		}
		
		if(top){
			Vector3 pos=new Vector3(0f,GlobalOptions.Vsizey-paddingTop,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x = 0f;
			pos.y-=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;	
		}
		
		if(bottom){
			Vector3 pos=new Vector3(0f,paddingBottom,singleTransform.position.z);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
			pos.x = 0f;
			pos.y+=singleRenderer.bounds.extents.y;
			
			singleTransform.position=pos;	
			return;	
		}
	}
	
}
