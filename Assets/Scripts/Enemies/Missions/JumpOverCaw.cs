using UnityEngine;
using System.Collections;


public class JumpOverCaw : AbstractBaseEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GlobalOptions.GetMissionEmmitters().NotifyJumpOverCaw(1);
	}
}