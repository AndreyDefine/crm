using UnityEngine;
using System.Collections;

public class LoadPlayingLevelButtonLastPlayed : LoadLevelButton {	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){		
			//GlobalOptions.GetPrefsLastPlayed();
			//Application.LoadLevel("Preloader"); 
		}

		return isTouchHandled;
	}
}
