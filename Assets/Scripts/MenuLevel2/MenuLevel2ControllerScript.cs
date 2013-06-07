using UnityEngine;
using System.Collections;

public class MenuLevel2ControllerScript : Abstract,ScreenControllerToShow {
	
	public bool openAll=false;
	
	public GameObject[] levelPoints;
	public GameObject[] levelMasks;
	
	protected void EnablePoints(){
		int curOpened=GlobalOptions.GetCurLevelOpened();
		GameObject curObject;
		int i;
		for(i=0;i<levelPoints.Length;i++)
		{
			curObject=(levelPoints[i] as GameObject);
			if(i>curOpened&&!openAll){
				curObject.SetActive(false);
			}
		}	

		
		//Masks
		for(i=0;i<levelMasks.Length;i++)
		{
			curObject=(levelMasks[i] as GameObject);
			if((curObject.GetComponent("MaskTag")as MaskTag).playingLevelNumber<curOpened||openAll){
				curObject.SetActive(false);
			}
		}
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		EnablePoints();
	}
	
	public void HideOnScreen()
	{
		//do nothing
	}
	
	//end Screen Controller To Show Methods
}
