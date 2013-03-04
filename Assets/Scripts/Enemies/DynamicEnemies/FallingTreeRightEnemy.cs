using UnityEngine;
using System.Collections;


public class FallingTreeRightEnemy : AbstractEnemy {	
	
	private bool effectMade=false;
	
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
			float raznx,raznz;
			raznx=singleTransform.position.x-playertransform.position.x;
			raznz=singleTransform.position.z-playertransform.position.z;
			if(raznx*raznx+raznz*raznz<=225)
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