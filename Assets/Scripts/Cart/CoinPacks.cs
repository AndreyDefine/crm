using UnityEngine;
using System.Collections;

public class CoinPacks : Abstract,IShopEventListener {

	public PurchaseItem purchaseItemPrefab;
	private PurchaseItem coinPack1;
	private PurchaseItem coinPack2;
	private PurchaseItem coinPack3;
	private PurchaseItem coinPack4;
	void Start(){
		coinPack1 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack1.singleTransform.parent = singleTransform;
		coinPack1.singleTransform.localPosition = new Vector3(0f,0.75f,0f);
		
		coinPack2 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack2.singleTransform.parent = singleTransform;
		coinPack2.singleTransform.localPosition = new Vector3(0f,0.25f,0f);
		
		coinPack3 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack3.singleTransform.parent = singleTransform;
		coinPack3.singleTransform.localPosition = new Vector3(0f,-0.25f,0f);
		
		coinPack4 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack4.singleTransform.parent = singleTransform;
		coinPack4.singleTransform.localPosition = new Vector3(0f,-0.75f,0f);
		
		ShopEvents.AddListener(this);
		if(ShopEvents.inventory!=null){
			InventoryDataRecieved();
		}
	}
	
	void OnDestroy(){
		ShopEvents.RemoveListener(this);	
	}
	
	public void InventoryDataRecieved ()
	{
		coinPack1.setPurchaseData(ShopEvents.inventory[ShopEvents.COIN_PACK_1]);	
		coinPack2.setPurchaseData(ShopEvents.inventory[ShopEvents.COIN_PACK_2]);
		coinPack3.setPurchaseData(ShopEvents.inventory[ShopEvents.COIN_PACK_3]);
		coinPack4.setPurchaseData(ShopEvents.inventory[ShopEvents.COIN_PACK_4]);
	}
}
