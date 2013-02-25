using UnityEngine;
using System.Collections;

public class LoadLevelButton : GuiButton {
	public string levelToLoad="";
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			Debug.Log (levelToLoad);
			//LoadMenu2
			GlobalOptions.loadingLevel=levelToLoad;
			Application.LoadLevel("Preloader"); 
		}

		return isTouchHandled;
	}
}
