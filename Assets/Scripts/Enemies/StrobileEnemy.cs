using UnityEngine;
using System.Collections;


public class StrobileEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.ShowQuestion(gameObject);
		//AudioSource.PlayClipAtPoint(playOnHit, transform.position);
	}
	
	public override void OnExit(Collider other)
	{
		GuiLayer.HideQuestion();
	}
}