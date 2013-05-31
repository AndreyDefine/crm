using UnityEngine;
using System.Collections;

public class Points : Abstract {

	public CrmFont crmFont;
	private int points;
	public X x;
	
	void Awake(){
		Vector3 pos=new Vector3(GlobalOptions.Vsizex-15,GlobalOptions.Vsizey-37,singleTransform.position.z);
		pos=GlobalOptions.NormalisePos(pos);
		pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
		singleTransform.position=pos;	
	}
	
	public void AddPoints(int addPoints){
		this.points+=addPoints;
		crmFont.text = string.Format ("{0}", this.points);
		x.Position();
	}
	
	public void SetPoints(int points){
		this.points = points;
		AddPoints(0);
	}
	
	public int GetPoints(){
		return points;
	}
}
