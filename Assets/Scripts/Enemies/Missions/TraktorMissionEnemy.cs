using UnityEngine;
using System.Collections;


public class TraktorMissionEnemy : ElkEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GlobalOptions.GetMissionEmmitters().NotifyTraktorDeath(1);
	}
}