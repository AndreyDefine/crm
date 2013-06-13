using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using  System.Globalization;

public enum TerrainTagNextGoingTo
{
	FORWARD,
	LEFT,
	RIGHT
}

public class TerrainTag : AbstractTag{
	public GameObject treeElementFactory;
	public TerrainTagNextGoingTo nextGoingTo=TerrainTagNextGoingTo.FORWARD;
	public float sizeOfPlane;
	public string RoadTerrains="";
	
	public string obstacleSetNames;
	
	public bool DynamicSize=true;
	
	private string curobstacleSetName;
	
	public string obstacleSetName
    {
        get
        {
            return curobstacleSetName;
        }
        set
        {
            curobstacleSetName = value;
        }
    }
	
	
	private bool flagFirstTimeInited=true;
	
	public bool firstTimeInited
    {
        get
        {
            return flagFirstTimeInited;
        }
        set
        {
            flagFirstTimeInited = value;
        }
    }
	
	protected Vector3 endOfTerrain;
	
	protected Transform []roadPathTransform=null;
	
	protected List<Transform> roadPathTransformArray=new List<Transform>();
	private string []obstacleSetNamesArray=null;
	
	private ArrayList obstacleSetNamesArrayUnique=new ArrayList();
	
	int curDotIndex=1;
	float curPos=0;
	
	
	int customDotIndex=1;
	float customPos=0;
	
	protected List<AbstractTag> AllElements = new List<AbstractTag>();
	protected List<AbstractEnemy> InactiveElements = new List<AbstractEnemy>();
	
	private float mnoshitel;
	
	private TerrainTag next=null,prev=null;
	
	private bool flagNextTerrainCustom;
	
	public ArrayList GetObstacleSetNamesArrayUnique()
	{
		return obstacleSetNamesArrayUnique;
	}
	
	//rotation
	public int rotatePointIndex=0;
	private	bool FlagLeft=false;
	
	public void ParseObstacleSets()
	{
		if(obstacleSetNamesArray==null){
			ParseObstacleSetNames();
		}
	}
	
	public void RemoveFromobstacleSetNamesArrayUniqueAt(int inindex)
	{
		obstacleSetNamesArrayUnique.RemoveAt(inindex);
		
		if(obstacleSetNamesArrayUnique.Count==0)
		{
			ReloadUniqueSets();
		}
	}
	
	private void ParseObstacleSetNames()
	{
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=obstacleSetNames.Split(separator);;
		obstacleSetNamesArray=names;
		
		
		ReloadUniqueSets();
	}
	
	public void ReloadUniqueSets()
	{
		for (int j=0;j<obstacleSetNamesArray.Length;j++)
		{
			obstacleSetNamesArrayUnique.Add (obstacleSetNamesArray[j]);
		}
	}
	
	public bool isEndOfTerrain()
	{
		Transform EndOfTerrain=singleTransform.FindChild("EndOfTerrain");
		if(EndOfTerrain)
		{
			return true;
		}
		
		return false;
	}
	
	public Vector3 GetEndOfTerrain()
	{
		Transform EndOfTerrain=singleTransform.FindChild("EndOfTerrain");;

		if(EndOfTerrain)
		{
			endOfTerrain=EndOfTerrain.position;
		}
		else
		{
			Debug.Log ("No End Of Terrain");
			endOfTerrain=new Vector3(0,0,0);
		}
		return endOfTerrain;
	}
	
	public Vector3 GetEndOfTerrainLocal()
	{
		Transform EndOfTerrain=singleTransform.FindChild("EndOfTerrain");

		if(EndOfTerrain)
		{
			endOfTerrain=EndOfTerrain.transform.localPosition;
		}
		else
		{
			Debug.Log ("No End Of Terrain");
			endOfTerrain=new Vector3(0,0,0);
		}
		return endOfTerrain;
	}
	
	void Start()
	{
		if(DynamicSize)
		{
			Transform terrain=singleTransform.FindChild("Terrain");
			if(terrain)
			{
				sizeOfPlane=(terrain.gameObject.GetComponent("MeshRenderer")as MeshRenderer).bounds.size.z;
			}
		}
	}
	
