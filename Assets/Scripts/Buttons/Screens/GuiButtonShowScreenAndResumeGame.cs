using UnityEngine;
using System.Collections;

public class GuiButtonShowScreenAndResumeGame : GuiButtonShowScreen {		
	override protected void MakeOnTouch(){
		screenLoader.ResumeGameScreen();
		GlobalOptions.GetPlayerScript().ResumeGame();
	}
}
