using UnityEngine;
using System.Collections;

public class PurchaseItem : Abstract {
	
	public CrmFont price;
	public CrmFont name;
	public ButtonBuyPerchaseItem button;
	
	private PurchaseData purchaseData;
	
	void Start(){
		button.SetPurchaseItem(this);
	}
		
	public void setPurchaseData(PurchaseData purchaseData){
		this.purchaseData = purchaseData;
		name.text = purchaseData.name;
		price.text = purchaseData.price;
	} 
	
	public void Purchase(){
		ShopEvents.Purchase(purchaseData);
	}
}
