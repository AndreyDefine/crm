using UnityEngine;
using System.Collections;

public class AndroidInap : Abstract, IInap{
	
	private string key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkUZfw6OPqYywVYwazU4AYOiI7yH0UGPC9U/SpWGKrviczp66r67Lfb22J9Q9iQKzbzinEJBdnJdSZyWJYuFTF4WGUvQzqb9MdMWhGg+FaNEv4kbDlkUWL+FJi1JsM5JrPEFQOP38sBiFl8RHvBJOmNsoDo9zkUHRV49DKbSE5ycZjms6RAqcKTXYmh0EYgo5IECmoNKSi5GCRCZjhFQTWYrOFTisQtrzrrPkuB9ALjOLriSt3snh9FCQXLbH7Nnab0mRxEgog8h2v5MYefzJYdA0/hZKq7EPOPIzV8HTkWy9Rc1N+VtFwp+x40Gkul3+CNL8m/ZVG/fpk7ROwGf5wQIDAQAB";
	
	public static string[] inapKeys = new string[] { 
		"android.test.purchased",
		"coins_pack2",
		"coins_pack3",
		"coins_pack4",
		"gold_pack1",
		"gold_pack2",
		"gold_pack3",
		"gold_pack4"
	};
	
#if UNITY_ANDROID
	void Start(){
		ShopEvents.inap = this;
		if(ShopEvents.inventory==null){
			InitPurchase();
		}
	}
#endif
	
	public void InitPurchase(){
		GoogleIAB.init( key );
	}
	
	public void QueryInventory(){
		GoogleIAB.queryInventory( inapKeys );		
	}
	
	public void Purchase(PurchaseData purchaseData){
		GoogleIAB.purchaseProduct(purchaseData.productId);	
	}
}
