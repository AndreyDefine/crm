using UnityEngine;
using System.Collections;


public class TochkaSbora : AbstractEnemy,IMissionEmmitterListener {	

	private bool flagMadeOnHit=false;
	
	public override void OnHit(Collider other)
	{
		flagMadeOnHit=true;
		GuiLayer.AddPosilka();
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
		GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().RemoveMissionEmmitterListener(this);
	}
}