using UnityEngine;
using System.Collections;

public class ResumeTimer : Abstract {
	
	public CrmFont crmFont;
	public int paddingTop;
	private int startTime = 3;
	private int curTime;
	
	void Start(){
		Vector3 pos=new Vector3(GlobalOptions.Vsizex/2,GlobalOptions.Vsizey-paddingTop,singleTransform.position.z);
		pos=GlobalOptions.NormalisePos(pos);
		pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
		//pos.x = 0f;
		pos.z = singleTransform.position.z;
		singleTransform.position=pos;
	}
	
	public void StartTimer(){
		curTime = startTime;
		crmFont.text = curTime.ToString();
		AnimationFactory.Bounce2(this,1f,1f,1.07f,"Bounce","BounceStopped");
	}
	
	public void BounceStopped(){
		curTime--;
		if(curTime>0){
			crmFont.text = curTime.ToString();
			animation.Rewind();
			animation.Play("Bounce");
		}else{
			Destroy(gameObject);
			GlobalOptions.GetGuiLayer().Resume();
		}
	}


}
