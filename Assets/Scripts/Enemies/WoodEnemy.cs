using UnityEngine;
using System.Collections;


public class WoodEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddToLife(-3);
		GuiLayer.AddHeadStars();
		PlayClipSound();
	}
}