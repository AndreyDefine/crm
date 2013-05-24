using UnityEngine;
using System.Collections;

public class ScreenFermaController : Abstract {
	
	public GuiPerson guiPerson;
	
	// Use this for initialization
	void Start () {
		InitSprites();
	}
	
	private void InitSprites()
	{
		guiPerson.InitPerson();
	}
}
