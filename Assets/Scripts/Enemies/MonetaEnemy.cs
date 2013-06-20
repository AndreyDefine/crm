using UnityEngine;
using System.Collections;


public class MonetaEnemy : AbstractEnemy {	
	
	private int effectMade=0;
	public int numberOfMoney=1;
	private float rasstChuvstv=155;
	private Transform parentTransform;
	
	private bool flagRotation=false;
	
	private float camx,camy;
	
	private bool flagPlusPlayerSpeed=false;
	
	private Transform cameraTransform;
	
	//private Transform oldParent;
	
	public override void OnHit(Collider other)
	{
		if(effectMade!=2)
		{
			GlobalOptions.GetPlayerScript().moneyCollected.Play();
			PlayClipSound();
			//MakeInactive();
			effectMade=2;
			camx=0;
			camy=-5;
		}
	}
	
	void Update () {
		Rotate();
		TestPlayer();
		
	}
	
	void LateUpdate()
	{
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU||GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			return;
		}
		MakeMagnit();
	}
	
	public void Rotate()
	{
		if(GlobalOptions.gameState==GameStates.PAUSE_MENU||!flagRotation)
		{
			return;
		}
		singleTransform.Rotate(new Vector3(0,Time.deltaTime*playerScript.GetRealVelocityWithNoDeltaTime()*5,0));
	}
	
	public override void initEnemy()
	{
		parentTransform=singleTransform.parent.parent;
		cameraTransform=playerScript.MainCamera.transform;
	}
	
	public override void ReStart()
	{
		UnMakeEffect();
		//gameObject.SetActiveRecursively(true);	
		singleTransform.rotation=Quaternion.Euler(0, 0, 0);
	}
	
	public void TestPlayer()
	{
		if(effectMade==0&&(!flagRotation||playerScript.GetMagnitFlag()))
		{
			Vector3 razn;
			razn=parentTransform.position-walkingBearTransform.position;
			float gipot=razn.x*razn.x+razn.z*razn.z;
			if(gipot<=rasstChuvstv&&Mathf.Abs (razn.y)<20&&playerScript.GetMagnitFlag())
			{
				MakeEffect();
			}
			if(gipot<=5000&&Mathf.Abs (razn.y)<20)
			{
				flagRotation=true;
			}
		}
	}
	
	private void MakeMagnit()
	{
		//этап 1
		if(effectMade==1)
		{
			float raznx,raznz,razny,vspz;
			float smex=0.125f;
			raznx=-parentTransform.position.x+walkingBearTransform.position.x;
			razny=-parentTransform.position.y+walkingBearTransform.position.y+1;
			raznz=-parentTransform.position.z+walkingBearTransform.position.z;
	
			if(Mathf.Abs(raznx)>smex)
			{
				raznx=Mathf.Sign(raznx)*smex;
			}
			if(Mathf.Abs(razny)>smex)
			{
				razny=Mathf.Sign(razny)*smex;
			}
			
			if(raznz>smex||flagPlusPlayerSpeed)
			{
				flagPlusPlayerSpeed=true;
				vspz=playerScript.GetRealVelocity();
			}
			else
			{
				vspz=0;
			}
				
			if(Mathf.Abs(raznz)>smex)
			{
				
				raznz=Mathf.Sign(raznz)*smex;
			}
			
			raznz+=vspz;
			
			parentTransform.position+=new Vector3(raznx,razny,raznz);	
		}
		
		if(effectMade==2)
		{
			float smex=0.1f;
			
			camx+=smex;
			camy+=smex*2.3f;
			
			parentTransform.position=new Vector3(cameraTransform.position.x+camx,cameraTransform.position.y+camy,walkingBearTransform.position.z+5);
			
			if(parentTransform.position.y>cameraTransform.position.y+0.5)
			{
				GuiLayer.AddMoney(numberOfMoney);
				flagRotation=false;
				effectMade=3;
			}
		}
	}
	
	private void MakeEffect()
	{
		effectMade=1;
	}
	
	
	private void UnMakeEffect()
	{
		effectMade=0;
		flagRotation=false;
		flagPlusPlayerSpeed=false;
	}
}