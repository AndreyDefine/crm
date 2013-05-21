using UnityEngine;
using System.Collections;


public class JumpOverSeno : AbstractBaseEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GlobalOptions.GetMissionEmmitters().NotifyJumpOverHaystack(1);
	}
}