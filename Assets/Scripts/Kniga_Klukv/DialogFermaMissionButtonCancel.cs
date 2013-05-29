using UnityEngine;
using System.Collections;

public class DialogFermaMissionButtonCancel: GuiButtonBase {
	public DialogFermaBuyMission dialogFermaBuyMission;
	override protected void MakeOnTouch(){
		dialogFermaBuyMission.GetFermaMission().Cancel();
	}
}
