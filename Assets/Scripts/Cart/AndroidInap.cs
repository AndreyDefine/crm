using UnityEngine;
using System.Collections;

public class AndroidInap : Abstract, IInap{
#if UNITY_ANDROID	
	private string key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkUZfw6OPqYywVYwazU4AYOiI7yH0UGPC9U/SpWGKrviczp66r67Lfb22J9Q9iQKzbzinEJBdnJdSZyWJYuFTF4WGUvQzqb9MdMWhGg+FaNEv4kbDlkUWL+FJi1JsM5JrPEFQOP38sBiFl8RHvBJOmNsoDo9zkUHRV49DKbSE5ycZjms6RAqcKTXYmh0EYgo5IECmoNKSi5GCRCZjhFQTWYrOFTisQtrzrrPkuB9ALjOLriSt3snh9FCQXLbH7Nnab0mRxEgog8h2v5MYefzJYdA0/hZKq7EPOPIzV8HTkWy9Rc1N+VtFwp+x40Gkul3+CNL8m/ZVG/fpk7ROwGf5wQIDAQAB";
	
	public static string[] inapKeys = new string[] { 
		"coins_pack1",
		"coins_pack2",
		"coins_pack3",
		"coins_pack4",
		"coins_pack5",
		"gold_pack1",
		"gold_pack2",
		"gold_pack3",
		"gold_pack4",
		"gold_pack5"
	};
#endif
	
#if UNITY_ANDROID
	void OnEnable()
	{
		ShopEvents.inap = this;
		ShopEvents.InitPurchase();
	}
#endif
	
	public void InitPurchase(){
		#if UNITY_ANDROID
		GoogleIAB.init( key );
		#endif
	}
	
	public void QueryInventory(){
		#if UNITY_ANDROID
		GoogleIAB.queryInventory( inapKeys );
		#endif
	}
	
	public void Purchase(string productId){
		#if UNITY_ANDROID
		if(ShopEvents.purchasedProductIds.Contains(productId)){
			GoogleIAB.consumeProduct(productId);
		}else{
			GoogleIAB.purchaseProduct(productId);
		}
		#endif
	}
	
	public void Consume(string productId){
		#if UNITY_ANDROID
		GoogleIAB.consumeProduct(productId);
		#endif
	}
}
