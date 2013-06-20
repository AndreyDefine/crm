using UnityEngine;
using System.Collections;

public interface IInap {

	void InitPurchase();
	void QueryInventory();
	void Purchase(PurchaseData purchaseData);
	void Consume(PurchaseData purchaseData);
	
}
