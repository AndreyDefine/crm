using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainElementFactory: AbstractElementFactory{	
	private TerrainTag currentTerrain=null;
	
	public override void ReStart(){
		base.ReStart();
		currentTerrain=null;
		for(int i=0;i<terrainsListToDel.Count;i++)
		{
			AbstractTag newterrainToDel=terrainsListToDel[i];
			List<AbstractTag> AllElements=(newterrainToDel.GetComponent("TerrainTag") as TerrainTag).GetAllElements();
			AllElements.Clear();	
		}
	}
	
	public override void DeleteOneFirstTerrain()
	{
		StartCoroutine(ClearTerrainFactory());	
	}
		
	private IEnumerator ClearTerrainFactory(){	
		
		if(terrainsList.Count>0)
		{
			
			AbstractTag newterrainToDel=terrainsList[0];
			TerrainTag terrainTag=newterrainToDel.GetComponent("TerrainTag") as TerrainTag;
			List<AbstractTag> AllElements=terrainTag.GetAllElements();
			terrainTag.RemakeAllElementsList();
			yield return null;
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);	
			yield return null;
			for(int i=0;i<AllElements.Count;i++)
			{
				if(AllElements[i]){
					AllElements[i].DeleteFromUsed();
				}
				if(i%5==0)yield return null;
			}
			
			AllElements.Clear();
			AllElements=null;
		}
		yield return null;
	}
	
	public override void PutToFirstState(AbstractTag newTerrain){
		newTerrain.singleTransform.position=new Vector3(-9999,-9999,-9999);
		newTerrain.singleTransform.rotation=Quaternion.identity;
		//MakeInactiveObjectsActive
		TerrainTag terrainTag=newTerrain.GetComponent("TerrainTag") as TerrainTag;
		
		terrainTag.ParseObstacleSets();
		//terrainTag.MakeAllActive();
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
