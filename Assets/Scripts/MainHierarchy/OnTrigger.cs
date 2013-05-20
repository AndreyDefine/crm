using UnityEngine;
using System.Collections;


public class OnTrigger : Abstract {	
	// Use this for initialization
	
	void OnTriggerEnter(Collider other) 
	{
		AbstractBaseEnemy obj=other.gameObject.GetComponent<AbstractBaseEnemy>();
		if (obj != null) {
            obj.OnHit(collider);
		}
	}
	void OnTriggerExit (Collider other)
	{
    	AbstractBaseEnemy obj=other.gameObject.GetComponent<AbstractBaseEnemy>();
		if (obj != null) {
            obj.OnExit(collider);
		}
	}

}