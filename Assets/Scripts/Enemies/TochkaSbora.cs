using UnityEngine;
using System.Collections;


public class TochkaSbora : AbstractEnemy,IMissionEmmitterListener {	
	
	public GameObject Yashik;
	private bool flagMadeOnHit=false;
	private Vector3 smexToYashikRight=new Vector3(0.07f,1.368f,0.137f);
	private Vector3 smexToYashikLeft=new Vector3(-0.07f,1.368f,0.137f);
	
	public override void OnHit(Collider other)
	{
		Vector3 moveTo=Yashik.transform.position;
		if(Yashik.transform.localPosition.x>0)
		{
			moveTo+=smexToYashikRight;
		}else
		{
			moveTo+=smexToYashikLeft;
		}
		flagMadeOnHit=true;
		GuiLayer.AddPosilka(moveTo);
		PlayClipSound();
	}
	
	public override void ReStart()
	{
		singleTransform.parent.gameObject.SetActive(true);	
		flagMadeOnHit=false;
	}
	
	
	public void HasMissions (BaseMissionEmmitter missionEmmitter)
	{
		//do nothing
	}
	
	public void NoMissions (BaseMissionEmmitter missionEmmitter)
	{
		if(!flagMadeOnHit)
		{
			MakeInactiveParent();
		}
	}
	
	protected virtual void OnDestroy(){
		if(GlobalOptions.GetMissionEmmitters())
		{
			GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().RemoveMissionEmmitterListener(this);
		}
	}
}