	public void SetNext(TerrainTag innext){
		next=innext;
		Transform []nextroadPath;
		nextroadPath=next.getRoadPath();
		
		roadPathTransformArray.Add(nextroadPath[1]);	
		roadPathTransformArray.Add(nextroadPath[2]);
	}
	
	public void SetPrev(TerrainTag inprev){
		prev=inprev;
		
		Transform []prevroadPath;
		prevroadPath=prev.getRoadPath();
		
		roadPathTransformArray.Insert(0,prevroadPath[prevroadPath.Length-1]);
		roadPathTransformArray.Insert(0,prevroadPath[prevroadPath.Length-2]);
	}
	
	public TerrainTag GetPrevTerrain()
	{
		return prev;
	}
	
	public Transform[] getRoadPath()
	{
		if(roadPathTransform==null){
			ParsePath();
		}
		return roadPathTransform;
	}
	
	private void ParsePath()
	{
		if(!GlobalOptions.flagOnlyFizik)
		{
			ParsePath3D();
		}
	}
	
	private void ParsePath3D()
	{		
		Transform Path3D=singleTransform.FindChild("Path3D");
		Transform rotationPoint;

		if(rotationPoint=Path3D.FindChild("rotationpointright"))
		{
			FlagLeft=false;
		}else
		if(rotationPoint=Path3D.FindChild("rotationpointleft"))
		{
			FlagLeft=true;
		}
		
		//find all marks
		Transform[] allChildrenPaht3D = Path3D.gameObject.GetComponentsInChildren<Transform>();
		
		//sort array bubblesort need to be changed to qsort or heapsort
		int a, b;
  		Transform t;

	  	for(a=2; a < allChildrenPaht3D.Length; ++a)
		{
			for(b=allChildrenPaht3D.Length-1; b >= a; --b) 
			{
				if(allChildrenPaht3D[b-1].position.z >= allChildrenPaht3D[b].position.z) {
					/* exchange elements */
					t = allChildrenPaht3D[b-1];
					allChildrenPaht3D[b-1] = allChildrenPaht3D[b];
					allChildrenPaht3D[b] = t;
				}
			}
			
			//Если поворот отсечём два пути
			if(nextGoingTo!=TerrainTagNextGoingTo.FORWARD&&rotationPoint)
			{
				if(rotationPoint.position.z<allChildrenPaht3D[a].position.z)	
				{
					rotatePointIndex=a;
					break;
				}
			}
		}
		
		//sort others
		if(rotatePointIndex>0)
		{
			for(a=rotatePointIndex; a < allChildrenPaht3D.Length; ++a)
			{
			    for(b=allChildrenPaht3D.Length-1; b >= a; --b) 
				{
			      	if(((!FlagLeft)&&allChildrenPaht3D[b-1].position.x >= allChildrenPaht3D[b].position.x)||
					((FlagLeft)&&allChildrenPaht3D[b-1].position.x <= allChildrenPaht3D[b].position.x))
					{
			        	/* exchange elements */
			       	 	t = allChildrenPaht3D[b-1];
			        	allChildrenPaht3D[b-1] = allChildrenPaht3D[b];
			        	allChildrenPaht3D[b] = t;
			     	}
				}
		    }
		}
		
		//получили массив пути
		roadPathTransform=allChildrenPaht3D;
	}
	
	public void RecalculateRoadPathArray()
	{
		curDotIndex=0;
		curPos=0;
		if(roadPathTransform==null){
			ParsePath();
		}
		roadPathTransformArray.Clear();
		for (int i=1;i<roadPathTransform.Length;i++){
			roadPathTransformArray.Add(roadPathTransform[i]);
		}
		
	}
	
	public void SetCurDotIndexAndCurPos(int inindex,float inpos)
	{
		curDotIndex=inindex;
		curPos=inpos;
	}
	
	public bool GetflagNextTerrainCustom()
	{
		return flagNextTerrainCustom;
	}
	
