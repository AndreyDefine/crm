using UnityEngine;
using System.Collections;

public class WorldFactory : AbstractFactory,ScreenControllerToShow {	
	
	public bool FlagAddExtraObjectsInPull;
	public int numOfTrees;
	public string PreloadTerrains="";
	public string RoadTerrains="";
	public GameObject debugPathIndicator;
	
	public GameObject terrainFactory;
	public GameObject treeFactory;
	public GameObject EnemiesFactory;
	public GameObject BerriesFactory;
	public GameObject StrobileFactory;
	public GameObject []levelTags;
	
	private GameObject terrainFactoryObject;
	private TerrainElementFactory terrainElementFactory;
	
	private GameObject treeFactoryObject;
	private AbstractElementFactory treeElementFactory;
	
	private GameObject enemyFactoryObject;
	private AbstractElementFactory enemyElementFactory;
	
	private GameObject berryFactoryObject;
	private AbstractElementFactory berryElementFactory;
	
	private GameObject strobileFactoryObject;
	private AbstractElementFactory strobileElementFactory;
	
	private GuiLayerInitializer guiLayer;
	
	private bool firstTimeInit=true;
	
	protected string []preloadTerrainsNames;
	protected string []roadTerrainsNames;
	protected int currentRoadPos=0;
	
	protected GameObject pathIndicator;
	
	private int vetexCount=0;
	
	private GameObject curLevelTagGameObject=null;
	private LevelTag curLevelTag;
	
	protected bool flagFirstTime=true;
	
	public override void init(){
		//terrain
		terrainFactoryObject=Instantiate (terrainFactory) as GameObject;
		terrainElementFactory=terrainFactoryObject.GetComponent("TerrainElementFactory") as TerrainElementFactory;
		
		//trees
		treeFactoryObject=Instantiate (treeFactory) as GameObject;
		treeElementFactory=treeFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//enemies
		enemyFactoryObject=Instantiate (EnemiesFactory) as GameObject;
		enemyElementFactory=enemyFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//berries
		berryFactoryObject=Instantiate (BerriesFactory) as GameObject;
		berryElementFactory=berryFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//strobiles
		strobileFactoryObject=Instantiate (StrobileFactory) as GameObject;
		strobileElementFactory=strobileFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//ищем gui слой
		guiLayer=GlobalOptions.GetGuiLayer();
		
		terrainLength=terrainElementFactory.terrainLength;
		
		numberOfTerrains=(int)(drawnPerspective/terrainLength);
		
		//Get current level
		LoadCurrentLevel();
		
		if(debugPathIndicator){
			pathIndicator=Instantiate (debugPathIndicator) as GameObject;
		}
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		if(!flagFirstTime){
			Debug.Log ("LoadCurrentLevel");
			LoadCurrentLevel();
			GlobalOptions.GetPlayerScript().Restart();
		}
	}
	
	public void HideOnScreen()
	{
		Debug.Log ("HideOnScreen");
		flagFirstTime=false;
	}
	//end Screen Controller To Show Methods
	
	public string GetNextLevelName()
	{
		string returnstring=GlobalOptions.loadingLevel;
		int i;
		for(i=0;i<levelTags.Length;i++)
		{
			//нашли
			if((levelTags[i] as GameObject).name==GlobalOptions.loadingLevel){
				break;
			}
		}	
		
		if(i<levelTags.Length-1)
		{
			i++;
			returnstring=(levelTags[i] as GameObject).name;
		}
		return returnstring;
	}
	
	public void LoadCurrentLevel(){
		int i;
		for(i=0;i<levelTags.Length;i++)
		{
			//нашли
			if((levelTags[i] as GameObject).name==GlobalOptions.loadingLevel){
				curLevelTagGameObject = Instantiate (levelTags[i]) as GameObject;
				break;
			}
		}	
		
		curLevelTag=curLevelTagGameObject.GetComponent("LevelTag")as LevelTag;
		curLevelTag.Parse();
		
		GlobalOptions.gameType=curLevelTag.gameType;
		
		preloadTerrainsNames=curLevelTag.GetPreloadTerrainsNames();
		roadTerrainsNames=curLevelTag.GetRoadTerrainNames();
		
		AddAllObjectsIntoPulls();
	}
	
