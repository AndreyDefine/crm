using UnityEngine;
using System.Collections;

public class DialogFermaButtonBuy: GuiButtonBase {
	public DialogFerma dialogFerma;
	public GameObject initTutorial;
	private GameObject tutorial;
	override protected void MakeOnTouch(){
		dialogFerma.GetFermaLocationPlace().Buy();
		if(tutorial)
		{
			Destroy(tutorial);
			tutorial=null;
		}
	}
	
	protected override void Start ()
	{
		ShowTutorialArrows();
		base.Start ();
	}
	
	public void ShowTutorialArrows(){
		tutorial=Instantiate(initTutorial) as GameObject;
		tutorial.transform.parent=singleTransform;
		tutorial.transform.localScale=new Vector3(1f,1f,1f);
		tutorial.transform.localPosition=new Vector3(singleTransform.position.x,singleTransform.position.y,-1.1f);
	}
}
