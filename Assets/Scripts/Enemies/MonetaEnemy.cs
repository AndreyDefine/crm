using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(300);
		PlayClipSound();
		//audio.Play();
		MakeInactive();
	}
	
	void Update () {
		Rotate();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*100,0));
	}
	
	public override void ReStart()
	{
		gameObject.SetActiveRecursively(true);	
	}
}