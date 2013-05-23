using UnityEngine;
using System.Collections;


public class SlideUnderBelie : AbstractBaseEnemy{	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GlobalOptions.GetMissionEmmitters().NotifySlideUnderRope(1);
	}
}