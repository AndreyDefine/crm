using UnityEngine;
using System.Collections;

public class GuiButtonTapToPlay : GuiButtonShowScreen {	
	override protected void MakeOnTouch(){
		GlobalOptions.GetPrefsLastPlayed();
		screenLoader.LoadScreenByName(screenToShow);
	}
}
