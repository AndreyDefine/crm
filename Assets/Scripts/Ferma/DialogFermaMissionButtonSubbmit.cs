using UnityEngine;
using System.Collections;

public class DialogFermaMissionButtonSubbmit: GuiButtonBase {
	public DialogFermaBuySlot dialogFermaBuySlot;
	override protected void MakeOnTouch(){
		dialogFermaBuySlot.Submit();
	}
}
