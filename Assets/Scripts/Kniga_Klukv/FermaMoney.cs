using UnityEngine;
using System.Collections;

public class FermaMoney : Abstract {

	public CrmFont crmFont;
	
	void Start(){
		SetMoney(PersonInfo.coins);
	}
	
	public void SetMoney(int money){
		crmFont.text = string.Format ("{0}", money);
	}
}
