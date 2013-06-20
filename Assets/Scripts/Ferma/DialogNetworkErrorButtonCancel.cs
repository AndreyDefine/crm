using UnityEngine;
using System.Collections;

public class DialogNetworkErrorButtonCancel: GuiButtonBase {
	public DialogNetworkError dialogNetworkError;
	override protected void MakeOnTouch(){
		dialogNetworkError.Cancel();
	}
}
