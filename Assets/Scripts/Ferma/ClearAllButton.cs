using UnityEngine;
using System.Collections;

public class ClearAllButton : GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		Preloader.clearAll = true;
		Application.LoadLevel("Preloader"); 
	}
}
