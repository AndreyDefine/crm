using UnityEngine;
using System.Collections;

public class GuiLayerInitMenuLevel2 : Abstract {
	
	Camera GUIcamera;
	// Use this for initialization
	void Start () {
		GUIcamera = Cameras.GetGUICamera();
		
		InitSprites();
	}
	
	private void InitSprites()
	{
	}
}
