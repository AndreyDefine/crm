using UnityEngine;
using System.Collections;

public class Preloader : Abstract {
	public float extraDelay;
	public GameObject GuiPersantage;
	public bool ResetAllSettings;
	
	private GameObject Persantage;	
	Camera GUIcamera;
    public AsyncOperation async = null;
	bool isLoading=false;
	
	private float persantage=0;
	private float curTime;
	private string levelName = "E00L01";
	
    IEnumerator Start() {
		if(ResetAllSettings)
		{
			PlayerPrefs.DeleteAll();
		}
		levelName=GlobalOptions.loadingLevel;
		curTime=0;
		GUIcamera = Cameras.GetGUICamera(); 
		
		Application.targetFrameRate = 300;
		
		InitSprites();
		if(GlobalOptions.UnityPro){
			isLoading=true;
			async = Application.LoadLevelAsync(levelName);
		}
		yield return async; 	
    }

    void Update() {
		if(!GlobalOptions.UnityPro&&Time.time>extraDelay){
			if(!isLoading){
				Application.LoadLevel(levelName); 
			}
			isLoading=true;
		}
		
		if(Time.time-curTime>0.3){
			curTime=Time.time;
			if(!GlobalOptions.UnityPro){
				UpdatePersantageSimulate();
			}
			else{
				UpdatePersantage();
			}
		}
    }
    
	private void UpdatePersantage(){
		tk2dTextMesh textMesh;
		textMesh = Persantage.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{000}%", (int)(async.progress*100));
		textMesh.Commit();
		//PosPersantage();
	}
	
	private void UpdatePersantageSimulate(){
		persantage+=3;
		persantage=persantage>=100?99:persantage;
		tk2dTextMesh textMesh;
		textMesh = Persantage.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{000}%", persantage);
		textMesh.Commit();
		//PosPersantage();
	}
	
	private void InitSprites(){
		Persantage=(GameObject)Instantiate(GuiPersantage);
		PosPersantage();
	}
	
	private void PosPersantage(){
		Vector3 pos;
		pos=new Vector3(606,75,-10f);
		pos=GlobalOptions.NormalisePos(pos);
		pos=GUIcamera.ScreenToWorldPoint(pos);
		//pos.x-=Persantage.renderer.bounds.size.x;
		
		Persantage.transform.position=pos;
	}
    
}
