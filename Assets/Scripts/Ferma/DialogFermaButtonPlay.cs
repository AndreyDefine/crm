using UnityEngine;
using System.Collections;

public class DialogFermaButtonPlay: GuiButtonBase {
	public DialogFerma dialogFerma;
	public GameObject initTutorial;
	private GameObject tutorial;
	private FermaLocationPlace place;
	
	override protected void MakeOnTouch(){
		place.Play();
		place.playedOneTime=true;
		if(tutorial)
		{
			Destroy(tutorial);
			tutorial=null;
		}
	}
	
	protected override void Start() {
        base.Start ();
	}
	
	public void ShowTutorialArrowsAndGetPlace(){
		place=dialogFerma.GetFermaLocationPlace();
		if(!place.playedOneTime)
		{
			tutorial=Instantiate(initTutorial) as GameObject;
			tutorial.transform.position=new Vector3(singleTransform.position.x,singleTransform.position.y,-1.1f);
			tutorial.transform.parent=singleTransform;
		}
	}
}
