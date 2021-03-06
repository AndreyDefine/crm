using UnityEngine;
using System.Collections;


public class ElkEnemy : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddToLife(-50, singleTransform);
		GuiLayer.AddHeadStars();
		PlayClipSound();
		if(playerScript.isVodka())
		{
			playerScript.MakeVodkaBoom();
			MakeInactiveParent();
		}
	}
	
	public override void ReStart()
	{
		singleTransform.parent.gameObject.SetActive(true);
	}
}