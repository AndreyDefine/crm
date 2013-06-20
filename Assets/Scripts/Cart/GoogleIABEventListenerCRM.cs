using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class GoogleIABEventListenerCRM : MonoBehaviour
{
#if UNITY_ANDROID
	void OnEnable()
	{
		// Listen to all events for illustration purposes
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}


	void OnDisable()
	{
		// Remove all event handlers
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}



	void billingSupportedEvent()
	{
		ShopEvents.InapSupported();
	}


	void billingNotSupportedEvent( string error )
	{
		ShopEvents.InapNotSupported(error);
	}


	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		Dictionary<string,PurchaseData> inventory = new Dictionary<string, PurchaseData>();
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
		for(int i=0;i<skus.Count;i++){
			GoogleSkuInfo sku = skus[i];
			PurchaseData purchaseData = new PurchaseData();
			string productId = sku.productId;
			string id = GetIdFromProductId(productId);
			
			purchaseData.id = id;
			purchaseData.name = sku.title;
			purchaseData.description = sku.description;
			purchaseData.price = sku.price;
			purchaseData.productId = productId;
			inventory.Add(id,purchaseData);
		}
		ShopEvents.QueryInventorySucceeded(inventory);
	}
	
	private string GetIdFromProductId(string productId){
		string id="";
	
		if(productId.Equals(AndroidInap.inapKeys[0])){
			id = ShopEvents.COIN_PACK_1;	
		}
		if(productId.Equals(AndroidInap.inapKeys[1])){
			id = ShopEvents.COIN_PACK_2;	
		}
		if(productId.Equals(AndroidInap.inapKeys[2])){
			id = ShopEvents.COIN_PACK_3;	
		}
		if(productId.Equals(AndroidInap.inapKeys[3])){
			id = ShopEvents.COIN_PACK_4;	
		}
		
		if(productId.Equals(AndroidInap.inapKeys[4])){
			id = ShopEvents.GOLD_PACK_1;	
		}
		if(productId.Equals(AndroidInap.inapKeys[5])){
			id = ShopEvents.GOLD_PACK_2;	
		}
		if(productId.Equals(AndroidInap.inapKeys[6])){
			id = ShopEvents.GOLD_PACK_3;	
		}
		if(productId.Equals(AndroidInap.inapKeys[7])){
			id = ShopEvents.GOLD_PACK_4;	
		}
		return id;
	}


	void queryInventoryFailedEvent( string error )
	{
		ShopEvents.QueryInventoryFailed(error);
	}


	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
	}
	

	void purchaseSucceededEvent( GooglePurchase purchase )
	{
		string id = GetIdFromProductId(purchase.productId);
		ShopEvents.PurchaseSucceeded(id);
		Debug.Log( "purchaseSucceededEvent: " + purchase );
	}


	void purchaseFailedEvent( string error )
	{
		Debug.Log( "purchaseFailedEvent: " + error );
		ShopEvents.PurchaseFailed(error);
	}


	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		string id = GetIdFromProductId(purchase.productId);
		ShopEvents.PurchaseSucceeded(id);
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
	}


	void consumePurchaseFailedEvent( string error )
	{
		ShopEvents.PurchaseFailed(error);
		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}
#endif
}


