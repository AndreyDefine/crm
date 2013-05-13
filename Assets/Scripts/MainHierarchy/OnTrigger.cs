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
	
	void OnCollisionEnter(Collision collision) {
        // Debug-draw all contact points and normals
		ContactPoint []contact=collision.contacts;
		int i;
        for (i=0;i<contact.Length;i++) {
			Debug.Log ("Contact");
            Debug.DrawRay(contact[i].point, contact[i].normal, Color.white);
        }
    }

}