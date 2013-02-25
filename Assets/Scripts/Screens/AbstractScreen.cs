using UnityEngine;
using System.Collections;

public class AbstractScreen : Abstract {
	
	public bool makeInactive=true;
	public bool Game3DScreen=false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void ShowScreen()
	{
		if(makeInactive){
			gameObject.SetActiveRecursively(true);
		}
		InitSprites();
		ShowObjects();
	}
	
	public virtual void HideScreen()
	{
		RemoveSprites();
		HideObjects();
		if(makeInactive){
			gameObject.SetActiveRecursively(false);
		}
	}
	
	protected virtual void InitSprites()
	{
		//do nothing
	}
	
	protected virtual void ShowObjects()
	{
		//do nothing
	}
	
	
	protected virtual void RemoveSprites()
	{
		//do nothing
	}
	
	public virtual void HideObjects()
	{
		//do nothing
	}
	
}
