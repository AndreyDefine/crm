using UnityEngine;
using System.Collections;


public class JumpOverCaw : AbstractBaseEnemy {	
	// Use this for initialization
	
	public void OnHit(Collider other)
	{
		Debug.Break();
		GlobalOptions.GetMissionEmmitters().NotifyJumpOverCaw(1);
	}
}