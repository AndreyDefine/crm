using UnityEngine;
using System.Collections;

public class DialogFermaPlay : DialogFerma {
	
	public CrmFont crmFont;
	
	public override void SetFermaLocationPlace (FermaLocationPlace place)
	{
		base.SetFermaLocationPlace (place);
		crmFont.text = place.factoryName;
	}	
}
