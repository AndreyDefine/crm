using UnityEngine;
using System.Collections;

public class Dialog : Abstract {
	public GuiMissions guiMissions;
	public GuiPerson guiPerson;
	
	void Start(){
		guiMissions.InitMissions();
		guiPerson.InitPerson();
	}
}
