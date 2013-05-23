using UnityEngine;
using System.Collections;

public class DialogFermaButtonPlay: GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		dialogFerma.factory.Play();
	}
}
