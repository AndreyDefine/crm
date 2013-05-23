using UnityEngine;
using System.Collections;

public class GlobalMoney : Abstract {

	public CrmFont crmFont;
	
	public void SetMoney(int money){
		crmFont.text = string.Format ("{0}", money);
	}
}
