using UnityEngine;
using System.Collections;

public class TerrainElementFactory: AbstractElementFactory{		
	public override void addTagToObject(GameObject newTerrain){
		//do nothing
	}
	
	public override void ReStart(){
		base.ReStart();
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
	
	public GameObject GetCurrentTerrainForZ(float inz)
	{
		GameObject terrainToTest;
		GameObject curTerrain=null;
		TerrainTag terrainTag;
		float terz=0f;
		
		for(int i=0;i<terrainsListToDel.Count&&!curTerrain;i++){
			terrainToTest=terrainsListToDel[i] as GameObject;
			terz=terrainToTest.transform.position.z;
			terrainTag=terrainToTest.GetComponent("TerrainTag") as TerrainTag;
			if(inz<=terrainTag.sizeOfPlane/2+terz&&inz>=terz-terrainTag.sizeOfPlane/2)
			{
				//нашли то что искали
				curTerrain=terrainToTest;
			}
		}
		
		
		for(int i=0;i<terrainsList.Count&&!curTerrain;i++){
			terrainToTest=terrainsList[i] as GameObject;
			terz=terrainToTest.transform.position.z;
			terrainTag=terrainToTest.GetComponent("TerrainTag") as TerrainTag;
			if(inz<=terrainTag.sizeOfPlane/2+terz&&inz>=terz-terrainTag.sizeOfPlane/2)
			{
				//нашли то что искали
				curTerrain=terrainToTest;
			}
		}
		
		return curTerrain;
	}
	
	//get xsmex
	public Vector3 GetXandYandAngleSmexForZ(float inz)
	{
		GameObject curTerrain=null;
		TerrainTag terrainTag;
		float terz=0f;
		Vector3 returnXandYandAngle=new Vector3(0f,0f,0f);
		
		curTerrain=GetCurrentTerrainForZ(inz);
		
		if(curTerrain){
			terz=curTerrain.transform.position.z;
			terrainTag=curTerrain.GetComponent("TerrainTag") as TerrainTag;
			returnXandYandAngle=terrainTag.GetXandYandAngleSmexForZ(inz-terz);
		}
		
		return returnXandYandAngle;
	}
}
