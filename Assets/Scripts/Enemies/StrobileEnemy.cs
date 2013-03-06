using UnityEngine;
using System.Collections;


public class StrobileEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.ShowQuestion(gameObject);
		PlayClipSound();
	}
	
	public override void OnExit(Collider other)
	{
		GuiLayer.HideQuestion();
	}
}