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
	
	//rotation
	public int rotatePointIndex=0;
	private	bool FlagLeft=false;
	private int startindex=0;
	
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
		Vector3 NormalizeVector=new Vector3(0,0,1);
		nextroadPath=next.getRoadPath();
		
		if(rotatePointIndex>0)
		{
			NormalizeVector=new Vector3(-1,0,0);
		}
		
		curDot=GlobalOptions.NormalizeVector3Smex(nextroadPath[0],NormalizeVector);
		curDot+=GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=sizeOfPlane/2+(next.sizeOfPlane/2+curDot.z);
		}
		else
		{
			if(rotatePointIndex>0)
			{
				curDot.x+=next.sizeOfPlane/2;
			}
			else
			{
				curDot.z+=next.sizeOfPlane/2;
			}
		}
		roadPathArray.Add(curDot);		
		
		curDot=GlobalOptions.NormalizeVector3Smex(nextroadPath[1],NormalizeVector);
		curDot+=GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=sizeOfPlane/2+(next.sizeOfPlane/2+curDot.z);
		}
		else
		{
			if(rotatePointIndex>0)
			{
				curDot.x+=next.sizeOfPlane/2;
			}
			else
			{
				curDot.z+=next.sizeOfPlane/2;
			}
		}
		roadPathArray.Add(curDot);	
		
		for (int i=0;i<roadPathArray.Count&&rotatePointIndex>0;i++)
		{
			Debug.Log (rotatePointIndex+" "+roadPathArray[i]);
		}
	}
	
	public void SetPrev(TerrainTag inprev){
		prev=inprev;
		
		Vector3 []prevroadPath;
		Vector3 curDot;
		prevroadPath=prev.getRoadPath();
		Vector3 NormalizeVector=new Vector3(0,0,1);
		
		if(prev.rotatePointIndex>0)
		{
			NormalizeVector=new Vector3(-1,0,0);
		}
		
		
		curDot=GlobalOptions.NormalizeVector3Smex(prevroadPath[prevroadPath.Length-1],NormalizeVector);
		curDot+=-prev.GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=-sizeOfPlane/2+(-prev.sizeOfPlane/2+curDot.z);
		}
		else
		{
			if(prev.rotatePointIndex>0)
			{
				curDot.z-=sizeOfPlane/2;
			}
			else
			{
				curDot.z-=sizeOfPlane/2;
			}
		}
		roadPathArray.Insert(0,curDot);
		
		curDot=GlobalOptions.NormalizeVector3Smex(prevroadPath[prevroadPath.Length-2],NormalizeVector);
		curDot+=-prev.GetEndOfTerrainLocal();
		if(!isEndOfTerrain())
		{
			curDot.z=-sizeOfPlane/2+(-prev.sizeOfPlane/2+curDot.z);
		}
		else
		{
			if(prev.rotatePointIndex>0)
			{
				curDot.z-=sizeOfPlane/2;
			}
			else
			{
				curDot.z-=sizeOfPlane/2;
			}
		}
		roadPathArray.Insert(0,curDot);
		
		for (int i=0;i<roadPathArray.Count&&prev.rotatePointIndex>0;i++)
		{
			Debug.Log (rotatePointIndex+" Prev "+roadPathArray[i]);
		}
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

		if(rotationPoint=Path3D.FindChild("rotationpointright"))
		{
			FlagLeft=false;
		}else
		if(rotationPoint=Path3D.FindChild("rotationpointleft"))
		{
			FlagLeft=true;
		}
		
		int i;
		
		//find all marks
		Transform[] allChildrenPaht3D = Path3D.gameObject.GetComponentsInChildren<Transform>();
		
		roadPath= new Vector3[allChildrenPaht3D.Length-1];
		
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
		

		//calculate mnoshitel
		mnoshitel=1;
		//получили массив пути
		for (i=1;i<allChildrenPaht3D.Length;i++){
			//normalized path
			roadPath[i-1]=allChildrenPaht3D[i].position-singleTransform.position;
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
		startindex=0;
		if(roadPath==null){
			ParsePath();
		}
		roadPathArray.Clear();
		for (int i=0;i<roadPath.Length;i++){
			roadPathArray.Add(roadPath[i]);
		}
		
	}
	
	public float GetLastPointPos()
	{
		float result=0;
		if(startindex==0)
		{
			result=sizeOfPlane/2;
		}
		else
		{
			Vector3 localEndOfTerrain=GetEndOfTerrainLocal();
			result=GlobalOptions.NormalizeVector3Smex(localEndOfTerrain,new Vector3(-1,0,0)).z;
			Debug.Log ("result="+result);
		}
		
		return result;
	}
	
	public Vector3 GetXandYandAngleSmexForZ(Vector3 inposition)
	{
		float inz=GlobalOptions.NormalizeVector3Smex(inposition,GlobalOptions.whereToGo).z;
		float terz=GlobalOptions.NormalizeVector3Smex(singleTransform.position,GlobalOptions.whereToGo).z;
		float razn=0;
		
		if(GlobalOptions.whereToGo.z>0||GlobalOptions.whereToGo.x>0){
			razn=inz-terz;
		}
		else
		{
			razn=-inz+terz;
		}
		
		Debug.Log ("razn="+razn);
	
		bool RotationFlag=false;
		Vector3 returnXandYandAngle;
		float Epsilon=0.01f;
		int i=0;
		Vector2 BezieDoty;//,BezieDot2y;
		Vector2 BezieDot,BezieDot2,origin,control,destination;
		Vector2 originy,controly,destinationy;
		Vector3 NormalizeVector=new Vector3(0,0,1);
		float t;
		
		Vector3 vspDot1,vspDot2;
		
		float ysmex=singleTransform.position.y;
		float xsmex=GlobalOptions.NormalizeVector3Smex(singleTransform.position,GlobalOptions.whereToGo).x;
		
		if(startindex!=0)
		{
			xsmex=GlobalOptions.NormalizeVector3Smex(singleTransform.position,GlobalOptions.whereToGo).z;
		}
		
		Debug.Log("xsmex="+xsmex+" ysmex="+ysmex);
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
		
		if (startindex!=0&&!FlagLeft)
		{
			NormalizeVector=new Vector3(-1,0,0);
		}
		
		for (i=startindex;i<roadPathArray.Count;i++){
			//rotation
			if(startindex==0&&rotatePointIndex<i&&nextGoingTo!=TerrainTagNextGoingTo.FORWARD)
			{
				startindex=i;
				GlobalOptions.whereToGo=GlobalOptions.TurnLeftRightVector(GlobalOptions.whereToGo,FlagLeft);
				GlobalOptions.GetPlayerScript().rotatePersone();
				RotationFlag=true;
				break;
				//need rotation
			}
			
	
			if(i<roadPathArray.Count-2)
			{
				vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
			}
			else
			{
				vspDot1=roadPathArray[i];
			}
			
			if(vspDot1.z>=razn)
			{
				Debug.Log (i+" Fount Point "+vspDot1);
				break;
			}
		}	
		
		if(RotationFlag)
		{
			//call recursively
			Debug.Log ("Recursively/////////////////////////////////////////  "+startindex);
			return GetXandYandAngleSmexForZ(inposition);
		}
		
		i=i>roadPathArray.Count-1?roadPathArray.Count-1:i;

		origin=originy=new Vector2(0,0);
		control=controly=new Vector2(0,0);
		destination=destinationy=new Vector2(0,0);

		//ищем середины	
		do{
			if(i>0){
				
				//if(i<roadPathArray.Count-2)
				{
					vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
					vspDot2=GlobalOptions.NormalizeVector3Smex(roadPathArray[i-1],NormalizeVector);
				}
				/*else
				{
					vspDot1=roadPathArray[i];
					vspDot2=roadPathArray[i-1];
				}*/
				
				vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
				vspDot2=GlobalOptions.NormalizeVector3Smex(roadPathArray[i-1],NormalizeVector);
				
				origin=new Vector2((vspDot1.x-vspDot2.x)/2+vspDot2.x+xsmex,(vspDot1.z-vspDot2.z)/2+vspDot2.z);
				originy=new Vector2((vspDot1.y-vspDot2.y)/2+vspDot2.y+ysmex,(vspDot1.z-vspDot2.z)/2+vspDot2.z);
			}else{
				origin=new Vector2(roadPathArray[i].x+xsmex,roadPathArray[i].z);
				originy=new Vector2(roadPathArray[i].y+ysmex,roadPathArray[i].z);
			}
			
			//не угадали с положением точки
			if(origin.y>razn)
			{
				i--;
			}
		}
		while(origin.y>razn&&i>=0); 
		i=i<0?0:i;
		
		//if(i<roadPathArray.Count-2)
		{
			vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
		}
		/*else
		{
			vspDot1=roadPathArray[i];
		}*/
		
		control=new Vector2(vspDot1.x+xsmex,vspDot1.z);
		controly=new Vector2(vspDot1.y+ysmex,vspDot1.z);
		
		if(i!=roadPathArray.Count-1){
			
			//if(i<roadPathArray.Count-2)
			{
				vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
				vspDot2=GlobalOptions.NormalizeVector3Smex(roadPathArray[i+1],NormalizeVector);
			}
			/*else
			{
				vspDot1=roadPathArray[i];
				vspDot2=roadPathArray[i+1];
			}*/
			
			destination=new Vector2((vspDot2.x-vspDot1.x)/2+vspDot1.x+xsmex,(vspDot2.z-vspDot1.z)/2+vspDot1.z);
			destinationy=new Vector2((vspDot2.y-vspDot1.y)/2+vspDot1.y+ysmex,(vspDot2.z-vspDot1.z)/2+vspDot1.z);
		}else{
			//last dot
			vspDot1=GlobalOptions.NormalizeVector3Smex(roadPathArray[i],NormalizeVector);
			
			Debug.Log ("LAST DOT");
			destination=new Vector2(vspDot1.x+xsmex,vspDot1.z);
			destinationy=new Vector2(vspDot1.y+ysmex,vspDot1.z);
		}			
		
		
		Debug.Log ("origin="+origin+"control="+control+"destination="+destination);
		
		t=(razn-origin.y)/(destination.y-origin.y);
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