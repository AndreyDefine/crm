using UnityEngine;
using System.Collections;

public class GlobalMoney : Abstract {

	public CrmFont crmFont;
	private bool animationAdded = false;
	
	public void SetMoney(int money){
		crmFont.text = string.Format ("{0}", money);
		if(!animationAdded){
			AnimationFactory.Attention(this,0.7f,1.05f,"Attention");
			animationAdded = true;
		}else{
			animation.Play("Attention");
		}
	}
}
