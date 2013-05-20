using UnityEngine;
using System.Collections;


public class SenoMissionEnemy : ElkEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GlobalOptions.GetMissionEmmitters().NotifySenoDeath(1);
	}
}