using UnityEngine;
using System.Collections;

public class DialogFermaButtonPlay: GuiButtonBase {
	public DialogFerma dialogFerma;
	private string screenToShow="ScreenGame";
	protected ScreenLoader screenLoader;
	override protected void MakeOnTouch(){
		Factory curFactory=dialogFerma.GetFactory();
		curFactory.Play();
		
		GlobalOptions.loadingLevel=curFactory.levelToLoad;
		GlobalOptions.PlayingLevelNumber=curFactory.playingLevelNumber;
		GlobalOptions.SavePrefsLastPlayed();
		screenLoader.LoadScreenByName(screenToShow);
		Debug.Log (GlobalOptions.PlayingLevelNumber);
	}
	
	protected override void Start() {
        base.Start ();
		screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
	}
}
