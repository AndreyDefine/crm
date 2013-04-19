using UnityEngine;
using System.Collections;

public class AbstractElementFactory: Abstract{
	public bool flagGenerate;
	public Vector3 initialPos;
	public GameObject[] terrain1;
	public int NumberOfTerrainsToDel;
	
	private GameObject terrainToDel1,terrainToDel2;
	protected ArrayList terrainsList=new ArrayList();
	protected ArrayList terrainsListToDel=new ArrayList();
	
	private Vector3 smexPos;
	
	public GameObject GetLastObject(){
		if(terrainsList.Count!=0)
		{
			return terrainsList[0] as GameObject;
		}
		else{
			return null;
		}
	}
	
	public GameObject GetLastAddedObject(){
		if(terrainsList.Count!=0)
		{
			return terrainsList[terrainsList.Count-1] as GameObject;
		}
		else{
			return null;
		}
	}
	
	void Start(){
		smexPos=initialPos;
	}
	
	public virtual void ReStart(){
		int i;
		Vector3 newPos=new Vector3(-200,-200,-200);
		for(i=0;i<terrainsListToDel.Count;i++){
			(terrainsListToDel[i]as GameObject).transform.position=newPos;
		}
		for(i=0;i<terrainsList.Count;i++){
			(terrainsList[i]as GameObject).transform.position=newPos;
			terrainsListToDel.Add(terrainsList[i]);
		}
		
		terrainsList.Clear();
	}
	
	public virtual Vector3 GetInitialPos(){
		return smexPos;
	}
	
	public virtual void DeleteOneFirstTerrain()
	{
		if(terrainsList.Count>0)
		{
			GameObject newterrainToDel=terrainsList[0] as GameObject;
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);
		}
	}
	
	public void DeleteCurrent(GameObject inObject){
		terrainsList.Remove(inObject);			
		terrainsListToDel.Add(inObject);
		inObject.transform.parent=null;
	}
	
	public void AddExtraObjectInPull()
	{
		GetNewObject();
	}
	
	public void AddExtraObjectInPullWithName(string instr){
		GameObject newTerrain;
		int i;
		for(i=0;i<terrain1.Length;i++)
		{
			//нашли
			if((terrain1[i] as GameObject).name==instr){
				newTerrain	= Instantiate (terrain1[i]) as GameObject;
				addTagToObject(newTerrain);	
				terrainsListToDel.Add(newTerrain);
				newTerrain.name=instr;
				break;
			}
		}	
	}
	
	public virtual void PutToFirstState(GameObject newTerrain){
		newTerrain.transform.position=new Vector3(-200,-200,-200);
		newTerrain.transform.rotation=Quaternion.identity;
		newTerrain.GetComponent<AbstractTag>().ReStart();
	}
	
	public virtual GameObject GetNewObject(){
		GameObject newTerrain=null;
		if(terrainsListToDel.Count>0){
			int RandIndex=Random.Range(0,terrainsListToDel.Count);
			newTerrain=terrainsListToDel[RandIndex] as GameObject;
			terrainsListToDel.Remove(newTerrain);		
			//put object to first state
			PutToFirstState(newTerrain);
		}else
		{
			//Debug.Log ("Instantiate");
			int RandIndex=Random.Range(0,terrain1.Length);
			newTerrain	= Instantiate (terrain1[RandIndex]) as GameObject;
			newTerrain.name=(terrain1[RandIndex] as GameObject).name;
			addTagToObject(newTerrain);	
			PutToFirstState(newTerrain);
		}
		terrainsList.Add(newTerrain);
		return newTerrain;
	}
	
	public virtual void DestroyPullObjects()
	{
		GameObject newTerrain=null;
		for (int i=0; i<terrainsListToDel.Count;i++){
			newTerrain	= terrainsListToDel[i] as GameObject;
			Destroy(newTerrain);
		}
		terrainsListToDel.Clear();
		
		for (int i=0; i<terrainsList.Count;i++){
			newTerrain	= terrainsList[i] as GameObject;
			Destroy(newTerrain);
		}
		terrainsList.Clear();
	}
	
	//add all objects into pull
	public virtual void PreloadPullObjects()
	{
		GameObject newTerrain=null;
		for (int i=0; i<terrain1.Length;i++){
			newTerrain	= Instantiate (terrain1[i]) as GameObject;
			newTerrain.name=(terrain1[i] as GameObject).name;
			addTagToObject(newTerrain);	
			newTerrain.transform.position=new Vector3(-200,-200,-200);
			terrainsListToDel.Add (newTerrain);
		}
	}
	
	public virtual GameObject GetNewObjectWithName(string instr){
		GameObject newTerrain=null;
		int i;
		//ищем в пуле
		if(terrainsListToDel.Count>0){
			for(i=0;i<terrainsListToDel.Count;i++)
			{
				//нашли
				if((terrainsListToDel[i] as GameObject).name==instr){
					newTerrain=terrainsListToDel[i] as GameObject;
					terrainsListToDel.Remove(newTerrain);
					PutToFirstState(newTerrain);
					//Debug.Log ("Loaded "+instr);
					break;
				}
			}
		}
		//ничего не нашли
		if(!newTerrain)
		{
			Debug.Log ("Instantiate");
			for(i=0;i<terrain1.Length;i++)
			{
				//нашли
				if((terrain1[i] as GameObject).name==instr){
					newTerrain	= Instantiate (terrain1[i]) as GameObject;
					addTagToObject(newTerrain);	
					PutToFirstState(newTerrain);
					newTerrain.name=instr;
					break;
				}
			}	
		}
		if(newTerrain)
		{
			terrainsList.Add(newTerrain);
		}
		return newTerrain;
	}
	
	public virtual void addTagToObject(GameObject newTerrain){
		newTerrain.AddComponent("AbstractTag");
		AbstractTag curTag;
		curTag=newTerrain.GetComponent("AbstractTag") as AbstractTag;
		curTag.addFactory(this);
	}
}
