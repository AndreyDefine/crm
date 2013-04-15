using UnityEngine;
using System.Collections;

public class LevelTag : Abstract {
	
	public string PreloadTerrains="";
	public string RoadTerrains="";
	
	protected string []preloadTerrainsNames;
	protected string []roadTerrainsNames;
	protected int numberOfTerrains;

	// Use this for initialization
	void Start () {
	}
	
	public void SetNumberOfTerrains(int innumberofTerrains)
	{
		numberOfTerrains=innumberofTerrains;
	}
	
	public void Parse()
	{
		if(roadTerrainsNames==null){
			ParseRoadTerrainNames();
		}
		
		if(preloadTerrainsNames==null){
			ParseTerrainNames();
		}
	}
	
	public string[] GetRoadTerrainNames()
	{
		return roadTerrainsNames;
	}
	
	public string[] GetPreloadTerrainsNames()
	{
		return preloadTerrainsNames;
	}
	
	
	private void ParseTerrainNames()
	{
		//посчитаем необходимые кусочки
		int i,j;
		bool flagFounded;
		ArrayList preloadTerrainList=new ArrayList(); 
		ArrayList terrainList=new ArrayList(); 
		ArrayList removeTerrainList=new ArrayList(); 
		//заполним видимые сначала
		for(i=0;i<numberOfTerrains+1;i++)
		{
			preloadTerrainList.Add(roadTerrainsNames[i]);
			terrainList.Add(roadTerrainsNames[i]);
		}
		
		for(i=numberOfTerrains;i<roadTerrainsNames.Length;i++)
		{
			flagFounded=false;
			for(j=0;j<removeTerrainList.Count;j++){
				if(removeTerrainList[j] as string == roadTerrainsNames[i])
				{
					//нашли
					terrainList.Add(removeTerrainList[j]);
					removeTerrainList.Remove(removeTerrainList[j]);
					flagFounded=true;
					break;
				}
			}
			//ничего не нашли
			if(!flagFounded)
			{
				preloadTerrainList.Add(roadTerrainsNames[i]);
				terrainList.Add(roadTerrainsNames[i]);
			}
			removeTerrainList.Add(terrainList[0]);
			terrainList.Remove(terrainList[0]);
		}
		
		preloadTerrainsNames=new string[preloadTerrainList.Count];
		for(i=0;i<preloadTerrainList.Count;i++){
			preloadTerrainsNames[i]=preloadTerrainList[i] as string;
		}
	}
	
	private void ParseRoadTerrainNames()
	{
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=RoadTerrains.Split(separator);;
		roadTerrainsNames=names;
	}
}
