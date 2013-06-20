using UnityEngine;
using System.Collections;

public class CoinPacks : Abstract,IShopEventListener {

	public PurchaseItem purchaseItemPrefab;
	private PurchaseItem coinPack1;
	private PurchaseItem coinPack2;
	private PurchaseItem coinPack3;
	private PurchaseItem coinPack4;
	private PurchaseItem coinPack5;
	void Start(){
		coinPack1 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack1.purchaseItemName.text = "7 500";
		coinPack1.singleTransform.parent = singleTransform;
		coinPack1.singleTransform.localPosition = new Vector3(0f,1f,0f);
		
		coinPack2 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack2.purchaseItemName.text = "30 000";
		coinPack2.singleTransform.parent = singleTransform;
		coinPack2.singleTransform.localPosition = new Vector3(0f,0.5f,0f);
		
		coinPack3 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack3.purchaseItemName.text = "150 000";
		coinPack3.singleTransform.parent = singleTransform;
		coinPack3.singleTransform.localPosition = new Vector3(0f,0f,0f);
		
		coinPack4 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack4.purchaseItemName.text = "300 000";
		coinPack4.singleTransform.parent = singleTransform;
		coinPack4.singleTransform.localPosition = new Vector3(0f,-0.5f,0f);
		
		coinPack5 = Instantiate(purchaseItemPrefab) as PurchaseItem;
		coinPack5.purchaseItemName.text = "500 000";
		coinPack5.singleTransform.parent = singleTransform;
		coinPack5.singleTransform.localPosition = new Vector3(0f,-1f,0f);
		
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
		coinPack5.setPurchaseData(ShopEvents.inventory[ShopEvents.COIN_PACK_5]);
	}
}
