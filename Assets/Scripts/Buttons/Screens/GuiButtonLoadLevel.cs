using UnityEngine;
using System.Collections;

public class GuiButtonLoadLevel : GuiButtonShowScreen {
	public int playingLevelNumber;
	public string levelToLoad="";
	
	override protected void MakeOnTouch(){
		GlobalOptions.loadingLevel=levelToLoad;
		GlobalOptions.PlayingLevelNumber=playingLevelNumber;
		GlobalOptions.SavePrefsLastPlayed();
		screenLoader.LoadScreenByName(screenToShow);
		Debug.Log (GlobalOptions.PlayingLevelNumber);
	}
}
