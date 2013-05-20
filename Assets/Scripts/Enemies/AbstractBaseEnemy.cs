using UnityEngine;
using System.Collections;


public class AbstractBaseEnemy:Abstract{	
	
	public virtual void OnHit(Collider other){
	}
	
	public virtual void OnExit(Collider other){
	}
}