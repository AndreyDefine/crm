using UnityEngine;
using System.Collections;


public class MovingEnemy : AbstractEnemy {	
	
	public float rasstChuvstv=255;
	
	private bool effectMade=false;
	
	private Animation animationScript;
	
	public bool GetEffectMade(){
		return effectMade;
	}
	
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
			}
			else
			{
				animationScript["MoveAnimation"].speed=playerScript.GetVelocityCurMnoshitel();
			}
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		animationScript.Play("MoveAnimation");
	}
	
	public override void ReStart()
	{
		effectMade=false;
		Animation animationScript=GetComponentInChildren<Animation>();
		if(animationScript)
		{
			animationScript.Stop ();
			animationScript.gameObject.transform.localPosition=new Vector3(0,0,0);
			//animationScript.Play("Restart");
		}
	}
}