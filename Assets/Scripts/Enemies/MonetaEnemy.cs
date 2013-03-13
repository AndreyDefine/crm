using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	
	private bool effectMade=false;
	public int numberOfMoney=1;
	private float rasstChuvstv=155;
	
	public override void OnHit(Collider other)
	{
		GuiLayer.AddMoney(numberOfMoney);
		PlayClipSound();
		//audio.Play();
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
	
	public override void ReStart()
	{
		UnMakeEffect();
		gameObject.SetActiveRecursively(true);	
		singleTransform.rotation=Quaternion.Euler(0, 0, 0);;
	}
	
	public void TestPlayer()
	{
		if(!effectMade&&playerScript.GetMagnitFlag())
		{
			float raznx,razny,raznz;
			raznx=singleTransform.position.x-characterTransform.position.x;
			raznz=singleTransform.position.z-characterTransform.position.z;
			razny=singleTransform.position.y-characterTransform.position.y;
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
			float smex=0.2f;
			
			raznx=-singleTransform.parent.position.x+characterTransform.position.x;
			razny=-singleTransform.parent.position.y+characterTransform.position.y;
			raznz=-singleTransform.parent.position.z+characterTransform.position.z;
			
			/*raznx=Mathf.Abs(raznx)>=smex?Mathf.Sign(raznx)*smex:raznx;
			razny=Mathf.Abs(razny)>=smex?Mathf.Sign(razny)*smex:razny;
			raznz=Mathf.Abs(raznz)>=smex?Mathf.Sign(raznz)*smex:raznz;*/
			
			raznx=Mathf.Abs(raznz)>1?raznx/25:raznx;
			razny=Mathf.Abs(raznz)>1?razny/25:razny;
			raznz=Mathf.Abs(raznz)>1?raznz/25:raznz;
			
			singleTransform.parent.position+=new Vector3(raznx,razny,raznz);			
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		singleTransform.parent.rigidbody.useGravity=false;
		singleTransform.parent.collider.isTrigger=true;
	}
	
	
	private void UnMakeEffect()
	{
		effectMade=false;
		singleTransform.parent.rigidbody.useGravity=true;
		singleTransform.parent.collider.isTrigger=false;
	}
}