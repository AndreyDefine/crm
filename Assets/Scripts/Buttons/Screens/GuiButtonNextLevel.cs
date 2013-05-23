using UnityEngine;
using System.Collections;

public class GuiButtonNextLevel : GuiButtonShowScreen {
	override protected void MakeOnTouch(){
		
		//Get NextLevel
		GlobalOptions.loadingLevel=GlobalOptions.GetWorldFactory().GetComponent<WorldFactory>().GetNextLevelName();
		//GlobalOptions.PlayingLevelNumber=GlobalOptions.PlayingLevelNumber++;
		//GlobalOptions.SavePrefsLastPlayed();
		screenLoader.LoadScreenByName(screenToShow);
	}
}
