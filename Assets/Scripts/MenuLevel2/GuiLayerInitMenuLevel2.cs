using UnityEngine;
using System.Collections;

public class GuiLayerInitMenuLevel2 : Abstract {
	
	public GameObject GuiBackButton;
	
	
	
	private GameObject BackButton;
	
	Camera GUIcamera;
	// Use this for initialization
	void Start () {
		GUIcamera = Cameras.GetGUICamera();
		
		InitSprites();
	}
	
	private void InitSprites()
	{
		Vector3 pos;		
		BackButton = (GameObject)Instantiate(GuiBackButton);
		
		pos=new Vector3(GlobalOptions.Vsizex-8,GlobalOptions.Vsizey-8,1);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		pos.x-=BackButton.renderer.bounds.extents.x;
		pos.y-=BackButton.renderer.bounds.extents.y;
		
		BackButton.transform.position=pos;
		
		BackButton.transform.parent=singleTransform;
		
		Debug.Log ("InitSprites");
	}
}
