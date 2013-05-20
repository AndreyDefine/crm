using UnityEngine;
using System.Collections;


public class KorovaEnemy : AbstractEnemy {	
	
	public float rasstChuvstv=255;
	
	private bool effectMade=false;
	
	private Animation animationScript;
	
	float animationSpeed;
	
	public override void OnHit(Collider other)
	{
		//Do nothing on hit
	}
	
	void Update () {
		TestPlayer();
	}
	
	public override void initEnemy()
	{
		rasstChuvstv+=Random.Range(-1000,1000);
		
		animationSpeed=Random.Range(0.5f,1.0f);
		//Debug.Log (animationSpeed);
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
				animationScript["Korova_SetUp_Anim_2"].speed=0;
				Debug.Log ("GamePaused Korova");
			}
			else
			{
				animationScript["Korova_SetUp_Anim_2"].speed=playerScript.GetVelocityCurMnoshitel()*animationSpeed;
			}
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		animationScript.Play("Korova_SetUp_Anim_2");
	}
	
	public override void ReStart()
	{
		effectMade=false;
		Animation animationScript=GetComponentInChildren<Animation>();
		if(animationScript)
		{
			//animationScript.Play("Restart");
		}
	}
}