using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldFactory : AbstractFactory,ScreenControllerToShow {	
	
	public bool MakeObstacleSet=false;
	public bool MakeTerrains=false;
	public bool FlagAddExtraObjectsInPull;
	public string PreloadTerrains="";
	public string RoadTerrains="";
	public GameObject debugPathIndicator;
	
	public GameObject terrainFactory;
	public GameObject UniqueFactory;
	public GameObject ObstacleFactory;
	public GameObject ObstacleSetFactory;
	public GameObject MoneyFactory;
	public GameObject BoostFactory;
	public GameObject []levelTags;
	
	private ArrayList treeElementFactories=new ArrayList();
	
	private TerrainElementFactory terrainElementFactory;
	
	private AbstractElementFactory uniqueElementFactory;
	private AbstractElementFactory boostElementFactory;
	
	
	private AbstractElementFactory obstacleElementFactory;  
	private AbstractElementFactory obstacleSetElementFactory;  	
	
	private AbstractElementFactory moneyElementFactory;
	
	private bool firstTimeInit=true;
	
	protected string []preloadTerrainsNames;
	protected string []roadTerrainsNames;
	protected int currentRoadPos=0;
	
	protected GameObject pathIndicator;
	
	private int vetexCount=0;
	
	private GameObject curLevelTagGameObject=null;
	private LevelTag curLevelTag;
	
	protected bool flagFirstTime=true;
	
	private int versionForCoRoutine=0;
	
	private FermaMissionEmmitter fermaMissionEmmiter;
	
	public string GetCurrentObstacleSet()
	{
		return terrainElementFactory.GetCurrentTerrainForZ().obstacleSetName;
	}
	
	public override void init(){
		GameObject curFactoryObject;
		
		fermaMissionEmmiter=GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter();
		
		//terrain
		curFactoryObject=Instantiate (terrainFactory) as GameObject;
		terrainElementFactory=curFactoryObject.GetComponent("TerrainElementFactory") as TerrainElementFactory;
		
		//uniqueObjects
		curFactoryObject=Instantiate (UniqueFactory) as GameObject;
		uniqueElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//boostObjects
		curFactoryObject=Instantiate (BoostFactory) as GameObject;
		boostElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//obstacles
		curFactoryObject=Instantiate (ObstacleFactory) as GameObject;
		obstacleElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		//Obstacle Set
		curFactoryObject=Instantiate (ObstacleSetFactory) as GameObject;
		obstacleSetElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		if(MakeObstacleSet)
		{
			obstacleSetElementFactory.pathInResources="ObstacleSets/new";
		}
		//money
		curFactoryObject=Instantiate (MoneyFactory) as GameObject;
		moneyElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		numberOfTerrains=3;
		
		//Get current level
		LoadCurrentLevel();
		
		if(debugPathIndicator){
			pathIndicator=Instantiate (debugPathIndicator) as GameObject;
		}
	}
	
	// Screen Controller To Show Methods
	public void ShowOnScreen()
	{
		GlobalOptions.GetMissionEmmitters().LevelBegin();
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
		
		preloadTerrainsNames=curLevelTag.GetPreloadTerrainsNames();
		roadTerrainsNames=curLevelTag.GetRoadTerrainNames();
		
		AddAllObjectsIntoPulls();
	}
	
	public void AddAllObjectsIntoPulls()
	{
		//если раннер
		uniqueElementFactory.DestroyPullObjects();
		boostElementFactory.DestroyPullObjects();
		obstacleElementFactory.DestroyPullObjects();
		obstacleSetElementFactory.DestroyPullObjects();
		moneyElementFactory.DestroyPullObjects();
		terrainElementFactory.DestroyPullObjects();
		
		for(int i=0;i<treeElementFactories.Count;i++)
		{
			(treeElementFactories[i] as GameObject).GetComponent<AbstractElementFactory>().DestroyPullObjects();
		}
		
		//really need this!!!
		//obstacleSetElementFactory.PreloadPullObjects();
		boostElementFactory.PreloadPullObjects();
		moneyElementFactory.PreloadPullObjects();
		obstacleElementFactory.PreloadPullObjects();
		uniqueElementFactory.PreloadPullObjects();
		obstacleSetElementFactory.PreloadPullObjects();
	}
	
	public override void ReStart(){
		oldObjectPos=initialPos;
		currentRoadPos=0;
		vetexCount=0;
		firstTimeInit=true;
		int i;
		
		versionForCoRoutine++;
		
		fermaMissionEmmiter=GlobalOptions.GetMissionEmmitters().GetFermaMissionEmmitter();
		
		terrainElementFactory.ReStart();
		uniqueElementFactory.ReStart();
		boostElementFactory.ReStart();
		obstacleElementFactory.ReStart();
		obstacleSetElementFactory.ReStart();
		for(i=0;i<treeElementFactories.Count;i++)
		{
			(treeElementFactories[i] as GameObject).GetComponent<AbstractElementFactory>().ReStart();
		}
		moneyElementFactory.ReStart();
		
		for(i=0;i<=numberOfTerrains;i++)
		{
			AddNextTerrain(false);
		}
	}
	
	public void TryAddTerrrain() {	
		if(GlobalOptions.flagOnlyFizik)
		{
			terrainElementFactory.SetNextCurrentTerrainNext();				
		}
		AddNextTerrain(true);
		//удаляем старый кусочек земли
		DeleteOneFirstTerrain();
    }
	
	private void ParseTerrainNames()
	{
		
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=PreloadTerrains.Split(separator);
		preloadTerrainsNames=names;
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
	
	private IEnumerator addDynamicByMarkers(GameObject inTerrain,TerrainTag interrainTag,bool FlagCoRoutine){
		int i,j;	
		int kolvo;
		int randIndex;
		string curname;
		
		//Debug.Log (FlagCoRoutine);
		
		int curversionForCoRoutine=versionForCoRoutine;
		//tree
		ArrayList markedObjectsTrees=new ArrayList();	
		//uniqueobjects Terrains
		ArrayList markedObjectsUniqueTerrains=new ArrayList();
		
		//uniqueobjects
		ArrayList markedObjectsUnique=new ArrayList();	
		
		//ObstacleSet
		ArrayList markedObjectsObstacleSet=new ArrayList();	
		int neededNumberOfObstacleSet=1;
		
		//find all marks
		Transform[] allChildren = inTerrain.gameObject.GetComponentsInChildren<Transform>();
		for(i=0;i<allChildren.Length;i++)
		{
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			//tree
			if(allChildren[i].name=="tree"){
				markedObjectsTrees.Add (allChildren[i]);
			}	
			
			//uniqueObjects
			if(allChildren[i].name=="UniqueObjectPool"){
				markedObjectsUnique.Add (allChildren[i]);
			}	
			
			//uniqueObjects Terrains
			if(allChildren[i].name=="UniqueObjectPoolTerrains"){
				markedObjectsUniqueTerrains.Add (allChildren[i]);
			}	
			
			//Obstacle Set
			if((currentRoadPos>1||!firstTimeInit)&&!PersonInfo.tutorial)
			{
				//Obstacle Set
				if(allChildren[i].name=="ObstacleSet"){
					markedObjectsObstacleSet.Add (allChildren[i]);
				}	
			}
			
			if(FlagCoRoutine&&i%50==0) yield return null;
		}
		
		//unique terrains
		Transform curUniqueTerrain;
		for(i=0;i<markedObjectsUniqueTerrains.Count;i++){
			Transform[] uniqueMarkers = (markedObjectsUniqueTerrains[i] as Transform).gameObject.GetComponentsInChildren<Transform>();
			for(j=1;j<uniqueMarkers.Length;j++){
				if(curversionForCoRoutine!=versionForCoRoutine) yield break; 
				curUniqueTerrain=(uniqueMarkers[j] as Transform);
				curname=curUniqueTerrain.name;
				if(!curname.Contains("Left")&&!curname.Contains("Right"))
				{
					addOneUniqueAtMarker(curUniqueTerrain,interrainTag);
					if(FlagCoRoutine) yield return null;
				}
			}
		}
		
		int jset;
		//obstacles
		if((currentRoadPos>1||!firstTimeInit)&&!PersonInfo.tutorial)
		{
			ArrayList terrainObstacleSetList=interrainTag.GetObstacleSetNamesArrayUnique();
			for(jset=0;interrainTag.obstacleSetNames!=""&&jset<terrainObstacleSetList.Count&&(MakeObstacleSet||jset==0);jset++)
			{				
				//ObstacleSet
				GameObject curSet;
				Transform OneObstacle,marker;
				int randomIndexOfSet=0;
				for(i=0;i<markedObjectsObstacleSet.Count&&terrainObstacleSetList.Count!=0;i++){
					if(curversionForCoRoutine!=versionForCoRoutine) yield break;
					kolvo=neededNumberOfObstacleSet>markedObjectsObstacleSet.Count?markedObjectsObstacleSet.Count:neededNumberOfObstacleSet;
					for(int i2=0;i2<kolvo;i2++){
						if(curversionForCoRoutine!=versionForCoRoutine) yield break;
						//случайный индекс маркера
						randIndex=Random.Range(0,markedObjectsObstacleSet.Count);
						//получим марке
						marker=markedObjectsObstacleSet[randIndex]as Transform;
						//теперь выбираем сет						
						if(interrainTag)
						
						if(MakeObstacleSet)
						{
							randomIndexOfSet=jset;
						}else
						{
							randomIndexOfSet=Random.Range(0,terrainObstacleSetList.Count);
						}
						// получаем препятствие
						curSet=obstacleSetElementFactory.GetNewObjectWithName(terrainObstacleSetList[randomIndexOfSet]as string);
						//поместим сет препятствий куда надо
						curSet.transform.position=marker.position;
						curSet.transform.rotation=marker.rotation;
						
						//name
						interrainTag.obstacleSetName=curSet.name;
						//получим список препятствий
						Transform[] setMarkers = curSet.GetComponentsInChildren<Transform>();
						
						for(j=1;j<setMarkers.Length;j++){
							if(curversionForCoRoutine!=versionForCoRoutine) yield break;
							OneObstacle=(setMarkers[j] as Transform);
							if(!OneObstacle)
							{
								continue;
							}
							curname=OneObstacle.name;
							
							bool flagCompiled=false;
							Transform parentOneObstacle;
							parentOneObstacle=OneObstacle.parent;
							//ищем отца
							while(parentOneObstacle!=null&&!MakeObstacleSet)
							{
								if(parentOneObstacle.name.Contains("Compiled"))
								{
									flagCompiled=true;
									break;
								}
								parentOneObstacle=parentOneObstacle.parent;
							}
							
							if(curname=="money"&&!flagCompiled)
							{
								addOneMoneyAtMarker(OneObstacle,curSet.transform,interrainTag);
							}
							else
							if(curname=="boost"&&!flagCompiled)
							{
								addOneBoostAtMarker(OneObstacle,curSet.transform,interrainTag);
							}
							else
							{
								if(!flagCompiled)
								{
									StartCoroutine(addOneObstacleFromSetAtMarker(OneObstacle,curSet.transform,interrainTag,0,FlagCoRoutine));
								}
							}
							if(FlagCoRoutine&&j%2==0) yield return null;
						}
						
						if(!MakeObstacleSet)
						{
							interrainTag.RemoveFromobstacleSetNamesArrayUniqueAt(randomIndexOfSet);
						}
						//добавим в terrain
						interrainTag.PushToAllElements(curSet.GetComponent<AbstractTag>());
					}
				}
			}
		}
		
		//unique
		Transform curUnique;
		for(i=0;i<markedObjectsUnique.Count;i++){
			Transform[] uniqueMarkers = (markedObjectsUnique[i] as Transform).gameObject.GetComponentsInChildren<Transform>();
			for(j=1;j<uniqueMarkers.Length;j++){
				if(curversionForCoRoutine!=versionForCoRoutine)yield break;
				curUnique=(uniqueMarkers[j] as Transform);
				curname=curUnique.name;
				if(!curname.Contains("Left")&&!curname.Contains("Right"))
				{
					addOneUniqueAtMarker(curUnique,interrainTag);
					if(FlagCoRoutine&&j%2==0) yield return null;
				}
			}
		}
		
		//trees
		for(i=0;i<markedObjectsTrees.Count;i++){
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			addOneTreeAtMarker(markedObjectsTrees[i] as Transform,interrainTag);
			if(FlagCoRoutine&&i%2==0) yield return null;
		}
		
		if(curversionForCoRoutine!=versionForCoRoutine) yield break;
		if(FlagCoRoutine) yield return null;
	}
	
	private IEnumerator addOneObstacleFromSetAtMarker(Transform marker,Transform inparent,TerrainTag interrainTag, int recursion,bool FlagCoRoutine){
		GameObject newObject,vspObject;
		bool flagCompiled=false;
	
		newObject = obstacleElementFactory.GetNewObjectWithName(marker.name);
				
		if(!newObject)
		{
			//Debug.Log (marker.name+" NOT FOUND!!!");
			if(FlagCoRoutine) yield return null;
		}
		
		//set position & rotation
		newObject.transform.position=marker.position;
		
		MarkerTag markerTag=newObject.GetComponent<MarkerTag>();
		if(markerTag)
		{
			markerTag.ApplyRotation(marker.rotation,interrainTag.singleTransform.rotation);
		}
		else
		{
			newObject.transform.rotation=marker.rotation;
		}
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
		
		if(marker.name=="TochkaSbora")
		{
			fermaMissionEmmiter.AddMissionEmmitterListener(newObject.GetComponentInChildren<TochkaSbora>());
			if(fermaMissionEmmiter.GetCurrentMissions().Count==0)
			{
				newObject.GetComponentInChildren<AbstractEnemy>().MakeInactiveParent();
			}
		}
		
		//if compiled object
		if(marker.name.Contains("Compiled"))
		{
			flagCompiled=true;
			int j;
			//ищем контейнер
			Transform Container;
			Transform Container2;
			Container=marker.transform.FindChild("ContainerOfObjects");
			if(MakeObstacleSet)
			{
				vspObject=new GameObject();
				Container2=vspObject.transform;
				
				Container2.parent=Container.parent;
				Container2.position=Container.position;
			    Container2.rotation=Container.rotation;
				
				Container2.name=Container.name;
			}
			else
			{
				Container2=newObject.transform.FindChild("ContainerOfObjects");
			}
			if(Container)
			{
				Transform[] allChildren = Container.gameObject.GetComponentsInChildren<Transform>();
				//обрабатываем все трансформы
				for(j=1;j<allChildren.Length;j++){
					//bug with NUULLS
					if(allChildren[j].name=="money")
					{
						addOneMoneyAtMarker(allChildren[j],Container2,interrainTag);
						continue;
					}
					
					if(allChildren[j].name=="boost")
					{
						addOneBoostAtMarker(allChildren[j],Container2,interrainTag);
						continue;
					}
					//reqursively
					if(FlagCoRoutine) yield return null;
					StartCoroutine(addOneObstacleFromSetAtMarker(allChildren[j],Container2,interrainTag,recursion+1,FlagCoRoutine));
				}
				if(MakeObstacleSet)
				{
					DestroyImmediate(Container.gameObject);
				}
			}
		}
				
		if(MakeObstacleSet)
		{
			vspObject=new GameObject();
			if(marker.name=="MonetaContainer")
			{
				vspObject.name="money";
			}
			
			else if(marker.name=="VodkaContainer"||marker.name=="MagnitContainer"||marker.name=="PostalContainer"||marker.name=="KopilkaContainer")
			{
				vspObject.name="boost";
			}
			else
			{
				vspObject.name=marker.name;
			}
			vspObject.transform.position=marker.position;
			vspObject.transform.rotation=marker.rotation;
			vspObject.transform.parent=inparent;
					
			if(recursion==0&&!flagCompiled)
			{
				DestroyImmediate(marker.gameObject);
			}
			
			if(recursion==0&&flagCompiled)
			{
				Transform Container=marker.transform.FindChild("ContainerOfObjects");
				Container.parent=vspObject.transform;
				DestroyImmediate(marker.gameObject);
			}
		}
		else
		{
			newObject.transform.parent=inparent;
		}
		
		if(FlagCoRoutine) yield return null;
	}
	
	private void addOneUniqueAtMarker(Transform marker,TerrainTag interrainTag){
		GameObject newObject;
		
		newObject = uniqueElementFactory.GetNewObjectWithName(marker.name);
		
		if(!newObject)
		{
			Debug.Log ("Object Not Found - "+marker.name);
			return;
		}
		
		//set position & rotation
		newObject.transform.position=marker.position;
		
		MarkerTag markerTag=newObject.GetComponent<MarkerTag>();
		if(markerTag)
		{
			markerTag.ApplyRotation(marker.rotation,interrainTag.singleTransform.rotation);
		}
		else
		{
			newObject.transform.rotation=marker.rotation;
		}
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
		
		if(MakeTerrains)
		{
			GameObject vspObject;
			vspObject=new GameObject();
			Transform vspTerrain=vspObject.transform;
				
			vspTerrain.parent=marker.parent;
			vspTerrain.position=marker.position;
			vspTerrain.rotation=marker.rotation;
			
			marker.parent=null;
				
			vspTerrain.name=marker.name;
		}
	}
	
	private void addOneBoostAtMarker(Transform marker,Transform inparent,TerrainTag interrainTag){
		GameObject newObject;
		
		if(Random.Range (0,2)<1)
		{
			return;
		}
		
		do
		{
			newObject = boostElementFactory.GetNewObject();
		}while(newObject.name=="PostalContainer"&&PersonInfo.post>2);
		
		//set position & rotation
		newObject.transform.position=marker.position;
		
		newObject.transform.rotation=marker.rotation;
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
		
		if(!MakeObstacleSet)
		{
			newObject.transform.parent=inparent;
		}
	}
	
	private void addOneTreeAtMarker(Transform marker,TerrainTag interrainTag){
		int i;
		GameObject newObject;
		AbstractElementFactory treeElementFactory=null;
		
		//findTreeFactory
		for(i=0;i<treeElementFactories.Count;i++)
		{
			//нашли
			if((treeElementFactories[i] as GameObject).name==interrainTag.treeElementFactory.name){
				treeElementFactory=(treeElementFactories[i] as GameObject).GetComponent<AbstractElementFactory>();
				break;
			}
		}	
		
		if(!treeElementFactory)
		{
			GameObject newTreeFactory;
			newTreeFactory	= Instantiate (interrainTag.treeElementFactory) as GameObject;
			treeElementFactories.Add(newTreeFactory);
			newTreeFactory.name=interrainTag.treeElementFactory.name;
			treeElementFactory=newTreeFactory.GetComponent<AbstractElementFactory>();
		}
		
		
		newObject	= treeElementFactory.GetNewObject();
			
		//set position & rotation
		newObject.transform.position=marker.position;
		
		MarkerTag marderTag=newObject.GetComponent<MarkerTag>();
		if(marderTag)
		{
			marderTag.ApplyRotation(marker.rotation,interrainTag.singleTransform.rotation);
		}
		else
		{
			newObject.transform.rotation=marker.rotation;
		}
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
		
		if(MakeTerrains)
		{
			GameObject vspObject;
			vspObject=new GameObject();
			Transform vspTerrain=vspObject.transform;
				
			vspTerrain.parent=marker.parent;
			vspTerrain.position=marker.position;
			vspTerrain.rotation=marker.rotation;
			
			marker.parent=null;
				
			vspTerrain.name=marker.name;
		}
	}
	
	
	private void addOneMoneyAtMarker(Transform marker,Transform inparent,TerrainTag interrainTag){
		GameObject newObject;
		newObject	= moneyElementFactory.GetNewObject();
			
		//set position & rotation
		newObject.transform.position=marker.position;
		
		MarkerTag markerTag=newObject.GetComponent<MarkerTag>();
		if(markerTag)
		{
			markerTag.ApplyRotation(marker.rotation,interrainTag.singleTransform.rotation);
		}
		else
		{
			newObject.transform.rotation=marker.rotation;
		}
		
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
		
		if(!MakeObstacleSet)
		{
			newObject.transform.parent=inparent;
		}
	}
	
	private IEnumerator AddObjects(bool FlagCoRoutine){	
		
		GameObject newTerrain=null;
		TerrainTag terrainTag=null;
		Vector3 newpos=new Vector3(0,0,0);
		
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
		//динамические объекты
		StartCoroutine(addDynamicByMarkers(newTerrain,terrainTag,FlagCoRoutine));	
		if(FlagCoRoutine) yield return null;
		
	}
	
	
	public GameObject AddTerrain()
	{
		GameObject newTerrain=null;
		if(firstTimeInit)
		{
			if(currentRoadPos>=roadTerrainsNames.Length)
			{
				firstTimeInit=false;
				currentRoadPos=0;
			}
			newTerrain=terrainElementFactory.GetNewObjectWithName(roadTerrainsNames[currentRoadPos]);
			currentRoadPos++;
		}
		else
		{
			newTerrain=terrainElementFactory.GetNewObject();
		}
		return newTerrain;
	}
	
	public override void DeleteOneFirstTerrain()
	{		
		if(terrainElementFactory.flagGenerate){
			terrainElementFactory.DeleteOneFirstTerrain();
		}
	}
	
	//get xsmex
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition){
		Debug.Log ("GetXandYandAngleSmexForZ");
		if(GlobalOptions.flagOnlyFizik)
		{
			return new Vector3(0f,0f,0f);
		}
		
		
		return terrainElementFactory.GetXandYandAngleSmexForZ(inposition);
		//addDotToPathIndicator(new Vector3(returnXandYandAngle.x,returnXandYandAngle.y,inposition.z));
	}
	
	public float GetCurTerrainCenter(){
		float returny=terrainElementFactory.GetCurTerrainCenter();
		
		return returny;
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
		TerrainTag prev=terrainElementFactory.GetCurrentTerrainForZ().GetComponent<TerrainTag>().GetPrevTerrain();
		
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
		return terrainElementFactory.GetCurrentTerrainForZ().transform.position;
	}
	
	public Vector3 GetCurTerrainEnd()
	{
		return terrainElementFactory.GetCurrentTerrainForZ().GetEndOfTerrain();
	}
	
	public override float GetTerrainLength()
	{
		return terrainElementFactory.GetCurrentTerrainForZ().GetComponent<TerrainTag>().sizeOfPlane;
	}
	
	public override float GetLastTerrainLength()
	{
		return terrainElementFactory.GetLastObject().GetComponent<TerrainTag>().sizeOfPlane;
	}
	
}


