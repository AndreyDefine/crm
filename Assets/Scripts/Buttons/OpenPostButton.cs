using UnityEngine;
using System.Collections;

public class OpenPostButton : SpriteTouch {	
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			MakeOnTouch();
		}
		return isTouchHandled;
	}
	
	private void MakeOnTouch()
	{
		int numberOfBoxes=PersonInfo.post;
		GlobalOptions.GetGuiLayer().AddMoney(numberOfBoxes*Random.Range(100,300));
		PersonInfo.AddPost(-PersonInfo.post);
		GlobalOptions.GetScreenLoader().LoadScreenByName("ScreenGameOver");
	}
}
