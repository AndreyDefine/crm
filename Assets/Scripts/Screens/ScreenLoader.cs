using UnityEngine;
using System.Collections;

public class ScreenLoader : MonoBehaviour {
	
	public string firstScreen="";
	
	
	private ArrayList arrayScreens=new ArrayList();
	
	private AbstractScreen ActiveScreen;
	private AbstractScreen Active3DScreen;
	private AbstractScreen PrevActive;

	// Use this for initialization
	void Start () {
		//load first sceene
		LoadScreenByName(firstScreen);
	}
	
	public void LoadPrevScreen()
	{
		if(Active3DScreen&&ActiveScreen)
		{
			ActiveScreen.HideScreen();
			PrevActive.ShowScreen();
			ActiveScreen=PrevActive;
			return;
		}
		
		
		if(ActiveScreen&&PrevActive)
		{
			ActiveScreen.HideScreen();
			PrevActive.ShowScreen();
			ActiveScreen=PrevActive;
			return;
		}
	}
	
	public void ResumeGameScreen()
	{
		GlobalOptions.GetGuiLayer().ResumeTimer();
		if(ActiveScreen)
		{
			PrevActive=ActiveScreen;
			ActiveScreen.HideScreen();
			Debug.Log ("Hide "+ActiveScreen.gameObject.name);
		}
	}

	public void LoadScreenByName(string instr){
		if(instr=="")
		{
			return;
		}
		AbstractScreen curScreenScript;
		GameObject newLoadedScreen=null;
		
		if(ActiveScreen)
		{
			PrevActive=ActiveScreen;
			ActiveScreen.HideScreen();
		}
		
		//ищем в загруженных скринах
		for (int j=0; j<arrayScreens.Count;j++)
		{
			//нашли
			if((arrayScreens[j] as GameObject).transform.name==instr)
			{
				newLoadedScreen=(arrayScreens[j] as GameObject);
				break;
			}
		}
		
		if(!newLoadedScreen)
		{
			newLoadedScreen= Instantiate(Resources.Load("Screens/"+instr)) as GameObject;
			newLoadedScreen.transform.name=instr;
			arrayScreens.Add(newLoadedScreen);
		}			
		curScreenScript=(newLoadedScreen.GetComponent("AbstractScreen") as AbstractScreen);
		//3d сцена
		if(curScreenScript.Game3DScreen)
		{
			Debug.Log ("3dScreen "+instr);
			ActiveScreen=null;
			if(Active3DScreen)
			{
				Active3DScreen.HideObjects();
			}
			Active3DScreen=curScreenScript;
			Active3DScreen.ShowScreen();
		}
		else 
		{
			Debug.Log ("2dScreen "+instr);
			ActiveScreen=curScreenScript;
			ActiveScreen.ShowScreen();
		}
	}
}
