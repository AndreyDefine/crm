using UnityEngine;
using System.Collections;

public class DialogFermaMissionButtonCancel: GuiButtonBase {
	public DialogFermaBuySlot dialogFermaBuySlot;
	override protected void MakeOnTouch(){
		dialogFermaBuySlot.Cancel();
	}
}
