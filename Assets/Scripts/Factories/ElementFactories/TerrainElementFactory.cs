using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainElementFactory: AbstractElementFactory{	
	private TerrainTag currentTerrain=null;
	
	private int versionForCoRoutine=0;
	
	public override void ReStart(){
		versionForCoRoutine++;
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
		
		int curversionForCoRoutine=versionForCoRoutine;
		if(terrainsList.Count>0)
		{
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			AbstractTag newterrainToDel=terrainsList[0];
			TerrainTag terrainTag=newterrainToDel.GetComponent("TerrainTag") as TerrainTag;
			List<AbstractTag> AllElements=terrainTag.GetAllElements();
			terrainTag.RemakeAllElementsList();
			yield return null;
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);	
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			yield return null;
			for(int i=0;i<AllElements.Count;i++)
			{
				if(curversionForCoRoutine!=versionForCoRoutine) yield break;
				if(AllElements[i]){
					AllElements[i].DeleteFromUsed();
				}
				yield return null;
			}
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
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
