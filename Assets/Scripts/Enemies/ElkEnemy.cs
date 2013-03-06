using UnityEngine;
using System.Collections;


public class ElkEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddToLife(-50);
		GuiLayer.AddHeadStars();
		PlayClipSound();
	}
}