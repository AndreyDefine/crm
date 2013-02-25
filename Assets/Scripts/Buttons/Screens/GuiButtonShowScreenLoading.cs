using UnityEngine;
using System.Collections;

public class GuiButtonShowScreenLoading : GuiButtonShowScreen {
	public string levelToLoad="";
	
	override protected void MakeOnTouch(){
		GlobalOptions.loadingLevel=levelToLoad;
		screenLoader.LoadScreenByName(screenToShow);
	}
}
