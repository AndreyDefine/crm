using UnityEngine;
using System.Collections;

public class ScreenLoadGameObjects : AbstractScreen {
	
	public GameObject[] objectsToLoad;
	public GameObject[] objectsToShow;
	
	protected ArrayList LoadedObjects=new ArrayList();
	
	protected override void InitSprites()
	{
		GameObject newObject;
		int i;
		for(i=0;i<objectsToLoad.Length;i++)
		{
			newObject=Instantiate (objectsToLoad[i]) as GameObject;
			LoadedObjects.Add(newObject);
		}	
	}
	
	protected override void ShowObjects()
	{
		int i;
		for(i=0;i<objectsToShow.Length;i++)
		{
			(objectsToShow[i].GetComponent("ScreenControllerToShow") as ScreenControllerToShow).ShowOnScreen();
		}	
	}
	
	public override void HideObjects()
	{
		int i;
		for(i=0;i<objectsToShow.Length;i++)
		{
			(objectsToShow[i].GetComponent("ScreenControllerToShow") as ScreenControllerToShow).HideOnScreen();
		}	
	}
	
	
	protected override void RemoveSprites()
	{
		GameObject newObject;
		int i;
		for(i=0;i<LoadedObjects.Count;i++)
		{
			newObject=LoadedObjects[i] as GameObject;
			Destroy(newObject);
		}	
		
		LoadedObjects.Clear();
	}
	
	public void AddLoadedObject(GameObject inObject)
	{
		LoadedObjects.Add(inObject);
	}
	
	public void RemoveLoadedObject(GameObject inObject)
	{
		LoadedObjects.Remove(inObject);
	}

}
