using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	
	public int numberOfMoney=1;
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(numberOfMoney);
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
		singleTransform.rotation=Quaternion.Euler(0, 0, 0);;
	}
}