	public void AddAllObjectsIntoPulls()
	{
		Debug.Log ("ObjectsAddedToPull");
		//если раннер
		if(curLevelTag.gameType==GameType.Runner)
		{
			enemyElementFactory.DestroyPullObjects();
			berryElementFactory.DestroyPullObjects();
			
			enemyElementFactory.PreloadPullObjects();
			berryElementFactory.PreloadPullObjects();
		}
	}
	
	public override void ReStart(){
		oldObjectPos=initialPos;
		currentRoadPos=0;
		vetexCount=0;
		firstTimeInit=true;
		int i;
		
		terrainElementFactory.ReStart();
		enemyElementFactory.ReStart();
		treeElementFactory.ReStart();
		berryElementFactory.ReStart();
		strobileElementFactory.ReStart();
		
		for(i=0;i<=numberOfTerrains;i++)
		{
			AddNextTerrain(false);
		}
	}
	
	void Update () {
		if(currentRoadPos<roadTerrainsNames.Length){
			TryAddTerrrain();
		}
		else
		{
			if(curLevelTag.gameType==GameType.Runner)
			{
				firstTimeInit=false;
				currentRoadPos=0;
			}
			else
			{
				//show you won
				guiLayer.ShowYouWon();
			}
		}
	}
	
	private void ParseTerrainNames()
	{
		//посчитаем необходимые кусочки
		if(drawMode){
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
		else
			{//получили массив террейнов
			char []separator={',','\n',' '};
			string []names=PreloadTerrains.Split(separator);
			preloadTerrainsNames=names;
		}
	}
	
	private void ParseRoadTerrainNames()
	{
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=RoadTerrains.Split(separator);;
		roadTerrainsNames=names;
	}
	
	public override void AddNextTerrain(bool FlagCoRoutine){
		StartCoroutine(AddObjects(FlagCoRoutine));		
	}
	
	public override void AddObjectsInPulls(bool FlagCoRoutine){
		StartCoroutine(AddExtraObjectsInPullsCoRutine(FlagCoRoutine));	
	}
	
	private IEnumerator AddExtraObjectsInPullsCoRutine(bool FlagCoRoutine){
		
		int i;
		for(i=0;i<preloadTerrainsNames.Length;i++)
		{
			terrainElementFactory.AddExtraObjectInPullWithName(preloadTerrainsNames[i]);
			if(FlagCoRoutine) yield return null;
		}
		
		if(FlagAddExtraObjectsInPull){
			if(terrainElementFactory.flagGenerate){
				terrainElementFactory.AddExtraObjectInPull();
				if(FlagCoRoutine) yield return null;
			}
			if(berryElementFactory.flagGenerate){
				berryElementFactory.AddExtraObjectInPull();
				if(FlagCoRoutine) yield return null;
			}
			if(strobileElementFactory.flagGenerate){
				strobileElementFactory.AddExtraObjectInPull();
				if(FlagCoRoutine) yield return null;
			}
			//trees
			for(i=0;i<numOfTrees;i++){
				if(treeElementFactory.flagGenerate){
					treeElementFactory.AddExtraObjectInPull();
					if(FlagCoRoutine) yield return null;
				}
			}
		}
		if(FlagCoRoutine) yield return null;
	}
	
	private IEnumerator addDynamicByMarkers(GameObject inTerrain,TerrainTag interrainTag,bool FlagCoRoutine){
		int i;	
		int kolvo;
		int randIndex;
		//tree
		ArrayList markedObjectsTrees=new ArrayList();	
		//berry
		ArrayList markedObjectsBerry=new ArrayList();	
		int neededNumberOfBerries=2;
		//enemy
		ArrayList markedObjectsEnemy=new ArrayList();	
		int neededNumberOfEnemy=2;
		
		//find all marks
		Transform[] allChildren = inTerrain.gameObject.GetComponentsInChildren<Transform>();
		for(i=0;i<allChildren.Length;i++)
		{
			//tree
			if(allChildren[i].name=="tree"&&treeElementFactory.flagGenerate){
				markedObjectsTrees.Add (allChildren[i]);
			}	
			
			//berry
			if(currentRoadPos>2||!firstTimeInit)
			{
				if(allChildren[i].name=="berry"&&berryElementFactory.flagGenerate){
					markedObjectsBerry.Add (allChildren[i]);
				}	
				
				//enemy
				if(allChildren[i].name=="enemy"&&enemyElementFactory.flagGenerate){
					markedObjectsEnemy.Add (allChildren[i]);
				}	
			}
		}
		
		//berry
		if(currentRoadPos>2||!firstTimeInit)
		{
			if(markedObjectsBerry.Count!=0)
			{
				kolvo=neededNumberOfBerries>markedObjectsBerry.Count?markedObjectsBerry.Count:neededNumberOfBerries;
				for(i=0;i<kolvo;i++){
					randIndex=Random.Range(0,markedObjectsBerry.Count);
					addOneBerryAtMarker(markedObjectsBerry[randIndex]as Transform,interrainTag);
					markedObjectsBerry.RemoveAt(randIndex);
					if(FlagCoRoutine) yield return null;
				}
			}
			
			//enemy
			if(markedObjectsEnemy.Count!=0)
			{
				kolvo=neededNumberOfEnemy>markedObjectsEnemy.Count?markedObjectsEnemy.Count:neededNumberOfEnemy;
				for(i=0;i<kolvo;i++){
					randIndex=Random.Range(0,markedObjectsEnemy.Count);
					addOneEnemyAtMarker(markedObjectsEnemy[randIndex]as Transform,interrainTag);
					markedObjectsEnemy.RemoveAt(randIndex);
					if(FlagCoRoutine) yield return null;
				}
			}
		}
		
		//trees
		for(i=0;i<markedObjectsTrees.Count;i++){
			addOneTreeAtMarker(markedObjectsTrees[i] as Transform,interrainTag);
			if(FlagCoRoutine&&(i%3==0)) yield return null;
		}
		
		if(FlagCoRoutine) yield return null;
	}
	
	private void addOneTreeAtMarker(Transform marker,TerrainTag interrainTag){
		GameObject newObject;
		newObject	= treeElementFactory.GetNewObject();
			
		//set position & rotation
		newObject.transform.position=marker.position;
		
		newObject.transform.rotation=marker.rotation;
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	private void addOneEnemyAtMarker(Transform marker,TerrainTag interrainTag){
		GameObject newObject;
		newObject	= enemyElementFactory.GetNewObject();
			
		//set position & rotation
		newObject.transform.position=marker.position;
		
		newObject.transform.rotation=marker.rotation;
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	private void addOneBerryAtMarker(Transform marker,TerrainTag interrainTag){
		GameObject newObject;
		newObject	= berryElementFactory.GetNewObject();
			
		//set position & rotation
		newObject.transform.position=marker.position;
		
		newObject.transform.rotation=marker.rotation;
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	private IEnumerator AddObjects(bool FlagCoRoutine){	
		
		GameObject newTerrain=null;
		TerrainTag terrainTag=null;
		Vector3 newpos=new Vector3(0,0,0);
		Vector3 treepos;
		float shag=terrainLength/numOfTrees;
		float xsmeh=0,zsmeh;
		
		Vector3 oldWhereToBuild=GlobalOptions.whereToBuild;
		
		
		if(terrainElementFactory.flagGenerate){
			Vector3 lastAddedEndOfTerrain;
			
			if(terrainElementFactory.GetLastAddedObject())
			{
				lastAddedEndOfTerrain=terrainElementFactory.GetLastAddedObject().GetComponent<TerrainTag>().GetEndOfTerrain();
			}
			else
			{
				lastAddedEndOfTerrain=new Vector3(0,0,0);
			}
			
			newTerrain=AddTerrain();
			terrainTag=newTerrain.GetComponent("TerrainTag") as TerrainTag;
			
			if(terrainTag.isEndOfTerrain())
			{
				newpos=new Vector3(lastAddedEndOfTerrain.x+terrainTag.sizeOfPlane/2*oldWhereToBuild.x,lastAddedEndOfTerrain.y,lastAddedEndOfTerrain.z+terrainTag.sizeOfPlane/2*oldWhereToBuild.z);
			}
			else
			{
				newpos=new Vector3(oldObjectPos.x+terrainTag.sizeOfPlane/2*oldWhereToBuild.x,oldObjectPos.y+lastAddedEndOfTerrain.y,oldObjectPos.z+terrainTag.sizeOfPlane/2*oldWhereToBuild.z);
				oldObjectPos=new Vector3(oldObjectPos.x+terrainTag.sizeOfPlane*oldWhereToBuild.x,oldObjectPos.y+lastAddedEndOfTerrain.y,oldObjectPos.z+terrainTag.sizeOfPlane*oldWhereToBuild.z);
			}
			
			//if rotation
			if(terrainTag.nextGoingTo!=TerrainTagNextGoingTo.FORWARD)
			{
				if(terrainTag.nextGoingTo==TerrainTagNextGoingTo.LEFT)
				{
					GlobalOptions.whereToBuild=GlobalOptions.TurnLeftRightVector(oldWhereToBuild,true);
				}else
				{
					GlobalOptions.whereToBuild=GlobalOptions.TurnLeftRightVector(oldWhereToBuild,false);
				}
			}
			
			
			newTerrain.transform.position=newpos;
			
			//rotate terrain
			GlobalOptions.rotateTransformForWhere(newTerrain.transform,oldWhereToBuild);
				
			if(FlagCoRoutine) yield return null;
		}
		//а надо ли что нибудь ещё добавлять?
		if(terrainTag.Dynamic)
		{
			//динамические объекты
			StartCoroutine(addDynamicByMarkers(newTerrain,terrainTag,FlagCoRoutine));	
			if(FlagCoRoutine) yield return null;
		}
		
		
		if(!terrainTag.HandMade)
		{
			//trees left
			if(treeElementFactory.flagGenerate){
				for(int i=0;i<numOfTrees;i++){
					treepos=newpos;
					treepos+=treeElementFactory.GetInitialPos();
					zsmeh=-terrainTag.sizeOfPlane/2+shag*i;
					if(terrainTag){
						xsmeh=terrainTag.GetXandYandAngleSmexForZ(zsmeh).x;
					}
					
					treepos.z+=zsmeh;
					treepos.x+=xsmeh;
					AddOneTreeAtPos(treepos,terrainTag,treeElementFactory,false);
					if(FlagCoRoutine) yield return null;
				}
			}
			
			
			//berries
			if(!terrainTag.HandMade)
			{
				if(berryElementFactory.flagGenerate){
					addBerry(newpos,terrainTag);
					if(FlagCoRoutine) yield return null;	
				}
			}
			
			//enemies
			if(!terrainTag.HandMade)
			{
				if(enemyElementFactory.flagGenerate){
					addEnemy(newpos,terrainTag);
					if(FlagCoRoutine) yield return null;	
				}
			}
		}
	}
	
	private void AddOneTreeAtPos(Vector3 inpos,TerrainTag interrainTag,AbstractElementFactory inAbstractElementFactory,bool flagRotate){
		GameObject newTree;
		newTree	= inAbstractElementFactory.GetNewObject();
		
		//some random
		inpos.x+=(Random.Range(0,1f)-0.5f)*1f;
		inpos.z+=(Random.Range(0,1f)-0.5f)*0.5f;
		newTree.transform.Translate(inpos);
		
		if(interrainTag){
			interrainTag.PushToAllElements(newTree);
		}		
		//rotation
		if(flagRotate){
			Vector3 randrotation =new Vector3((float)(Random.Range(0,1f)-0.5)*10,(float)(Random.Range(0,1f)-0.5)*180,(float)(Random.Range(0,1f)-0.5)*10);
			newTree.transform.Rotate(randrotation);
		}
	}	
	
	public GameObject AddTerrain()
	{
		GameObject newTerrain;
		newTerrain=terrainElementFactory.GetNewObjectWithName(roadTerrainsNames[currentRoadPos]);
		currentRoadPos++;		
		return newTerrain;
	}
			
	public void addStrobile(Vector3 inpos,TerrainTag interrainTag)
	{
		GameObject newObject;
		inpos+=strobileElementFactory.GetInitialPos();
		newObject=strobileElementFactory.GetNewObject();
		
		newObject.transform.Translate(inpos);
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	public void addEnemy(Vector3 inpos,TerrainTag interrainTag)
	{
		GameObject newObject;
		inpos+=enemyElementFactory.GetInitialPos();
		Vector3 randompos;
		float xsmeh=0;
		randompos=new Vector3((float)(Random.Range(0,1f)-0.5)*2f,0,(float)(Random.Range(0,1f)-0.5)*10);
		inpos+=randompos;
		
		if(interrainTag){
			xsmeh=interrainTag.GetXandYandAngleSmexForZ(randompos.z).x;
		}
		inpos.x+=xsmeh;
		
		newObject=enemyElementFactory.GetNewObject();
		
		newObject.transform.Translate(inpos);
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	public void addBerry(Vector3 inpos,TerrainTag interrainTag)
	{
		GameObject newObject;
		inpos+=berryElementFactory.GetInitialPos();
		Vector3 randompos;
		float xsmeh=0;
		randompos=new Vector3((float)(Random.Range(0,1f)-0.5)*2f,0,(float)(Random.Range(0,1f)-0.5)*10);
		inpos+=randompos;
		
		if(interrainTag){
			xsmeh=interrainTag.GetXandYandAngleSmexForZ(randompos.z).x;
		}
		inpos.x+=xsmeh;
		
		newObject=berryElementFactory.GetNewObject();
		
		newObject.transform.Translate(inpos);
		if(interrainTag){
			interrainTag.PushToAllElements(newObject);
		}
	}
	
	public override void DeleteOneFirstTerrain()
	{		
		if(terrainElementFactory.flagGenerate){
			terrainElementFactory.DeleteOneFirstTerrain();
		}
	}
	
	//get xsmex
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition){
		Vector3 returnXandYandAngle=terrainElementFactory.GetXandYandAngleSmexForZ(inposition);
		//addDotToPathIndicator(new Vector3(returnXandYandAngle.x,returnXandYandAngle.y,inposition.z));
		
		
		return returnXandYandAngle;
	}
	
	private void addDotToPathIndicator(Vector3 indot)
	{
		if(pathIndicator)
		{
			LineRenderer pathRenderer = pathIndicator.GetComponent<LineRenderer>();
			
			vetexCount++;
			
			pathRenderer.SetVertexCount(vetexCount);
			
			pathRenderer.SetPosition(vetexCount-1, indot);
		}
	}
	
	public override Vector3 GetPrevTerrainPos()
	{
		TerrainTag prev=terrainElementFactory.GetCurrentTerrainForZ(GlobalOptions.GetPlayerScript().GetCharacterPosition()).GetComponent<TerrainTag>().GetPrevTerrain();
		
		if(prev)
		{
			return prev.transform.position;
		}
		
		return GetCurTerrainPos();
	}
	
	public override Vector3 GetLastTerrainPos()
	{
		return terrainElementFactory.GetLastObject().transform.position;
	}
	
	
	public override Vector3 GetCurTerrainPos()
	{
		return terrainElementFactory.GetCurrentTerrainForZ(GlobalOptions.GetPlayerScript().GetCharacterPosition()).transform.position;
	}
	
	public override float GetTerrainLength()
	{
		return terrainElementFactory.GetCurrentTerrainForZ(GlobalOptions.GetPlayerScript().GetCharacterPosition()).GetComponent<TerrainTag>().sizeOfPlane;
	}
	
	public override float GetLastTerrainLength()
	{
		return terrainElementFactory.GetLastObject().GetComponent<TerrainTag>().sizeOfPlane;
	}
	
}


