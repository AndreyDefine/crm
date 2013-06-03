using UnityEngine;
using System.Collections;

public class AbstractElementFactory: Abstract{
	public string pathInResources="";
	public bool flagGenerate;
	public int NumberOfTerrainsToDel;
	
	public string preloadNames="";
	
	private string []terrain1=null;
	
	private GameObject terrainToDel1,terrainToDel2;
	protected ArrayList terrainsList=new ArrayList();
	protected ArrayList terrainsListToDel=new ArrayList();
	
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
		if(preloadNames!="")
		{
			parseTerrainNames();
		}
	}
	
	public void parseTerrainNames()
	{
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=preloadNames.Split(separator);
		terrain1=names;
	}
	
	public virtual void ReStart(){
		int i;
		Vector3 newPos=new Vector3(-9999,-9999,-9999);
		for(i=0;i<terrainsListToDel.Count;i++){
			(terrainsListToDel[i]as GameObject).transform.position=newPos;
		}
		for(i=0;i<terrainsList.Count;i++){
			(terrainsList[i]as GameObject).transform.position=newPos;
			terrainsListToDel.Add(terrainsList[i]);
		}
		
		terrainsList.Clear();
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
		if(terrain1==null&&preloadNames!="")
		{
			parseTerrainNames();
		}
		for(i=0;i<terrain1.Length;i++)
		{
			//нашли
			if(terrain1[i]==instr){
				newTerrain	= Instantiate(Resources.Load(pathInResources+"/"+terrain1[i])) as GameObject;
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
			if(terrain1==null&&preloadNames!="")
			{
				parseTerrainNames();
			}
			
			if(preloadNames=="")
			{
				int RandIndex=Random.Range(0,terrainsList.Count);
				newTerrain	= Instantiate(terrainsList[RandIndex] as GameObject) as GameObject;
				newTerrain.name=(terrainsList[RandIndex] as GameObject).name;
			}
			else
			{
				int RandIndex=Random.Range(0,terrain1.Length);
				newTerrain	= Instantiate(Resources.Load(pathInResources+"/"+terrain1[RandIndex])) as GameObject;
				newTerrain.name=terrain1[RandIndex];
			}

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
		if(terrain1==null&&preloadNames!="")
		{
			parseTerrainNames();
		}
		GameObject newTerrain=null;
		for (int i=0; i<terrain1.Length;i++){
			newTerrain	= Instantiate(Resources.Load(pathInResources+"/"+terrain1[i])) as GameObject;
			newTerrain.name=terrain1[i];
			addTagToObject(newTerrain);	
			newTerrain.transform.position=new Vector3(-9999,-9999,-9999);
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
			if(Resources.Load(pathInResources+"/"+instr)==null)
			{
					//Debug.Log (instr);
			}
			else
			{
					newTerrain	= Instantiate(Resources.Load(pathInResources+"/"+instr)) as GameObject;
			}
			
			if(newTerrain)
			{
				addTagToObject(newTerrain);	
				PutToFirstState(newTerrain);
				newTerrain.name=instr;
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
