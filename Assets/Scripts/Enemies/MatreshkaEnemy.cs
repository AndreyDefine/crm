using UnityEngine;
using System.Collections;


public class MatreshkaEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddScoreScale(1);
		AudioSource.PlayClipAtPoint(playOnHit, transform.position);
		//audio.Play();
		MakeInactive();
	}
	
	void Update () {
		//Rotate();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,0,Time.deltaTime*100));
	}
	
	public override void ReStart()
	{
		gameObject.SetActiveRecursively(true);	
	}
}