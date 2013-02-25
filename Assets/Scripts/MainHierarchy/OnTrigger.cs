using UnityEngine;
using System.Collections;


public class OnTrigger : Abstract {	
	// Use this for initialization
	
	void OnTriggerEnter(Collider other) 
	{
		AbstractEnemy obj=other.gameObject.GetComponent<AbstractEnemy>();
		if (obj != null) {
            obj.OnHit(collider);
		}
	}
	void OnTriggerExit (Collider other)
	{
    	AbstractEnemy obj=other.gameObject.GetComponent<AbstractEnemy>();
		if (obj != null) {
            obj.OnExit(collider);
		}
	}

}