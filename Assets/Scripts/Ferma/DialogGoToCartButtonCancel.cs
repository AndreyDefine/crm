using UnityEngine;
using System.Collections;

public class DialogGoToCartButtonCancel: GuiButtonBase {
	public DialogGoToCart dialogGoToCart;
	override protected void MakeOnTouch(){
		dialogGoToCart.Cancel();
	}
}
