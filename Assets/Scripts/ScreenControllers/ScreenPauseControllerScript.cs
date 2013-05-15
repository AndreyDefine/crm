using UnityEngine;
using System.Collections;

public class ScreenPauseControllerScript : Abstract,ScreenControllerToShow {
	
	public GuiMissions guiMissions;
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		guiMissions.InitMissions();
	}
	
	public void HideOnScreen()
	{
		//do nothing
	}
	
	//end Screen Controller To Show Methods
}
