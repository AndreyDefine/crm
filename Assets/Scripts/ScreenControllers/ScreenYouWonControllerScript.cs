using UnityEngine;
using System.Collections;

public class ScreenYouWonControllerScript : Abstract,ScreenControllerToShow {
	
	public GameObject GuiScore;
	public GameObject GuiHP;
	public GameObject GuiTime;
	
	protected void InitSprites(){
		UpdatePoints();
		UpdateHP();
		UpdateTime();
			
	}
	
	private void UpdatePoints()
	{
		int points=GlobalOptions.GetGuiLayer().GetPoints();
		tk2dTextMesh textMesh;
		textMesh = GuiScore.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:000000000}", points);
		textMesh.Commit();
	}
	
	private void UpdateTime()
	{
		float nullTime=GlobalOptions.GetGuiLayer().GetTime();
		tk2dTextMesh textMesh;
		textMesh = GuiTime.GetComponent<tk2dTextMesh>();
		int min,sec;
		min=(int)nullTime/60;
		sec=(int)nullTime % 60;
		textMesh.text = string.Format ("{0:00}:{1:00}", min,sec);
		textMesh.Commit();
	}
	
	private void UpdateHP()
	{
		int hp=GlobalOptions.GetGuiLayer().GetHP();
		tk2dTextMesh textMesh;
		textMesh = GuiHP.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:00}", hp);
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
