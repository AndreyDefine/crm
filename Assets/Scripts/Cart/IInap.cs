using UnityEngine;
using System.Collections;

public interface IInap {

	void InitPurchase();
	void QueryInventory();
	void Purchase(string productId);
	void Consume(string productId);
	
}
