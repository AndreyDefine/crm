using UnityEngine;
using System.Collections;


public class Rotation : Abstract {	
	// Use this for initialization
	
	void Update () {
		Rotate();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*300,0));
	}
}