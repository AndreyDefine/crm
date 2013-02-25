using UnityEngine;
using System.Collections;

public class GuiButtonShowScreenAndRestartGame : GuiButtonShowScreen {		
	override protected void MakeOnTouch(){
		screenLoader.LoadScreenByName(screenToShow);
		GlobalOptions.GetPlayerScript().Restart();
	}
}
