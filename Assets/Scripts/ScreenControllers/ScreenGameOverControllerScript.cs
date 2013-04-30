using UnityEngine;
using System.Collections;

public class ScreenGameOverControllerScript : Abstract,ScreenControllerToShow {
	
	public GameObject GuiButtonReplay;
	public GameObject GuiScore;
	public GameObject GuiMoney;
	
	private GameObject ButtonReplay;
	
	protected void InitSprites(){
		Vector3 pos;
		if(!ButtonReplay){
			
			ButtonReplay = (GameObject)Instantiate(GuiButtonReplay);
		
			pos=new Vector3(5,GlobalOptions.Vsizey-5,1);
			pos=GlobalOptions.NormalisePos(pos);
			pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
		
			pos.x+=ButtonReplay.renderer.bounds.extents.x;
			pos.y-=ButtonReplay.renderer.bounds.extents.y;
		
			ButtonReplay.transform.position=pos;
			ButtonReplay.transform.parent=gameObject.transform;
		}
		
		UpdateScore();
		UpdateMoney();
			
	}
	
	private void UpdateScore()
	{
		int score=GlobalOptions.GetGuiLayer().GetScore();
		tk2dTextMesh textMesh;
		textMesh = GuiScore.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:000000000}", score);
		textMesh.Commit();
	}
	
	private void UpdateMoney()
	{
		int money=GlobalOptions.GetGuiLayer().GetMoney();
		tk2dTextMesh textMesh;
		textMesh = GuiMoney.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:00000000}", money);
		textMesh.Commit();
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
