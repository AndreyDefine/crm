using UnityEngine;
using System.Collections;

public class SmallBear : Abstract {
	public FactoryBuildingWihtSpriteAnimation factoryBuilding;
	
	private bool flagEasing=false;
	private float timeToEase;
	private float neededTimeToEase=3;
	
	void EaseOut()
	{
		flagEasing=true;
		timeToEase=Time.time;		
	}
	
	void Update()
	{	
		if(flagEasing)
		{
			if(Time.time-timeToEase>neededTimeToEase)
			{
				singleTransform.localPosition=new Vector3(0,0,0);
				GetComponent<tk2dSprite>().color=new Color(1,1,1,1);
				flagEasing=false;
				factoryBuilding.PlayAnimation();
			}
			else
			{
				GetComponent<tk2dSprite>().color=new Color(1,1,1,(neededTimeToEase-(Time.time-timeToEase))/neededTimeToEase);
			}
		}
	}
}
