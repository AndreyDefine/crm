using UnityEngine;
using System.Collections;

public class FactoryBuildingPost : FactoryBuildingWihtSpriteAnimation {
	
	public GameObject[] Golubs;
	public GameObject EmmisionPoint;
	
	public GameObject LeftPost;
	public GameObject RightPost;
	
	private bool flagIsActive=false;
	private float intervalGolubRight=18;
	private float intervalGolubLeft=17;
	private float lastGolubTimeRight,lastGolubTimeLeft;
	
	public override void SetActive(bool a){	
		if(a){
			if(flagAddAnimation)
			{
				lastGolubTimeRight=Time.time;
				lastGolubTimeLeft=Time.time;
				flagIsActive=true;
			}
		}else{
			if(flagAddAnimation)
			{
				flagIsActive=false;
			}
		}
	}
	
	void Update()
	{
		if(flagIsActive)
		{
			GenerateGolubsRight();
			GenerateGolubsLeft();
		}
	}
	
	void GenerateGolubsRight()
	{
		if(Time.time-lastGolubTimeRight>intervalGolubRight-Random.Range(0,7f))
		{
			lastGolubTimeRight=Time.time;
			PlayRightPost ();
		}
	}
	
	void GenerateGolubsLeft()
	{
		if(Time.time-lastGolubTimeLeft>intervalGolubLeft-Random.Range(0,7f))
		{
			lastGolubTimeLeft=Time.time;
			PlayLeftPost();
		}
	}
	
	private void PlayLeftPost()
	{
		int RandIndex=Random.Range(0,Golubs.Length);
		GameObject Golub;
		Golub=Instantiate(Golubs[RandIndex]) as GameObject;
		Golub.transform.position=EmmisionPoint.transform.position;
		Golub.GetComponentInChildren<GolubLeft>().LeftPost=LeftPost;
	}
	
	private void PlayRightPost()
	{
		RightPost.GetComponent<tk2dAnimatedSprite>().Play("Action1");
	}

}
