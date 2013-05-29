using UnityEngine;
using System.Collections;

public class DialogFermaButtonPlay: GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		FermaLocationPlace place=dialogFerma.GetFermaLocationPlace();
		place.Play();
	}
	
	protected override void Start() {
        base.Start ();
	}
}
