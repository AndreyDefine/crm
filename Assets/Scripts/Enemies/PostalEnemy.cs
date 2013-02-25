using UnityEngine;
using System.Collections;


public class PostalEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(50);
		GuiLayer.AddScoreScale(1);
		GuiLayer.AddPostal();
		AudioSource.PlayClipAtPoint(playOnHit, transform.position);
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