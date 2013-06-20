using UnityEngine;
using System.Collections;

public class GlobalGold : Abstract {
	
	void OnEnable()
	{
		SetGold(PersonInfo.gold);
	}
	
	public CrmFont crmFont;
	private bool animationAdded = false;
	
	public void SetGold(int gold){
		crmFont.text = string.Format ("{0}", gold);
		if(!animationAdded){
			AnimationFactory.Attention(this,0.7f,1.05f,"Attention");
			animationAdded = true;
		}else{
			animation.Play("Attention");
		}
	}
}
