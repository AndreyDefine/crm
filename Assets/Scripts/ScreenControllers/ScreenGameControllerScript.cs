using UnityEngine;
using System.Collections;

public class ScreenGameControllerScript : Abstract,ScreenControllerToShow {
	public GameObject worldFactoryToLoad;
	
	private GameObject worldFactory=null;
	private ScreenControllerToShow screenControllerToShowWorldFactory;
	
	protected void InitSprites(){
		if(!worldFactory)
		{
			worldFactory=Instantiate(worldFactoryToLoad)as GameObject;
			worldFactory.name=worldFactoryToLoad.name;
			screenControllerToShowWorldFactory=(worldFactory.GetComponent("ScreenControllerToShow") as ScreenControllerToShow);
		}
		screenControllerToShowWorldFactory.ShowOnScreen();
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		InitSprites();
	}
	
	public void HideOnScreen()
	{
		if(screenControllerToShowWorldFactory!=null)
		{
			screenControllerToShowWorldFactory.HideOnScreen();
		}
	}
	
	
}
