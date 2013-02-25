using UnityEngine;
using System.Collections;


public class FallingTreeEnemy : AbstractEnemy {	
	
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
			if(Mathf.Abs(singleTransform.position.z-playertransform.position.z)<=8)
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
		animationScript.Play("FallingTree");
	}
	
	public override void ReStart()
	{
		effectMade=false;
		Animation animationScript=GetComponentInChildren<Animation>();
		animationScript.Play("RestartTree");;
	}
}