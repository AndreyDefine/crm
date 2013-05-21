using UnityEngine;
using System.Collections;


public class BelieEnemy : WoodEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GlobalOptions.GetMissionEmmitters().NotifySlideUnderRope(1);
	}
}