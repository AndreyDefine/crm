using UnityEngine;
using System.Collections;


public class TochkaSbora : AbstractEnemy,IMissionEmmitterListener {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddPosilka();
		PlayClipSound();
	}
	
	public override void ReStart()
	{
		singleTransform.parent.gameObject.SetActive(true);	
	}
	
	
	public void HasMissions (BaseMissionEmmitter missionEmmitter)
	{
		//do nothing
	}
	
	public void NoMissions (BaseMissionEmmitter missionEmmitter)
	{
		MakeInactiveParent();
	}
	
	protected virtual void OnDestroy(){
		GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter().RemoveMissionEmmitterListener(this);
	}
}