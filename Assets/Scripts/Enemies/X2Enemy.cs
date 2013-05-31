using UnityEngine;
using System.Collections;


public class X2Enemy : EnemyWithBoost {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddX2(boostPrefab);
		PlayClipSound();
		//audio.Play();
		MakeInactive();
	}
	
	void Update () {
		Rotate();
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
		gameObject.SetActiveRecursively(true);	
	}
}