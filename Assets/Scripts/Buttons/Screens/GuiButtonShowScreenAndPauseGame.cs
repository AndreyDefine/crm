using UnityEngine;
using System.Collections;

public class GuiButtonShowScreenAndPauseGame : GuiButtonShowScreen {	
	
	override protected void MakeOnTouch(){
		if(GlobalOptions.gameState==GameStates.GAME)
		{
			gameObject.active = false;
			screenLoader.LoadScreenByName(screenToShow);
			GlobalOptions.GetPlayerScript().PauseGame();
		}
	}
}
