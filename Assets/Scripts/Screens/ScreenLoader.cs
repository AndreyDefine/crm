using UnityEngine;
using System.Collections;

public class ScreenLoader : MonoBehaviour {
	
	public GameObject[] Screens;
	public string firstScreen="";
	
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
		GameObject newScreen;
		Debug.Log (instr);
		for (int i=0;i<Screens.Length;i++)
		{
			if((Screens[i] as GameObject).name==instr)
			{
				if(ActiveScreen)
				{
					PrevActive=ActiveScreen;
					ActiveScreen.HideScreen();
					Debug.Log ("Hide "+ActiveScreen.gameObject.name);
				}
				newScreen=(Screens[i] as GameObject);
				curScreenScript=(newScreen.GetComponent("AbstractScreen") as AbstractScreen);
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
				break;
			}
		}
	}
}
