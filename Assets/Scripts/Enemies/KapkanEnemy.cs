using UnityEngine;
using System.Collections;


public class KapkanEnemy : AbstractEnemy {	
	
	private GameObject kapkan2;
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		playerScript.StumbleTrigger();
		GuiLayer.AddToLife(-3,singleTransform);
		GuiLayer.AddHeadStars();
		PlayClipSound();
		MakeInactive();
	}
	
	public override void MakeInactive()
	{
		if(!kapkan2)
		{
			kapkan2=singleTransform.parent.GetChild(1).gameObject;
		}
		gameObject.SetActive(false);
		if(kapkan2)
		{
			kapkan2.SetActive(true);
		}
	}
	
	public override void ReStart()
	{
		if(!kapkan2)
		{
			kapkan2=singleTransform.parent.GetChild(1).gameObject;
		}
		gameObject.SetActive(true);
		if(kapkan2)
		{
			kapkan2.SetActive(false);
		}
	}
}