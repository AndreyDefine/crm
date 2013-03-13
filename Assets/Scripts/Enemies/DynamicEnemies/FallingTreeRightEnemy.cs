using UnityEngine;
using System.Collections;


public class FallingTreeRightEnemy : AbstractEnemy {	
	
	private bool effectMade=false;
	private float rasstChuvstv=255;
	
	public override void OnHit(Collider other)
	{
		//Do nothing on hit
	}
	
	void Update () {
		TestPlayer();
	}
	
	public override void initEnemy()
	{
		singleTransform.rotation=Quaternion.Euler(0,0,0);
	}
	
	public void TestPlayer()
	{
		if(!effectMade)
		{
			float raznx,razny,raznz;
			raznx=singleTransform.position.x-characterTransform.position.x;
			raznz=singleTransform.position.z-characterTransform.position.z;
			razny=singleTransform.position.y-characterTransform.position.y;
			if(raznx*raznx+raznz*raznz<=rasstChuvstv&&Mathf.Abs (razny)<10)
			{
				PutToInactiveList();
				MakeEffect();
			}
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		Animation animationScript=GetComponentInChildren<Animation>();
		animationScript.Play("FallingTreeRight");
	}
	
	public override void ReStart()
	{
		effectMade=false;
		Animation animationScript=GetComponentInChildren<Animation>();
		animationScript.Play("RestartTree");;
	}
}