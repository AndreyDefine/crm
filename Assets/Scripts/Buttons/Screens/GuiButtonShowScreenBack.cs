using UnityEngine;
using System.Collections;

public class GuiButtonShowScreenBack : GuiButtonShowScreen {	
	override protected void MakeOnTouch(){
		screenLoader.LoadPrevScreen();
	}
	
}
