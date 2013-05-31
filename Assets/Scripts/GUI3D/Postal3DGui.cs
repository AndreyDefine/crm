using UnityEngine;
using System.Collections;

public class Postal3DGui : Abstract {
	
	// Update is called once per frame
	void Update () {
		Rotate();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*100,0));
	}
}
