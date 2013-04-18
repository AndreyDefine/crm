using UnityEngine;
using System.Collections;


public class TochkaSbora : AbstractEnemy {	
	// Use this for initialization
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMission();
		PlayClipSound();
	}
}