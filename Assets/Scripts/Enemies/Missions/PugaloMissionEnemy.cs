using UnityEngine;
using System.Collections;


public class PugaloMissionEnemy : ElkEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GlobalOptions.GetMissionEmmitters().NotifyScarecrowDeath(1);
	}
}