using UnityEngine;
using System.Collections;

public class DialogFermaPlay : DialogFerma {
	
	public CrmFont crmFont;
	
	public override void SetFactory (Factory factory)
	{
		base.SetFactory (factory);
		crmFont.text = factory.name;
	}	
}
