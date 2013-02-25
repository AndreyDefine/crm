using UnityEngine;
using System.Collections;

public class QuestionButton : GuiButton {
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){			
			playerScript.ShowCap();
		}
		return isTouchHandled;
	}
}
