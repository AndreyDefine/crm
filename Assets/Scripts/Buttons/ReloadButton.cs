using UnityEngine;
using System.Collections;

public class ReloadButton : GuiButton {
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){			
			playerScript.Restart();
		}

		return isTouchHandled;
	}
}
