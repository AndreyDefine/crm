using UnityEngine;
using System.Collections;

public class DialogFermaPlay : DialogFerma {
	
	public CrmFont crmFont;
	public CrmFont factoryMissionsHeader; 
	public FermaMissions fermaMissions;
	
	public DialogFermaButtonPlay buttonPlay;
	
	public override void SetFermaLocationPlace (FermaLocationPlace place)
	{
		base.SetFermaLocationPlace (place);
		fermaMissions.SetFermaLocationPlace(place);
		crmFont.text = place.factoryName;
		factoryMissionsHeader.text = place.factoryName+" missions";
		buttonPlay.ShowTutorialArrowsAndGetPlace();
	}	
	
}
