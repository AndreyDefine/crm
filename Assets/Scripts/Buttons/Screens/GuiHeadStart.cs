using UnityEngine;
using System.Collections;

public class GuiHeadStart : Abstract {
	
	private int numberOfBlinks;
	private int nedeedNumberOfBlinks=7;
	private float timeToEase;
	private float neededTimeToEase=1.3f;
	private int directionOfTwinkling;
	
	private Transform[] allTransforms;
	
	private void Start()
	{
		allTransforms=gameObject.GetComponentsInChildren<Transform>();
	}
	
	public void ResetTwinkling()
	{
		gameObject.SetActive(false);
	}
	
	public void StartTwinkling()
	{
		directionOfTwinkling=1;
		numberOfBlinks=0;
		gameObject.SetActive(true);
		timeToEase=Time.time;
	}
	
	private void Update()
	{
		if(numberOfBlinks<=nedeedNumberOfBlinks)
		{
			tk2dSprite curSprite;
			tk2dTextMesh curMesh;
			float opacity;
			if(directionOfTwinkling==0)
			{
				opacity=(neededTimeToEase-(Time.time-timeToEase))/neededTimeToEase;
				if(opacity<=0)
				{
					timeToEase=Time.time;
					directionOfTwinkling=1;
					numberOfBlinks++;
				}
			}else
			{
				opacity=((Time.time-timeToEase))/neededTimeToEase;
				if(opacity>=1)
				{
					timeToEase=Time.time;
					directionOfTwinkling=0;
					numberOfBlinks++;
				}
			}
			
			for (int i=0;i<allTransforms.Length;i++)
			{
				curSprite=allTransforms[i].GetComponent<tk2dSprite>();
				curMesh=allTransforms[i].GetComponent<tk2dTextMesh>();
				if(curSprite)
				{
					MakeOpacityChangeSprite(curSprite,opacity);
				}
				
				if(curMesh)
				{
					MakeOpacityChangeText(curMesh,opacity);
				}
			}
		}
		
		if(numberOfBlinks>nedeedNumberOfBlinks)
		{
			StopTwinkling();
		}
	}
	
	private void MakeOpacityChangeSprite(tk2dSprite inSprite,float opacity)
	{
		inSprite.color=new Color(1,1,1,opacity);
	}
	
	private void MakeOpacityChangeText(tk2dTextMesh inMesh,float opacity)
	{
		inMesh.color=new Color(1,1,1,opacity);
		inMesh.Commit();
	}
	
	private void MekaOpacity0()
	{
		if(allTransforms!=null)
		{
			tk2dSprite curSprite;
			tk2dTextMesh curMesh;
			for (int i=0;i<allTransforms.Length;i++)
			{
				curSprite=allTransforms[i].GetComponent<tk2dSprite>();
				curMesh=allTransforms[i].GetComponent<tk2dTextMesh>();
				if(curSprite)
				{
					MakeOpacityChangeSprite(curSprite,0);
				}
				
				if(curMesh)
				{
					MakeOpacityChangeText(curMesh,0);
				}
			}
		}
	}
	
	
	public void StopTwinkling()
	{
		gameObject.SetActive(false);
		numberOfBlinks=nedeedNumberOfBlinks+1;
		MekaOpacity0();
	}
}
