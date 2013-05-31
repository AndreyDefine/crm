using UnityEngine;
using System.Collections;

public class GuiButtonShowScreen : GuiButtonBase {
	public string screenToShow="";
	
	protected ScreenLoader screenLoader;
	
	protected override void Start() {
        init();
		screenLoader=GlobalOptions.GetScreenLoader();
	}
	
	override protected void MakeOnTouch(){
		if(screenToShow=="ScreenShop"||screenToShow=="ScreenSettings"||screenToShow=="ScreenMap"||screenToShow=="ScreenKniga")
		{
			FlurryPlugin.FlurryLogEvent(screenToShow);
		}
		screenLoader.LoadScreenByName(screenToShow);
	}
	
}
