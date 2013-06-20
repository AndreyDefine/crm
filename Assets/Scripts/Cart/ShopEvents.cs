using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopEvents{
	public static string COIN_PACK_1 = "coin_pack_1";
	public static string COIN_PACK_2 = "coin_pack_2";
	public static string COIN_PACK_3 = "coin_pack_3";
	public static string COIN_PACK_4 = "coin_pack_4";
	
	public static string GOLD_PACK_1 = "gold_pack_1";
	public static string GOLD_PACK_2 = "gold_pack_2";
	public static string GOLD_PACK_3 = "gold_pack_3";
	public static string GOLD_PACK_4 = "gold_pack_4";
	
	public static Dictionary<string,PurchaseData> inventory = null;
	
	private static ArrayList listeners = new ArrayList();
	
	public static IInap inap;
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
		//do nothing
	}
	
	public static void ConsumePurchaseSucceeded(string id){
		if(id.Equals(COIN_PACK_1)){
			PersonInfo.AddCoins(1000);
		}
	}
	
	
	public static void PurchaseFailed(string error){
		ShowErrorDialog(error);
	}
	
	public static void ConsumePurchaseFailed(string error){
		ShowErrorDialog(error);
	}
	
	
	public static void Purchase(PurchaseData purchaseData){
		inap.Purchase(purchaseData);
	}
	
	public static void Consume(PurchaseData purchaseData){
		inap.Consume(purchaseData);
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
