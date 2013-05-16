using UnityEngine;
using System.Collections;

public class BaseScriptWithDialogScript : Abstract,ScreenControllerToShow {
	
	public Dialog dialogPrefab;
	private Dialog dialog = null;
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		dialog = Instantiate(dialogPrefab) as Dialog;
		dialog.singleTransform.parent = singleTransform;
		dialog.singleTransform.localPosition = new Vector3(dialog.singleTransform.localPosition.x, dialog.singleTransform.localPosition.y, 0f);
	}
	
	public void HideOnScreen()
	{
		if(dialog!=null){	
			Destroy(dialog.gameObject);
			dialog = null;
		}
	}
	
	//end Screen Controller To Show Methods
}
