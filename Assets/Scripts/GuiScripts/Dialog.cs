using UnityEngine;
using System.Collections;

public class Dialog : Abstract {
	public GuiMissions guiMissions;
	public GuiPerson guiPerson;
	public GuiPointsAndMoney guiPointsAndMoney;
	public CrmFont dialogName;
	
	void Start(){
		if(GlobalOptions.gameState == GameStates.GAME_OVER){
			dialogName.text = "Game is Over";
		}else if(GlobalOptions.gameState == GameStates.PAUSE_MENU){
			dialogName.text = "Game is Paused";
		}else{
			dialogName.text ="";
		}	
		guiMissions.InitMissions();
		guiPerson.InitPerson();
		guiPointsAndMoney.InitPointsAndMoney();
	}
}
