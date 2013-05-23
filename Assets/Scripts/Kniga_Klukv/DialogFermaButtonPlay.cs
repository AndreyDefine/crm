using UnityEngine;
using System.Collections;

public class DialogFermaButtonPlay: GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		Factory curFactory=dialogFerma.GetFactory();
		curFactory.Play();
	}
	
	protected override void Start() {
        base.Start ();
	}
}