	public void SetCustomDotIndex(int inindex,float inpos)
	{
		customDotIndex=inindex;
		customPos=inpos;
		flagNextTerrainCustom=false;
	}
	
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition, bool usecustomDotIndexAndCustomPos)
	{
		Debug.Log ("GetXandYandAngleSmexForZ");
		Vector3 returnXandYandAngle=new Vector3(0,0,0);
		int i;
		float length=0;
		float testlength;
		float needShag=inposition.z;
		float t;
		
		int tekDotIndex=0;
		float tekPos=0;
		
		if(usecustomDotIndexAndCustomPos)
		{
			tekDotIndex=customDotIndex;
			tekPos=customPos;
		}
		else
		{
			tekDotIndex=curDotIndex;
			tekPos=curPos;
		}
		
		float length1=0,length2=0;
		
		//Vector2 BezieDoty;
		Vector2 BezieDot,origin,control,destination;
		//Vector2 originy,controly,destinationy;
		
		for(i=tekDotIndex;i<roadPathTransformArray.Count-1;i++)
		{
			if(i>roadPathTransformArray.Count-2&&usecustomDotIndexAndCustomPos)
			{
				Debug.Log ("i>roadPathTransformArray.Count");
				flagNextTerrainCustom=true;
				return Vector3.zero;
			}
			//nextterrain
			if(i>roadPathTransformArray.Count-3&&!usecustomDotIndexAndCustomPos)
			{
				int newCurDot=1;
				GlobalOptions.GetWorldFactory().GetComponent<WorldFactory>().TryAddTerrrain();
				(abstractElementFactory as TerrainElementFactory).SetNextCurrentTerrain(next);
		
				next.SetCurDotIndexAndCurPos(newCurDot,tekPos);
				return next.GetXandYandAngleSmexForZ(inposition,usecustomDotIndexAndCustomPos);
			}
			
			origin=new Vector2((roadPathTransformArray[i].position.x+roadPathTransformArray[i-1].position.x)/2,(roadPathTransformArray[i].position.z+roadPathTransformArray[i-1].position.z)/2);
			control=new Vector2(roadPathTransformArray[i].position.x,roadPathTransformArray[i].position.z);
			destination=new Vector2((roadPathTransformArray[i+1].position.x+roadPathTransformArray[i].position.x)/2,(roadPathTransformArray[i+1].position.z+roadPathTransformArray[i].position.z)/2);
			
			/////
			/*testlength=Mathf.Sqrt(Mathf.Pow (destination.x-origin.x,2)+Mathf.Pow (destination.y-origin.y,2));
			Debug.Log (tekPos);
			if(testlength-tekPos>=needShag-length)
			{
				//нашли
				tekPos+=needShag-length;
				break;
			}
			else
			{
				length+=testlength-tekPos;
				tekPos=0;
			}*/
				
			//наматываем на длинну
			if(tekPos<0&&i==tekDotIndex)
			{
				if(-tekPos>needShag)
				{
					//нашли текущую точку
					tekPos+=needShag;
					break;
				}
				else
				{
					length+=-tekPos;
					tekPos=0;
				}
				//Debug.Log ("0 Dot");
			}
			
			if(tekPos>=0&&i==tekDotIndex)
			{
				testlength=Mathf.Sqrt(Mathf.Pow (destination.x-control.x,2)+Mathf.Pow (destination.y-control.y,2));
				if(testlength-tekPos>=needShag-length)
				{
					//нашли текущую точку
					tekPos+=needShag-length;
					break;
				}
				else
				{
					length+=testlength-tekPos;
				}
			}
			
			
			if(i!=tekDotIndex)
			{
				testlength=Mathf.Sqrt(Mathf.Pow (control.x-origin.x,2)+Mathf.Pow (control.y-origin.y,2));
				
				if(testlength>=needShag-length)
				{
					//нашли текущую точку
					tekPos=-(testlength-(needShag-length));
					break;
				}
				else
				{
					length+=testlength;
				}
				
				testlength=Mathf.Sqrt(Mathf.Pow (destination.x-control.x,2)+Mathf.Pow (destination.y-control.y,2));
				if(testlength>=needShag-length)
				{
					//нашли текущую точку
					tekPos=needShag-length;
					break;
				}
				else
				{
					length+=testlength;
				}
			}
		}
		
		//фиксируем точку
		if(usecustomDotIndexAndCustomPos)
		{
			customDotIndex=i;
			customPos=tekPos;
		}
		else
		{
			curDotIndex=i;
			curPos=tekPos;
		}
		
		//Debug.Log ("i="+i+"roadPathTransformArray.Count="+roadPathTransformArray.Count);
		
		//теперь получим точки безье
		origin=new Vector2((roadPathTransformArray[i].position.x+roadPathTransformArray[i-1].position.x)/2,(roadPathTransformArray[i].position.z+roadPathTransformArray[i-1].position.z)/2);
		control=new Vector2(roadPathTransformArray[i].position.x,roadPathTransformArray[i].position.z);
		//Debug.Log (i+"flag"+usecustomDotIndexAndCustomPos);
		if(i>roadPathTransformArray.Count-2)
		{
			destination=control;
		}else
		{
			destination=new Vector2((roadPathTransformArray[i+1].position.x+roadPathTransformArray[i].position.x)/2,(roadPathTransformArray[i+1].position.z+roadPathTransformArray[i].position.z)/2);
		}
		
		//originy=new Vector2((roadPathTransformArray[i].position.y+roadPathTransformArray[i-1].position.y)/2,(roadPathTransformArray[i].position.z+roadPathTransformArray[i-1].position.z)/2);
		//controly=new Vector2(roadPathTransformArray[i].position.y,roadPathTransformArray[i].position.y);
		//destinationy=new Vector2((roadPathTransformArray[i+1].position.y+roadPathTransformArray[i].position.y)/2,(roadPathTransformArray[i+1].position.z+roadPathTransformArray[i].position.z)/2);

		length1=Mathf.Sqrt(Mathf.Pow (control.x-origin.x,2)+Mathf.Pow (control.y-origin.y,2));
		length2=Mathf.Sqrt(Mathf.Pow (destination.x-control.x,2)+Mathf.Pow (destination.y-control.y,2));
		
		//length2=Mathf.Sqrt(Mathf.Pow (destination.x-origin.x,2)+Mathf.Pow (destination.y-origin.y,2));
		
		t=(length1+tekPos)/(length1+length2);
		
		//t=(tekPos)/(length2);
	
		
		
		BezieDot=GetQuadBezieForT(origin,control,destination,t);
		//BezieDot2=GetQuadBezieForT(origin,control,destination,t-Epsilon);
		
		//BezieDoty=GetQuadBezieForT(originy,controly,destinationy,t);
		
		
		//returnXandYandAngle=new Vector3(BezieDot.x,BezieDot.y,Mathf.Atan ((BezieDot.x-BezieDot2.x)/(BezieDot.y-BezieDot2.y)));	
		returnXandYandAngle=new Vector3(BezieDot.x,0,BezieDot.y);			
		
		//returnXandYandAngle=new Vector3(BezieDot.x,BezieDoty.x,Mathf.Atan ((BezieDot.x-BezieDot2.x)/(BezieDot.y-BezieDot2.y)));
		if(t>0.5&&usecustomDotIndexAndCustomPos&&i>roadPathTransformArray.Count-2)
		{
			flagNextTerrainCustom=true;
		}
		return returnXandYandAngle;	
	}
	
	public void PushToAllElements(AbstractTag inobj){
		AllElements.Add(inobj);
	}
	
	public List<AbstractTag> GetAllElements(){
		return AllElements;
	}
	
	public void RemakeAllElementsList()
	{
		AllElements = new List<AbstractTag>();
	}
	
	public void PutToInactiveList(AbstractEnemy inobj){
		InactiveElements.Add(inobj);
	}
	
	public void MakeAllActive()
	{
		foreach (AbstractEnemy obj in InactiveElements)
		{
			obj.ReStart();
		}
		InactiveElements.Clear();
	}
	
	public Vector2 GetQuadBezieForT(Vector2 origin,Vector2 control, Vector2 destination, float t){	
		float x = Mathf.Pow(1 - t, 2) * origin.x + 2.0f * (1 - t) * t * control.x + t * t * destination.x;
		float y = Mathf.Pow(1 - t, 2) * origin.y + 2.0f * (1 - t) * t * control.y + t * t * destination.y;
		return new Vector2(x,y);
	}
}