using UnityEngine;
using System.Collections;

public class LoadPlayingLevelButton : LoadLevelButton {	
	public int playingLevelNumber;
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){		
			//LoadMenu2
			GlobalOptions.loadingLevel=levelToLoad;
			//GlobalOptions.PlayingLevelNumber=playingLevelNumber;
			//GlobalOptions.SavePrefsLastPlayed();
			Application.LoadLevel("Preloader"); 
		}

		return isTouchHandled;
	}
}
