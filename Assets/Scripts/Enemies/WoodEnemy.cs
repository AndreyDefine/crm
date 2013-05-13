using UnityEngine;
using System.Collections;


public class WoodEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		playerScript.StumbleTrigger();
		GuiLayer.AddToLife(-3,singleTransform);
		GuiLayer.AddHeadStars();
		PlayClipSound();
	}
}