using UnityEngine;
using System.Collections;


public class EnemyWithBoost : AbstractEnemy {	
	public Boost boostPrefab;
	
	public override void OnHit (Collider other)
	{
		base.OnHit (other);
		GlobalOptions.GetPlayerScript().boostCollected.Play();
	}
}