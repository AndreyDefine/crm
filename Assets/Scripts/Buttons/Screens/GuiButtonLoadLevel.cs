using UnityEngine;
using System.Collections;

public class GuiButtonLoadLevel : GuiButtonShowScreen {
	public int playingLevelNumber;
	public string levelToLoad="";
	
	override protected void MakeOnTouch(){
		//obalOptions.loadingLevel=levelToLoad;
		//GlobalOptions.PlayingLevelNumber=playingLevelNumber;
		//GlobalOptions.SavePrefsLastPlayed();
		screenLoader.LoadScreenByName(screenToShow);
	}
}
