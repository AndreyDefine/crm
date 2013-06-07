using UnityEngine;
using System.Collections;

public class DialogFermaBuy : DialogFerma {
	
	public CrmFont priceValue;
	public GameObject money;
	public CrmFont errorText;
	public GameObject buyButton;
	
	public override void SetFermaLocationPlace (FermaLocationPlace place)
	{
		base.SetFermaLocationPlace (place);
		if(place.needLevel>PersonInfo.personLevel){
			money.SetActive(false);
			errorText.gameObject.SetActive(true);
			errorText.text = string.Format("You need {0} level", place.needLevel);
			buyButton.SetActive(false);
		}else{
			money.SetActive(true);
			priceValue.text = string.Format("{0}", place.price);
			errorText.gameObject.SetActive(false);
			buyButton.SetActive(true);
		}
	}
}
