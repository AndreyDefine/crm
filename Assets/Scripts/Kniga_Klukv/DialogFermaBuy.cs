using UnityEngine;
using System.Collections;

public class DialogFermaBuy : DialogFerma {
	
	public CrmFont priceValue;
	
	public override void SetFactory (Factory factory)
	{
		base.SetFactory (factory);
		priceValue.text = string.Format("{0}",factory.price);
	}
}
