using UnityEngine;
using System.Collections;

public class KrestButton : GuiButton {
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId)&&GlobalOptions.gameState==GameStates.GAME;
		if(isTouchHandled){	
			playerScript.Restart();
		}
		return isTouchHandled;
	}
}
