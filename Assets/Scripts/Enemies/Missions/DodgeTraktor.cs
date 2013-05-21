using UnityEngine;
using System.Collections;

public class DodgeTraktor : AbstractBaseEnemy
{	
	// Use this for initialization
	
	public override void OnHit (Collider other)
	{
		Transform parent4 = GetParent (4);
		if (parent4 != null) {
			MovingEnemy movingEnemy = parent4.GetComponent<MovingEnemy> ();
			if (movingEnemy != null) {
				if (movingEnemy.GetEffectMade ()) {
					GlobalOptions.GetMissionEmmitters ().NotifyDodgeTractor (1);
				}
			}
		}

	}
}