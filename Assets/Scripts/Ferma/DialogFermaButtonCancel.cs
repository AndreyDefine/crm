using UnityEngine;
using System.Collections;

public class DialogFermaButtonCancel: GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		dialogFerma.GetFermaLocationPlace().Cancel();
	}
}
