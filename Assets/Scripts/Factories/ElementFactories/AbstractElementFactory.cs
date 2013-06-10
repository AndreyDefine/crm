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
	
	public AbstractTag GetLastObject(){
		if(terrainsList.Count!=0)
		{
			return terrainsList[0]  as AbstractTag;
		}
		else{
			return null;
		}
	}
	
	public AbstractTag GetLastAddedObject(){
		if(terrainsList.Count!=0)
		{
			return terrainsList[terrainsList.Count-1] as AbstractTag;
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
			(terrainsListToDel[i]as AbstractTag).singleTransform.position=newPos;
		}
		for(i=0;i<terrainsList.Count;i++){
			(terrainsList[i]as AbstractTag).singleTransform.position=newPos;
			terrainsListToDel.Add(terrainsList[i]);
		}
		
		terrainsList.Clear();
	}
	
	public virtual void DeleteOneFirstTerrain()
	{
		if(terrainsList.Count>0)
		{
			AbstractTag newterrainToDel=terrainsList[0] as AbstractTag;
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);
		}
	}
	
	public void DeleteCurrent(AbstractTag inObject){
		terrainsList.Remove(inObject);			
		terrainsListToDel.Add(inObject);
		inObject.singleTransform.parent=null;
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
				terrainsListToDel.Add(newTerrain.GetComponent<AbstractTag>());
				newTerrain.name=instr;
				break;
			}
		}	
	}
	
	public virtual void PutToFirstState(AbstractTag newTerrain){
		newTerrain.singleTransform.position=new Vector3(-9999,-9999,-9999);
		newTerrain.singleTransform.rotation=Quaternion.identity;
		newTerrain.ReStart();
	}
	
	public virtual GameObject GetNewObject(){
		GameObject newTerrain=null;
		if(terrainsListToDel.Count>0){
			AbstractTag newTerrainTag;
			int RandIndex=Random.Range(0,terrainsListToDel.Count);
			newTerrainTag=(terrainsListToDel[RandIndex] as AbstractTag);
			terrainsListToDel.Remove(newTerrainTag);		
			//put object to first state
			PutToFirstState(newTerrainTag);
			terrainsList.Add(newTerrainTag);
			newTerrain=newTerrainTag.gameObject;
		}else
		{
			if(terrain1==null&&preloadNames!="")
			{
				parseTerrainNames();
			}
			
			if(preloadNames=="")
			{
				int RandIndex=Random.Range(0,terrainsList.Count);
				newTerrain	= Instantiate((terrainsList[RandIndex] as AbstractTag).gameObject) as GameObject;
				newTerrain.name=(terrainsList[RandIndex] as AbstractTag).gameObject.name;
			}
			else
			{
				int RandIndex=Random.Range(0,terrain1.Length);
				newTerrain	= Instantiate(Resources.Load(pathInResources+"/"+terrain1[RandIndex])) as GameObject;
				newTerrain.name=terrain1[RandIndex];
			}

			addTagToObject(newTerrain);	
			PutToFirstState(newTerrain.GetComponent<AbstractTag>());
			
			terrainsList.Add(newTerrain.GetComponent<AbstractTag>());
		}
		return newTerrain;
	}
	
	public virtual void DestroyPullObjects()
	{
		AbstractTag newTerrain=null;
		for (int i=0; i<terrainsListToDel.Count;i++){
			newTerrain	= terrainsListToDel[i] as AbstractTag;
			Destroy(newTerrain.gameObject);
		}
		terrainsListToDel.Clear();
		
		for (int i=0; i<terrainsList.Count;i++){
			newTerrain	= terrainsList[i] as AbstractTag;
			Destroy(newTerrain.gameObject);
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
			terrainsListToDel.Add (newTerrain.GetComponent<AbstractTag>());
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
				AbstractTag newTerrainTag;
				if((terrainsListToDel[i] as AbstractTag).name==instr){
					newTerrainTag=(terrainsListToDel[i] as AbstractTag);
					terrainsListToDel.Remove(newTerrainTag);
					PutToFirstState(newTerrainTag);
					newTerrain=newTerrainTag.gameObject;
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
				PutToFirstState(newTerrain.GetComponent<AbstractTag>());
				newTerrain.name=instr;
			}
		}
		if(newTerrain)
		{
			terrainsList.Add(newTerrain.GetComponent<AbstractTag>());
		}
		return newTerrain;
	}
	
	public virtual void addTagToObject(GameObject newTerrain){
		AbstractTag curTag;
		
		if(!newTerrain.GetComponent<AbstractTag>())
		{
			newTerrain.AddComponent("AbstractTag");
		}
		else
		{
			Debug.Log ("NOT addTagToObject");
		}
		
		
		curTag=newTerrain.GetComponent("AbstractTag") as AbstractTag;
		curTag.addFactory(this);
	}
}
