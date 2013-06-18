using UnityEngine;
using System.Collections;

public class GuiButtonHeadStart : GuiButtonBase {
	
	public float headStartDistance;
	public int boostPrice;
	
	override protected void MakeOnTouch(){
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			if(PersonInfo.TryToBuy(boostPrice,0))
			{
				GlobalOptions.GetGuiLayer().MakeHeadStartButtonPushed(headStartDistance);	
			}
		}
	}
}
