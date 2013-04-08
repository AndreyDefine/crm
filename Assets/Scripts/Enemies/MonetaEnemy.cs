using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	
	private bool effectMade=false;
	public int numberOfMoney=1;
	private float rasstChuvstv=155;
	private Transform parentTransform;
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(numberOfMoney);
		PlayClipSound();
		MakeInactive();
		effectMade=false;
	}
	
	void Update () {
		Rotate();
		TestPlayer();
		MakeMagnit();
	}
	
	public void Rotate()
	{
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*200,0));
	}
	
	public override void initEnemy()
	{
		parentTransform=singleTransform.parent;
	}
	
	public override void ReStart()
	{
		//Debug.Log ("MonetaRestart");
		UnMakeEffect();
		gameObject.SetActiveRecursively(true);	
		singleTransform.rotation=Quaternion.Euler(0, 0, 0);
	}
	
	public void TestPlayer()
	{
		if(!effectMade&&playerScript.GetMagnitFlag())
		{
			float raznx,razny,raznz;
			raznx=parentTransform.position.x-walkingBearTransform.position.x;
			raznz=parentTransform.position.z-walkingBearTransform.position.z;
			razny=parentTransform.position.y-walkingBearTransform.position.y;
			if(raznx*raznx+raznz*raznz<=rasstChuvstv&&Mathf.Abs (razny)<10)
			{
				MakeEffect();
			}
		}
	}
	
	private void MakeMagnit()
	{
		if(effectMade)
		{
			float raznx,raznz,razny;
			//float smex=0.2f;
			
			raznx=-parentTransform.position.x+walkingBearTransform.position.x;
			razny=-parentTransform.position.y+walkingBearTransform.position.y;
			raznz=-parentTransform.position.z+walkingBearTransform.position.z;
			
			if(raznx*raznx+raznz*raznz<=rasstChuvstv/30)
			{
				//do nothing
			}
			else
			{
				raznx=raznx/10;
				razny=razny/10;
				raznz=raznz/10;
			}
			
			parentTransform.position+=new Vector3(raznx,razny,raznz);			
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		parentTransform.rigidbody.useGravity=false;
		parentTransform.collider.isTrigger=true;
	}
	
	
	private void UnMakeEffect()
	{
		effectMade=false;
		if(parentTransform)
		{
			parentTransform.rigidbody.useGravity=true;
			parentTransform.collider.isTrigger=false;
		}
	}
}