using UnityEngine;
using System.Collections;

public class GuiButton : SpriteTouch {	
	protected Player playerScript; 
	
	private void Start() {
        init();
		getPlayer();
	}
	
	protected void getPlayer()
	{
		playerScript=GlobalOptions.GetPlayerScript();
	}
}
