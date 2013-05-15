using UnityEngine;
using System.Collections;

public class ScreenPauseControllerScript : Abstract,ScreenControllerToShow {
	
	public GuiMissions guiMissions;
	public GuiPerson guiPerson;
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		guiMissions.InitMissions();
		guiPerson.InitPerson();
	}
	
	public void HideOnScreen()
	{
		//do nothing
	}
	
	//end Screen Controller To Show Methods
}
