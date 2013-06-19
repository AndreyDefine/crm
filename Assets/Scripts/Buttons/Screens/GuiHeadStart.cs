using UnityEngine;
using System.Collections;

public class GuiHeadStart : Abstract {
	
	private int numberOfBlinks;
	private int nedeedNumberOfBlinks=3;
	private float timeToEase;
	private float neededTimeToEase=1.6f;
	private int directionOfTwinkling;
	
	private float startstopTime=0,stopTime=0;//время остановки
	
	bool flagPaused=false;
	
	
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
		flagPaused=false;
		stopTime=0;
		startstopTime=0;
		directionOfTwinkling=1;
		numberOfBlinks=0;
		gameObject.SetActive(true);
		SetGetTouches(true);
		timeToEase=Time.time;
	}
	
	public void PauseTwinkling()
	{
		if(numberOfBlinks<=nedeedNumberOfBlinks)
		{
			if(startstopTime==0)
			{
				flagPaused=true;
				startstopTime=Time.time;
				
				SetGetTouches(false);
			}

			stopTime=Time.time-startstopTime;
		}
	}
	
	private void SetGetTouches(bool inGetTouches)
	{
		if(allTransforms!=null)
		{
			SpriteTouch curSpriteTouch;
			for (int i=0;i<allTransforms.Length;i++)
			{
				curSpriteTouch=allTransforms[i].GetComponent<SpriteTouch>();
				if(curSpriteTouch)
				{
					curSpriteTouch.getTouches=inGetTouches;
				}
			}
		}
	}
	
	public void ResumeTwinkling()
	{
		if(flagPaused&&numberOfBlinks<=nedeedNumberOfBlinks)
		{
			timeToEase+=stopTime;
			stopTime=0;
			startstopTime=0;
			flagPaused=false;
			SetGetTouches(true);
		}
	}
	
	private void Update()
	{
		if(!flagPaused&&numberOfBlinks<=nedeedNumberOfBlinks)
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
