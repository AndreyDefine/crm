using UnityEngine;
using System.Collections;

public class DialogFermaButtonBuy: GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		dialogFerma.GetFactory().Buy();
	}
}
