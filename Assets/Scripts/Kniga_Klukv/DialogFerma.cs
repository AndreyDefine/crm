using UnityEngine;
using System.Collections;

public class DialogFerma : SpriteTouch {
	
	private FermaLocationPlace place;
	
	public virtual void SetFermaLocationPlace(FermaLocationPlace place){
		this.place = place;
	}
	
	public FermaLocationPlace GetFermaLocationPlace(){
		return place;
	}
	
	protected override void InitTouchZone ()
	{
		touchZone = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	public void Show(){	
		singleTransform.localScale = new Vector3(0f,0f,0f);
		AnimationFactory.ScaleInXYZ(this,new Vector3(1f,1f,1f),0.5f,"scaleIn","ScaleInEnd");
	}
	
	public void ScaleInEnd(){
		float radius = 0.005f;
		AnimationFactory.MoveRound(this,2.5f,0.015f,"MoveRound");
	}
		
	public void CloseDialog(){
		AnimationFactory.ScaleOutXYZ(this,new Vector3(0f,0f,0f),0.5f,"ScaleOut", "ScaleOutEnd");
	}	
	
	public void ScaleOutEnd(){
		GetFermaLocationPlace().DialogClosed();
		Destroy(gameObject);
	}
}
