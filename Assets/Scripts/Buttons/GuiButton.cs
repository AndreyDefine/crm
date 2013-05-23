using UnityEngine;
using System.Collections;

public class GuiButton : SpriteTouch {	
	protected Player playerScript; 
	
	protected override void Start() {
		base.Start();
		getPlayer();
	}
	
	protected void getPlayer()
	{
		playerScript=GlobalOptions.GetPlayerScript();
	}
}
