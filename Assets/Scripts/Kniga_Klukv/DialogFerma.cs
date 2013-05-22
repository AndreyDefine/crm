using UnityEngine;
using System.Collections;

public class DialogFerma : Abstract {
	
	private float padding = 15;
	public Factory factory;
	
	public void ShowForPosition(Vector3 pos){
		singleTransform.position = pos;
		
		Vector3 posLeftTop=new Vector3(padding,GlobalOptions.Vsizey-padding,1f);
		posLeftTop=GlobalOptions.NormalisePos(posLeftTop);
		posLeftTop=Cameras.GetGUICamera().ScreenToWorldPoint(posLeftTop);
	
		Vector3 posRightBottom=new Vector3(GlobalOptions.Vsizex-padding,padding,1f);
		posRightBottom=GlobalOptions.NormalisePos(posRightBottom);
		posRightBottom=Cameras.GetGUICamera().ScreenToWorldPoint(posRightBottom);
		
		Vector3 newPos = pos;
		//right	
		if(pos.x+singleRenderer.bounds.extents.x>posRightBottom.x){
			pos.x = posRightBottom.x-singleRenderer.bounds.extents.x;
		}
		//left
		if(pos.x-singleRenderer.bounds.extents.x<posLeftTop.x){
			pos.x = posLeftTop.x+singleRenderer.bounds.extents.x;
		}
		//top
		if(pos.y+singleRenderer.bounds.extents.y>posLeftTop.y){
			pos.y = posLeftTop.y-singleRenderer.bounds.extents.y;
		}
		//bottom
		if(pos.y-singleRenderer.bounds.extents.y<posRightBottom.y){
			pos.y = posRightBottom.y+singleRenderer.bounds.extents.y;
		}
		singleTransform.position=pos;	
		
		singleTransform.localScale = new Vector3(0f,0f,0f);
		AnimationFactory.ScaleInXYZ(this,new Vector3(1f,1f,1f),0.5f,"scaleIn","ScaleInEnd");
	}
	
	public void ScaleInEnd(){
		float radius = 0.015f;
		AnimationFactory.MoveRound(this,2.5f,0.015f,"MoveRound");
	}
		
	public void CloseDialog(){
		AnimationFactory.ScaleOutXYZ(this,new Vector3(0f,0f,0f),0.5f,"ScaleOut", "ScaleOutEnd");
	}	
	
	public void ScaleOutEnd(){
		Destroy(gameObject);
	}
}
