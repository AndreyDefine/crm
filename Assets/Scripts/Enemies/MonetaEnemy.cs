using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	
	private bool effectMade=false;
	public int numberOfMoney=1;
	private float rasstChuvstv=155;
	private Transform parentTransform;
	
	private Transform oldParent;
	
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
		
	}
	
	void LateUpdate()
	{
		MakeMagnit();
	}
	
	public void Rotate()
	{
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
		{
			return;
		}
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*200,0));
	}
	
	public override void initEnemy()
	{
		parentTransform=singleTransform.parent.parent;
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
			if(raznx*raznx+raznz*raznz<=rasstChuvstv&&Mathf.Abs (razny)<20)
			{
				MakeEffect();
			}
		}
	}
	
	private void MakeMagnit()
	{
		if(effectMade)
		{
			bool flagMoving=false;
			float raznx,raznz,razny;
			float smex=0.35f;
			raznx=-parentTransform.position.x+walkingBearTransform.position.x;
			razny=-parentTransform.position.y+walkingBearTransform.position.y+1;
			raznz=-parentTransform.position.z+walkingBearTransform.position.z;
	
			float delitel=20;
			if(Mathf.Abs(raznx)>smex)
			{
				//raznx/=Mathf.Abs(raznx)>smex*3?delitel:delitel/1.5f;
				raznx/=delitel;
				flagMoving=true;
			}
			if(Mathf.Abs(razny)>smex)
			{
				//razny/=Mathf.Abs(razny)>smex*3?delitel:delitel/1.5f;
				razny/=delitel;
				flagMoving=true;
			}
				
			if(Mathf.Abs(raznz)>smex)
			{
				//raznz/=Mathf.Abs(raznz)>smex*3?delitel:delitel/1.5f;
				raznz/=delitel;
				flagMoving=true;
			}
			
			parentTransform.position+=new Vector3(raznx,razny,raznz);	
			if(!flagMoving)
			{
				parentTransform.parent=oldParent;
			}
		}
	}
	
	private void MakeEffect()
	{
		effectMade=true;
		oldParent=parentTransform.parent;
		parentTransform.parent=playertransform;
	}
	
	
	private void UnMakeEffect()
	{
		effectMade=false;
		if(parentTransform)
		{
			parentTransform.parent=oldParent;
		}
	}
}