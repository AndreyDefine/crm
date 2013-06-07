using UnityEngine;
using System.Collections;


public class IkraEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(100);
		PlayClipSound();
		//audio.Play();
		MakeInactive();
	}
	
	void Update () {
		//Rotate();
	}
	
	public void Rotate()
	{
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
		{
			return;
		}
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*200,0));
	}
	
	public override void ReStart()
	{
		gameObject.SetActive(true);	
	}
}