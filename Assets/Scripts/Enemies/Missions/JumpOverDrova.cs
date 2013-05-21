using UnityEngine;
using System.Collections;


public class JumpOverDrova : AbstractBaseEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GlobalOptions.GetMissionEmmitters().NotifyJumpOverDrova(1);
	}
}