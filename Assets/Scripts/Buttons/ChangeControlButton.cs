using UnityEngine;
using System.Collections;

public class ChangeControlButton : GuiButton {	
	
	private int maxControl=1;
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId)&&GlobalOptions.gameState==GameStates.GAME;
		if(isTouchHandled){	
			playerScript.typeOfControl++;
			playerScript.typeOfControl=playerScript.typeOfControl>maxControl?0:playerScript.typeOfControl;
			Debug.Log (playerScript.typeOfControl);
		}
		return isTouchHandled;
	}
}
