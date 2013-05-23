using UnityEngine;
using System.Collections;

public class DialogFermaBuy : DialogFerma {
	
	public CrmFont priceValue;
	public GameObject money;
	public CrmFont errorText;
	public GameObject buyButton;
	
	public override void SetFactory (Factory factory)
	{
		base.SetFactory (factory);
		if(factory.needLevel>PersonInfo.personLevel){
			money.SetActiveRecursively(false);
			errorText.gameObject.SetActiveRecursively(true);
			errorText.text = string.Format("You need {0} level", factory.needLevel);
			buyButton.SetActiveRecursively(false);
		}else{
			money.SetActiveRecursively(true);
			priceValue.text = string.Format("{0}", factory.price);
			errorText.gameObject.SetActiveRecursively(false);
			buyButton.SetActiveRecursively(true);
		}
	}
}
