using UnityEngine;
using System.Collections;


public class VodkaEnemy : EnemyWithBoost {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GuiLayer.AddVodka(boostPrefab);
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
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*playerScript.GetRealVelocityWithNoDeltaTime()*5,0));
	}
	
	public override void ReStart()
	{
		gameObject.SetActive(true);	
	}
}