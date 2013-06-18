using UnityEngine;
using System.Collections;


public class TochkaSbora : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddPosilka();
		PlayClipSound();
	}
	
	public override void ReStart()
	{
		singleTransform.parent.gameObject.SetActive(true);	
	}
}