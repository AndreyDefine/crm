using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopEvents{
	public static string COIN_PACK_1 = "coin_pack_1";
	public static string COIN_PACK_2 = "coin_pack_2";
	public static string COIN_PACK_3 = "coin_pack_3";
	public static string COIN_PACK_4 = "coin_pack_4";
	public static string COIN_PACK_5 = "coin_pack_5";
	
	public static string GOLD_PACK_1 = "gold_pack_1";
	public static string GOLD_PACK_2 = "gold_pack_2";
	public static string GOLD_PACK_3 = "gold_pack_3";
	public static string GOLD_PACK_4 = "gold_pack_4";
	public static string GOLD_PACK_5 = "gold_pack_5";
	
	public static Dictionary<string,PurchaseData> inventory = null;
	public static ArrayList purchasedProductIds = new ArrayList();
	
	private static ArrayList listeners = new ArrayList();
	
	public static IInap inap;
	
	public static void InitPurchase(){
		inap.InitPurchase();
	}
	
	public static void InapNotSupported(string error){
		ShowErrorDialog(error);
	}
	
	public static void QueryInventoryFailed(string error){
		ShowErrorDialog(error);
	}
	
	private static void ShowErrorDialog(string error){
		GameObject dialogGoToCartGameObj = GameObject.Instantiate(Resources.Load("Screens/DialogNetworkError")) as GameObject;
			DialogNetworkError dialogNetworkError = dialogGoToCartGameObj.GetComponent<DialogNetworkError>();
			dialogNetworkError.SetErrorText(error);
			dialogNetworkError.Show();	
	}
	
	public static void InapSupported(){
		inap.QueryInventory();
	}
	
	public static void QueryInventorySucceeded(Dictionary<string,PurchaseData> inv){
		inventory = inv;
		NotifyInventoryDataRecieved();
	}
	
	public static void PurchaseSucceeded(string id){
		if(id.Equals(COIN_PACK_1)){
			PersonInfo.AddCoins(7500);
		}
		
		if(id.Equals(COIN_PACK_2)){
			PersonInfo.AddCoins(30000);
		}
		
		if(id.Equals(COIN_PACK_3)){
			PersonInfo.AddCoins(150000);
		}
		
		if(id.Equals(COIN_PACK_4)){
			PersonInfo.AddCoins(300000);
		}
		
		if(id.Equals(COIN_PACK_5)){
			PersonInfo.AddCoins(500000);
		}
		
		if(id.Equals(GOLD_PACK_1)){
			PersonInfo.AddGold(50);
		}
		
		if(id.Equals(GOLD_PACK_2)){
			PersonInfo.AddGold(120);
		}
		
		if(id.Equals(GOLD_PACK_3)){
			PersonInfo.AddGold(250);
		}
		
		if(id.Equals(GOLD_PACK_4)){
			PersonInfo.AddGold(560);
		}
		
		if(id.Equals(GOLD_PACK_5)){
			PersonInfo.AddGold(1400);
		}
	}
	
	public static void ConsumePurchaseSucceeded(string productId){
		inap.Purchase(productId);
	}
	
	
	public static void PurchaseFailed(string error){
		ShowErrorDialog(error);
	}
	
	public static void ConsumePurchaseFailed(string error){
		ShowErrorDialog(error);
	}
	
	
	public static void Purchase(PurchaseData purchaseData){
		inap.Purchase(purchaseData.productId);
	}
	
	public static void Consume(PurchaseData purchaseData){
		inap.Consume(purchaseData.productId);
	}
	
	//listeners
	public static void AddListener(IShopEventListener list){
		listeners.Add(list);	
	}
	
	public static void RemoveListener(IShopEventListener list){
		listeners.Remove(list);	
	}
	
	private static void NotifyInventoryDataRecieved(){
		for(int i=0;i<listeners.Count;i++){
			((IShopEventListener)listeners[i]).InventoryDataRecieved();
		}
	}
}
