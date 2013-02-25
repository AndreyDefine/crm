using UnityEngine;
using System.Collections;

public class ScreenPauseControllerScript : Abstract,ScreenControllerToShow {
	
	public GameObject GuiButtonReplay;
	
	private GameObject ButtonReplay;
	
	protected void InitSprites(){
		//do nothing
		/*
		if(!ButtonReplay){
			Vector2 pos;
			
			ButtonReplay = (GameObject)Instantiate(GuiButtonReplay);
		
			pos=new Vector3(GlobalOptions.Vsizex-5,GlobalOptions.Vsizey-14,1);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
		
			pos.x-=ButtonReplay.renderer.bounds.extents.x;
			pos.y-=ButtonReplay.renderer.bounds.extents.y;
		
			ButtonReplay.transform.position=pos;
			ButtonReplay.transform.parent=gameObject.transform;
		}*/
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		InitSprites();
	}
	
	public void HideOnScreen()
	{
		//do nothing
	}
	
	//end Screen Controller To Show Methods
}
