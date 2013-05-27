using UnityEngine;
using System.Collections;

public class GlobalGold : Abstract {

	public CrmFont crmFont;
	
	public void SetGold(int gold){
		crmFont.text = string.Format ("{0}", gold);
	}
}
