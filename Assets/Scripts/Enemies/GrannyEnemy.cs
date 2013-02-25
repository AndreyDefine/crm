using UnityEngine;
using System.Collections;


public class GrannyEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		AudioSource.PlayClipAtPoint(playOnHit, transform.position);
		
		MakeInactive();
		MakeAction();
	}
	
	void Update () {
		//Rotate();
	}
	
	public override void MakeAction()
	{
		MakeInactive();
		GuiLayer.ShowStar();	
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