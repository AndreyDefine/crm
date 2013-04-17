using UnityEngine;
using System.Collections;

//singleton
public class MissionStack{
	//сам стек миссий
	private ArrayList missionStack=new ArrayList();
	private static MissionStack instance = null;
	public static MissionStack sharedMissionStack()
	{
		if(instance==null)
		{
			instance=new MissionStack();
		}
		return instance;	
	}
	
	public void Push(MissionTag inTag)
	{
		missionStack.Add(inTag);
	}
	
	public MissionTag Pop()
	{
		MissionTag curTag=null;
		if(missionStack.Count>0)
		{
			curTag=missionStack[0]as MissionTag;
			missionStack.RemoveAt(0);
		}
		
		return curTag;
	}
}
