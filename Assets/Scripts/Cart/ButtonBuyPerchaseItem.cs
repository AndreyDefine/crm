using UnityEngine;
using System.Collections;

public class ButtonBuyPerchaseItem : GuiButtonBase {
	private PurchaseItem purchaseItem;
		
	public void SetPurchaseItem(PurchaseItem purchaseItem){
		this.purchaseItem = purchaseItem;			
	}
	
	override protected void MakeOnTouch(){
		purchaseItem.Purchase();
	} 
}
