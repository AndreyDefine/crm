using UnityEngine;
using System.Collections;

public class TerrainElementFactory: AbstractElementFactory{	
	private GameObject currentTerrain=null;
	
	public override void ReStart(){
		base.ReStart();
		currentTerrain=null;
		for(int i=0;i<terrainsListToDel.Count;i++)
		{
			GameObject newterrainToDel=terrainsListToDel[i] as GameObject;
			ArrayList AllElements=(newterrainToDel.GetComponent("TerrainTag") as TerrainTag).GetAllElements();
			AllElements.Clear();	
		}
	}
	
	public override void DeleteOneFirstTerrain()
	{
		if(terrainsList.Count>0)
		{
			
			GameObject newterrainToDel=terrainsList[0] as GameObject;
			ArrayList AllElements=(newterrainToDel.GetComponent("TerrainTag") as TerrainTag).GetAllElements();
			for(int i=0;i<AllElements.Count;i++)
			{
				AbstractTag curTag;
				curTag=(AllElements[i] as GameObject).GetComponent("AbstractTag") as AbstractTag;
				if(curTag){
					curTag.DeleteFromUsed();
				}
			}
			
			AllElements.Clear();
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);		
		}
	}
	
	public override void PutToFirstState(GameObject newTerrain){
		newTerrain.transform.position=new Vector3(-200,-200,-200);
		newTerrain.transform.rotation=Quaternion.identity;
		//MakeInactiveObjectsActive
		TerrainTag terrainTag=newTerrain.GetComponent("TerrainTag") as TerrainTag;
		
		terrainTag.MakeAllActive();
		terrainTag.RecalculateRoadPathArray();
		if(terrainsList.Count>0){
			TerrainTag terrainTagPrev=(terrainsList[terrainsList.Count-1] as GameObject).GetComponent("TerrainTag") as TerrainTag;
			terrainTag.SetPrev(terrainTagPrev);
			terrainTagPrev.SetNext(terrainTag);
		}
	}
	
	public GameObject GetCurrentTerrainForZ()
	{
		if(!currentTerrain)
		{
			currentTerrain=terrainsList[0] as GameObject;
			TerrainTag terrainTag=currentTerrain.GetComponent<TerrainTag>();
			terrainTag.SetCurDotIndexAndCurPos(1,0);
			
			//GlobalOptions.GetPlayerScript().PlaceCharacterFirstly(terrainTag.GetXandYandAngleSmexForZ(new Vector3(0,0,0.001f)));
		}
	
		return currentTerrain;
	}
	
	public void SetNextCurrentTerrain(GameObject interrain)
	{
		currentTerrain=interrain;
	}
	
	//get xsmex
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition)
	{
		GameObject curTerrain=null;
		TerrainTag terrainTag;
		Vector3 returnXandYandAngle=new Vector3(0f,0f,0f);
		
		curTerrain=GetCurrentTerrainForZ();
		
		if(curTerrain){
			terrainTag=curTerrain.GetComponent("TerrainTag") as TerrainTag;
			returnXandYandAngle=terrainTag.GetXandYandAngleSmexForZ(inposition,false);
		}
		else
		{
			Debug.Log ("TerrainTag Not Found");
		}
		
		return returnXandYandAngle;
	}
	
	public float GetCurTerrainCenter(){
		float returny=GetCurrentTerrainForZ().transform.position.y;
		
		return returny;
	}
}
