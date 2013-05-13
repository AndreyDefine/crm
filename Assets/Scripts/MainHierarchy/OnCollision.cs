using UnityEngine;
using System.Collections;


public class OnCollision : Abstract {	
	// Use this for initialization
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("OnCollisionEnter");
        // Debug-draw all contact points and normals
		ContactPoint []contact=collision.contacts;
		int i;
        for (i=0;i<contact.Length;i++) {
			Debug.Log ("Contact");
            Debug.DrawRay(contact[i].point, contact[i].normal, Color.white);
        }
    }

}