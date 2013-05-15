using UnityEngine;
using System.Collections;


public class ElkMovingEnemy : AbstractEnemy {	
	
	public float rasstChuvstv=255;
	
	private bool effectMade=false;
	
	private Animation animationScript;
	private Animation animationScriptBaran;
	
	public override void OnHit(Collider other)
	{
		//Do nothing on hit
	}
	
	void Update () {
		TestPlayer();
	}
	
	public override void initEnemy()
	{
		animationScript=GetComponentInChildren<Animation>();
		animationScriptBaran=null;		
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
		else
		{
			if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
			{
				animationScript["MoveAnimation"].speed=0;
				if(animationScriptBaran)
					animationScriptBaran["Take_001"].speed=0;
			}
			else
			{
				animationScript["MoveAnimation"].speed=playerScript.GetVelocityCurMnoshitel();
				if(animationScriptBaran)
					animationScriptBaran["Take_001"].speed=playerScript.GetVelocityCurMnoshitel()*2.5;
			}
		}
	}
	
	private void MakeEffect()
	{
		if(!animationScriptBaran)
		{
			int i;
			Transform[] allChildren = singleTransform.gameObject.GetComponentsInChildren<Transform>();
			for(i=1;i<allChildren.Length;i++)
			{
				if(allChildren[i].name=="Baran")
				{
					animationScriptBaran=allChildren[i].GetComponentInChildren<Animation>();
				}
			}
		}
		effectMade=true;
		animationScript.Play("MoveAnimation");
		if(animationScriptBaran)
			animationScriptBaran.Play("Take_001");
	}
	
	public override void ReStart()
	{
		effectMade=false;
		if(animationScriptBaran)
		{
			animationScriptBaran["Take_001"].speed=0;
		}
		if(animationScript)
		{
			animationScript.Play("Restart");
		}
	}
}