using UnityEngine;
using System.Collections;

public class Slot : Abstract{
	
	public int coin;
	public int gold;
	public bool initBought = false;
	
	private string slotBoughtTag{
		get{
			return this.name+"slot_bought";
		}
	}
	private bool _bought;	
	public bool bought {
        get {
            return _bought;
        }
        private set {
            _bought = value;
			PlayerPrefs.SetInt(slotBoughtTag,value?1:0);
        }
    }
	
	public void Init(){
		_bought = PlayerPrefs.GetInt (slotBoughtTag, 0) != 0||initBought;
	}
	
	public void BuySlot(){
		if(PersonInfo.TryToBuy(coin,gold)){
			bought = true;
		}
	}
}
