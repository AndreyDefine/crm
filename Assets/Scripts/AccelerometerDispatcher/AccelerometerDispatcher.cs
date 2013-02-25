using UnityEngine;
using System.Collections;

public class AccelerateInfo
{
	public bool swallowsAcceles;
	public int accelPriority;
	public int fingerId;
	public AccelerateInfo(){
		accelPriority=1;
		swallowsAcceles=false;
		fingerId=-1;
	}
}


public class AccelerometerDispatcher : MonoBehaviour {
	
	private ArrayList targetedHandlers;
	private ArrayList targetedHandlersAccelerateInfo;
	
	 void Start () {
		targetedHandlers = new ArrayList();
		targetedHandlersAccelerateInfo=new ArrayList();
	}  
	
	public void addTargetedDelegate(AccelerometerTargetedDelegate intarget,int inaccelPriority,bool inswallowsAcceles)
	{	
		int i=0;
		//searching for place to insert delegate
		for (i=0;i<targetedHandlers.Count;i++)
		{
			if((targetedHandlersAccelerateInfo[i] as AccelerateInfo).accelPriority>inaccelPriority)
				break;
		}
		
		targetedHandlers.Insert(i,intarget);
		
		AccelerateInfo newAccelInfo=new AccelerateInfo();
		newAccelInfo.swallowsAcceles=inswallowsAcceles;
		newAccelInfo.accelPriority=inaccelPriority;
		
		targetedHandlersAccelerateInfo.Insert(i,newAccelInfo);
	}
	
	void removeDelegate(TouchTargetedDelegate intarget)
	{
		int index=targetedHandlers.IndexOf(intarget);
		targetedHandlersAccelerateInfo.RemoveAt(index);
		targetedHandlers.Remove(intarget);
	}
	
	void removeAllDelegates()
	{
		targetedHandlers.Clear();
		targetedHandlersAccelerateInfo.Clear();
	}
	
	private void Update() { 
		if(targetedHandlers!=null&&targetedHandlers.Count>0){
			MakeDetectionAcceleration();
		}
	}
	
	protected virtual void MakeDetectionAcceleration()
	{		
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        	//do nothing
		#else
			Vector3 acceleration;
		
			#if UNITY_IPHONE &&  !UNITY_EDITOR
	  			acceleration=new Vector3(Input.acceleration.x,Input.acceleration.y,Input.acceleration.z);
			#endif
	
			#if UNITY_ANDROID && !UNITY_EDITOR
				acceleration=new Vector3(-Input.acceleration.y,-Input.acceleration.x,Input.acceleration.z);
			#endif
			
			if(Screen.orientation==ScreenOrientation.PortraitUpsideDown){
			
				#if UNITY_IPHONE && !UNITY_EDITOR
					acceleration.x=-acceleration.x;
					acceleration.y=-acceleration.y;
				#endif
				
				#if UNITY_ANDROID && !UNITY_EDITOR
					//acceleration.x=acceleration.x;
					acceleration.y=-acceleration.y;
				#endif
			}
       		Accelerate(acceleration,1);
		#endif
	}
	
	public virtual bool Accelerate(Vector3 acceleration,int infingerId) {
		for (int i=0;i<targetedHandlers.Count;i++)
		{
			if((targetedHandlers[i] as AccelerometerTargetedDelegate).Accelerate(acceleration,infingerId)){
				(targetedHandlersAccelerateInfo[i] as AccelerateInfo).fingerId=infingerId;
				if((targetedHandlersAccelerateInfo[i] as AccelerateInfo).swallowsAcceles){
					break;
				}
			}
		}
		return true;
	}
	
	public virtual void ReleaseAccelerometer() {
		for (int i=0;i<targetedHandlers.Count;i++)
		{
			(targetedHandlersAccelerateInfo[i] as AccelerateInfo).fingerId=-1;
		}
	}
}
