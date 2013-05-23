using UnityEngine;
using System.Collections;


public class SlideUnderSomething : AbstractBaseEnemy{	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GlobalOptions.GetMissionEmmitters().NotifySlideUnderSomething(1);
	}
}