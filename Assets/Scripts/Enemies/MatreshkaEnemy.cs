using UnityEngine;
using System.Collections;


public class MatreshkaEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddScoreScale(1);
		PlayClipSound();
		//audio.Play();
		MakeInactive();
	}
	
	void Update () {
		Rotate();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*200,0));
	}
	
	public override void ReStart()
	{
		gameObject.SetActiveRecursively(true);	
	}
}