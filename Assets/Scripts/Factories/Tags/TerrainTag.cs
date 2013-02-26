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
	public string pathString="";
	public TerrainTagNextGoingTo nextGoingTo=TerrainTagNextGoingTo.FORWARD;
	public float sizeOfTexture;
	public float sizeOfPlane;
	
	public bool HandMade=false;
	public bool Dynamic=false;
	public bool Path3D=false;
	
	public bool DynamicSize=true;
	
	protected Vector3 endOfTerrain;
	
	protected Vector3 []roadPath=null;
	
	protected List<Vector3> roadPathArray=new List<Vector3>();
	
	protected ArrayList AllElements=new ArrayList();
	protected ArrayList InactiveElements=new ArrayList();
	
	private float mnoshitel;
	
	private TerrainTag next=null,prev=null;
	
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
			endOfTerrain=EndOfTerrain.transform.position;
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
		Vector3 []nextroadPath;
		Vector3 curDot;
		nextroadPath=next.getRoadPath();
		
		curDot=nextroadPath[0];
		curDot+=GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=sizeOfPlane/2+(next.sizeOfPlane/2+curDot.z);
		}
		else
		{
			curDot.z+=next.sizeOfPlane/2;
		}
		roadPathArray.Add(curDot);		
		
		curDot=nextroadPath[1];
		curDot+=GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=sizeOfPlane/2+(next.sizeOfPlane/2+curDot.z);
		}
		else
		{
			curDot.z+=next.sizeOfPlane/2;
		}
		roadPathArray.Add(curDot);	
	}
	
	public void SetPrev(TerrainTag inprev){
		prev=inprev;
		
		Vector3 []prevroadPath;
		Vector3 curDot;
		prevroadPath=prev.getRoadPath();
		
		curDot=prevroadPath[prevroadPath.Length-1];
		curDot+=-prev.GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=-sizeOfPlane/2+(-prev.sizeOfPlane/2+curDot.z);
		}
		else
		{
			curDot.z+=-sizeOfPlane/2;
		}
		roadPathArray.Insert(0,curDot);
		
		curDot=prevroadPath[prevroadPath.Length-2];
		curDot+=-prev.GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=-sizeOfPlane/2+(-prev.sizeOfPlane/2+curDot.z);
		}
		else
		{
			curDot.z+=-sizeOfPlane/2;
		}
		roadPathArray.Insert(0,curDot);
	}
	
	public TerrainTag GetPrevTerrain()
	{
		return prev;
	}
	
	public Vector3[] getRoadPath()
	{
		if(roadPath==null){
			ParsePath();
		}
		return roadPath;
	}
	
	private void ParsePath()
	{
		if(Path3D){
			ParsePath3D();
		}
		else{
			ParsePathVertexHelper();
		}
	}
	
	private void ParsePath3D()
	{		
		Transform Path3D=singleTransform.FindChild("Path3D");
		Transform rotationPoint;
		bool FlagRight;
		if(rotationPoint=Path3D.FindChild("rotationpointright"))
		{
			FlagRight=true;
		}else
		if(rotationPoint=Path3D.FindChild("rotationpointleft"))
		{
			FlagRight=false;
		}
		
		int i;
		int rotateCount=0;
		
		//find all marks
		Transform[] allChildrenPaht3D = Path3D.gameObject.GetComponentsInChildren<Transform>();
		
		roadPath= new Vector3[allChildrenPaht3D.Length-1];
		
		//sort array bubblesort need to be changed to qsort or heapsort
		int a, b;
  		Transform t;

	  	for(a=2; a < allChildrenPaht3D.Length; ++a)
		    for(b=allChildrenPaht3D.Length-1; b >= a; --b) {
		      if(allChildrenPaht3D[b-1].position.z >= allChildrenPaht3D[b].position.z) {
		        /* exchange elements */
		        t = allChildrenPaht3D[b-1];
		        allChildrenPaht3D[b-1] = allChildrenPaht3D[b];
		        allChildrenPaht3D[b] = t;
		      }
			
			//Если поворот отсечём два пути
			if(nextGoingTo!=TerrainTagNextGoingTo.FORWARD&&rotationPoint)
			{
				if(rotationPoint.position.z<=allChildrenPaht3D[a].position.z)	
				{
					rotateCount++;
				}
			}
	    }

		//calculate mnoshitel
		mnoshitel=1;
		//получили массив пути
		for (i=1;i<allChildrenPaht3D.Length-rotateCount;i++){
			//normalized path
			roadPath[i-1]=new Vector3(allChildrenPaht3D[i].position.x-singleTransform.position.x,allChildrenPaht3D[i].position.y-singleTransform.position.y,allChildrenPaht3D[i].position.z-singleTransform.position.z);
			//roadPath[i-1]=new Vector3(allChildrenPaht3D[i].localPosition.x,allChildrenPaht3D[i].localPosition.y,allChildrenPaht3D[i].localPosition.z);
		}
	}
	
	private void ParsePathVertexHelper()
	{
		//calculate mnoshitel
		mnoshitel=sizeOfPlane/sizeOfTexture;
		//получили массив пути
		char []separator={',','\n'};
		string []numbers=pathString.Split(separator);
		roadPath= new Vector3[numbers.Length/2];
		for (int i=0;i+1<numbers.Length;i+=2){
			//normalized path
			roadPath[i/2]=new Vector3(float.Parse(numbers[i],NumberStyles.Currency)*mnoshitel,0,float.Parse(numbers[i+1],NumberStyles.Currency)*mnoshitel);
		}
	}
	
	public void RecalculateRoadPathArray()
	{
		if(roadPath==null){
			ParsePath();
		}
		roadPathArray.Clear();
		for (int i=0;i<roadPath.Length;i++){
			roadPathArray.Add(roadPath[i]);
		}
		
	}
	
	public Vector3 GetXandYandAngleSmexForZ(float inz)
	{
		Vector3 returnXandYandAngle;
		float Epsilon=0.01f;
		int i=0;
		Vector2 BezieDoty;//,BezieDot2y;
		Vector2 BezieDot,BezieDot2,origin,control,destination;
		Vector2 originy,controly,destinationy;
		float t;
		
		float ysmex=singleTransform.position.y;
		float xsmex=GlobalOptions.NormalizeVector3Smex(singleTransform.position,GlobalOptions.whereToGo).x;
		if(GlobalOptions.whereToGo.x!=0)
		{
			xsmex=-xsmex;
		}
		//calculate xpos
		if(roadPath==null){
			ParsePath();
		}
		
		if(roadPathArray.Count==0)
		{
			Debug.Log ("000");
			return new Vector3(0,0,0);
		}
		
		for (i=0;i<roadPathArray.Count;i++){
			if(roadPathArray[i].z>=inz){
				break;
			}
		}	
		
		i=i>roadPathArray.Count-1?roadPathArray.Count-1:i;

		origin=originy=new Vector2(roadPathArray[i].x,roadPathArray[i].z);
		control=controly=new Vector2(roadPathArray[i].x,roadPathArray[i].z);
		destination=destinationy=new Vector2(roadPathArray[i].x,roadPathArray[i].z);

		//ищем середины	
		do{
			if(i>0){
				origin=new Vector2((roadPathArray[i].x-roadPathArray[i-1].x)/2+roadPathArray[i-1].x+xsmex,(roadPathArray[i].z-roadPathArray[i-1].z)/2+roadPathArray[i-1].z);
				originy=new Vector2((roadPathArray[i].y-roadPathArray[i-1].y)/2+roadPathArray[i-1].y+ysmex,(roadPathArray[i].z-roadPathArray[i-1].z)/2+roadPathArray[i-1].z);
			}else{
				origin=new Vector2(roadPathArray[i].x+xsmex,roadPathArray[i].z);
				originy=new Vector2(roadPathArray[i].y+ysmex,roadPathArray[i].z);
			}
			
			//не угадали с положением точки
			if(origin.y>inz)
			{
				i--;
			}
		}
		while(origin.y>inz&&i>=0); 
		i=i<0?0:i;
		
		control=new Vector2(roadPathArray[i].x+xsmex,roadPathArray[i].z);
		controly=new Vector2(roadPathArray[i].y+ysmex,roadPathArray[i].z);
		
		if(i!=roadPathArray.Count-1){
			destination=new Vector2((roadPathArray[i+1].x-roadPathArray[i].x)/2+roadPathArray[i].x+xsmex,(roadPathArray[i+1].z-roadPathArray[i].z)/2+roadPathArray[i].z);
			destinationy=new Vector2((roadPathArray[i+1].y-roadPathArray[i].y)/2+roadPathArray[i].y+ysmex,(roadPathArray[i+1].z-roadPathArray[i].z)/2+roadPathArray[i].z);
		}else{
			destination=new Vector2(roadPathArray[i].x+xsmex,roadPathArray[i].z);
			destinationy=new Vector2(roadPathArray[i].y+ysmex,roadPathArray[i].z);
		}			
		
		
		t=(inz-origin.y)/(destination.y-origin.y);
		BezieDot=GetQuadBezieForT(origin,control,destination,t);
		BezieDot2=GetQuadBezieForT(origin,control,destination,t-Epsilon);
		
		BezieDoty=GetQuadBezieForT(originy,controly,destinationy,t);
		//BezieDot2y=GetQuadBezieForT(originy,controly,destinationy,t-Epsilon);
		
		
		returnXandYandAngle=new Vector3(BezieDot.x,BezieDoty.x,Mathf.Atan ((BezieDot.x-BezieDot2.x)/(BezieDot.y-BezieDot2.y)));
		
		return returnXandYandAngle;	
	}
	
	public void PushToAllElements(GameObject inobj){
		AllElements.Add(inobj);
	}
	
	public ArrayList GetAllElements(){
		return AllElements;
	}
	
	public void PutToInactiveList(GameObject inobj){
		InactiveElements.Add(inobj);
	}
	
	public void MakeAllActive()
	{
		foreach (GameObject obj in InactiveElements)
		{
			obj.GetComponent<AbstractEnemy>().ReStart();
		}
		InactiveElements.Clear();
	}
	
	public Vector2 GetQuadBezieForT(Vector2 origin,Vector2 control, Vector2 destination, float t){	
		float x = Mathf.Pow(1 - t, 2) * origin.x + 2.0f * (1 - t) * t * control.x + t * t * destination.x;
		float y = Mathf.Pow(1 - t, 2) * origin.y + 2.0f * (1 - t) * t * control.y + t * t * destination.y;
		return new Vector2(x,y);
	}
}