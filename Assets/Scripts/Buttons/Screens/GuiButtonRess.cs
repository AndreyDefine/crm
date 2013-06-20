using UnityEngine;
using System.Collections;

public class GuiButtonRess : GuiButtonBase {
	
	public int boostPriceGold;
	
	override protected void MakeOnTouch(){
		if(GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			if(PersonInfo.TryToBuy(0,boostPriceGold))
			{
				GlobalOptions.GetGuiLayer().MakeRessButtonPushed();	
			}
		}
	}
}
