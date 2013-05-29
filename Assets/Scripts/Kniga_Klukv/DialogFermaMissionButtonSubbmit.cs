using UnityEngine;
using System.Collections;

public class DialogFermaMissionButtonSubbmit: GuiButtonBase {
	public DialogFermaBuyMission dialogFermaBuyMission;
	override protected void MakeOnTouch(){
		dialogFermaBuyMission.GetFermaMission().Subbmit();
	}
}
