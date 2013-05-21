using UnityEngine;
using System.Collections;


public class DodgeBaran : AbstractBaseEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		Transform parent4 = GetParent (4);
		if (parent4 != null) {
			ElkMovingEnemy movingEnemy = parent4.GetComponent<ElkMovingEnemy> ();
			if (movingEnemy != null) {
				if (movingEnemy.GetEffectMade ()) {
					GlobalOptions.GetMissionEmmitters ().NotifyDodgeBaran (1);
				}
			}
		}
	}
}