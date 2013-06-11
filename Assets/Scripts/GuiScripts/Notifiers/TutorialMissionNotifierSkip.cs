using UnityEngine;
using System.Collections;


public class TutorialMissionNotifierSkip : GuiButtonBase {
	public TutorialMissionNotifier tutorialMissionNotifier;
	override protected void MakeOnTouch(){
		tutorialMissionNotifier.Skip();
	}
}