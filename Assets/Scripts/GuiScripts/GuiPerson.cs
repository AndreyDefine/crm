using UnityEngine;
using System.Collections;

public class GuiPerson : Abstract {

	public GuiProgress progress;
	public CrmFont level;
	
	public void InitPerson(){
		level.text = "x"+PersonInfo.GetPersonLevel().ToString();
		progress.SetProgress(PersonInfo.GetCurrentLevelProgress());
	}
}
