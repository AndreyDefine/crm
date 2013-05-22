using UnityEngine;
using System.Collections;

public class ClearAllButton : GuiButtonBase {
	public DialogFerma dialogFerma;
	override protected void MakeOnTouch(){
		PlayerPrefs.DeleteAll();
	}
}
