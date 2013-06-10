using UnityEngine;
using System.Collections;

public class TerrainElementFactory: AbstractElementFactory{	
	private TerrainTag currentTerrain=null;
	
	public override void ReStart(){
		base.ReStart();
		currentTerrain=null;
		for(int i=0;i<terrainsListToDel.Count;i++)
		{
			Abstract newterrainToDel=terrainsListToDel[i] as Abstract;
			ArrayList AllElements=(newterrainToDel.GetComponent("TerrainTag") as TerrainTag).GetAllElements();
			AllElements.Clear();	
		}
	}
	
	public override void DeleteOneFirstTerrain()
	{
		if(terrainsList.Count>0)
		{
			
			Abstract newterrainToDel=terrainsList[0] as Abstract;
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
	
	public override void PutToFirstState(AbstractTag newTerrain){
		newTerrain.singleTransform.position=new Vector3(-9999,-9999,-9999);
		newTerrain.singleTransform.rotation=Quaternion.identity;
		//MakeInactiveObjectsActive
		TerrainTag terrainTag=newTerrain.GetComponent("TerrainTag") as TerrainTag;
		
		terrainTag.ParseObstacleSets();
		terrainTag.MakeAllActive();
		if(!GlobalOptions.flagOnlyFizik)
		{
			terrainTag.RecalculateRoadPathArray();
			if(terrainsList.Count>0){
				TerrainTag terrainTagPrev=(terrainsList[terrainsList.Count-1] as Abstract).GetComponent("TerrainTag") as TerrainTag;
				terrainTag.SetPrev(terrainTagPrev);
				terrainTagPrev.SetNext(terrainTag);
			}
		}
	}
	
	public TerrainTag GetCurrentTerrainForZ()
	{
		if(!currentTerrain)
		{
			currentTerrain=(terrainsList[0] as Abstract).GetComponent<TerrainTag>();
			if(!GlobalOptions.flagOnlyFizik)
			{
				currentTerrain.SetCurDotIndexAndCurPos(1,0);
			}
			
			//GlobalOptions.GetPlayerScript().PlaceCharacterFirstly(terrainTag.GetXandYandAngleSmexForZ(new Vector3(0,0,0.0001f),false));
		}	
		return currentTerrain;
	}
	
	public void SetNextCurrentTerrain(TerrainTag interrain)
	{
		currentTerrain=interrain;
	}
	
	public void  SetNextCurrentTerrainNext()
	{
		SetNextCurrentTerrain(terrainsList[1]as TerrainTag);
	}
	
	//get xsmex
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition)
	{
		TerrainTag curTerrain=null;
		Vector3 returnXandYandAngle=new Vector3(0f,0f,0f);
		
		curTerrain=GetCurrentTerrainForZ();
		
		if(curTerrain){
			returnXandYandAngle=curTerrain.GetXandYandAngleSmexForZ(inposition,false);
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
