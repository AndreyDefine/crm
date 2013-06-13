using UnityEngine;
using System.Collections;


public class PostalEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		base.OnHit(other);
		GlobalOptions.GetPlayerScript().boostFx.Play();
		PersonInfo.AddPost(1);
		GuiLayer.AddPostal();
		PlayClipSound();
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
		gameObject.SetActive(true);	
	}
}