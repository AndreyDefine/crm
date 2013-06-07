using UnityEngine;
using System.Collections;

public class KnigaKlukvControllerScript : Abstract,ScreenControllerToShow {
	
	public bool openAll=false;
	
	public GameObject[] items;
	
	KnigaItem curItem;
	
	public KnigaItem curKnigaItem=null;
	
	protected void EnablePoints(){
		GameObject curObject;
		int i;
		for(i=0;i<items.Length;i++)
		{
			curObject=(items[i] as GameObject);
			curObject.SetActive(false);
			//if(i>curOpened&&!openAll){
			//	curObject.active=false;
			//}
		}	
	}
	
	protected void DisablePoints(){
		if(curKnigaItem)
		{
			curKnigaItem.HideItem();
		}
	}
	
	public void HideCurKnigaItem()
	{
		if(curKnigaItem){
			curKnigaItem.HideItem();
			curKnigaItem=null;
		}		
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		EnablePoints();
	}
	
	public void HideOnScreen()
	{
		DisablePoints();
	}
	
	//end Screen Controller To Show Methods
}
