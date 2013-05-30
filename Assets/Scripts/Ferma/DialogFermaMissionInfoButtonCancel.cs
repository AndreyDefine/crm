using UnityEngine;
using System.Collections;

public class DialogFermaMissionInfoButtonCancel: GuiButtonBase {
	public DialogFermaMissionInfo dialogFermaMissionInfo;
	override protected void MakeOnTouch(){
		dialogFermaMissionInfo.Cancel();
	}
}
