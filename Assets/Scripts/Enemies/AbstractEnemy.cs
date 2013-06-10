using UnityEngine;
using System.Collections;


public class AbstractEnemy : AbstractBaseEnemy {	
	public AudioClip playOnHit;
	public TerrainTag parentTerrainTag=null;
	
	protected GuiLayerInitializer GuiLayer;
	protected Transform playertransform;
	protected Player playerScript;
	protected Transform characterTransform;
	protected Transform walkingBearTransform;
	
	void Start(){
		GuiLayer=GlobalOptions.GetGuiLayer();	
		playerScript=GlobalOptions.GetPlayerScript();
		playertransform=GlobalOptions.GetPlayerScript().singleTransform;
		characterTransform=playerScript.Character.transform;
		walkingBearTransform=playerScript.GetWalkingBear();
		initEnemy();
	}
	
	public virtual void initEnemy()
	{
		//do nothing
	}
	
	public virtual void PutToInactiveList()
	{
		Transform curtransform=singleTransform;
		
		
		//Ищем террейн
		while(curtransform.parent){
			curtransform=curtransform.parent;
		}
		parentTerrainTag=curtransform.gameObject.GetComponent("TerrainTag") as TerrainTag;
		
		if(parentTerrainTag){
			parentTerrainTag.PutToInactiveList(this);
		}
	}
	
	public virtual void MakeInactive()
	{
		gameObject.SetActive(false);
		
		PutToInactiveList();
	}
	
	public virtual void MakeInactiveParent()
	{
		singleTransform.parent.gameObject.SetActive(false);
		PutToInactiveList();
	}
	
	public virtual void MakeAction()
	{
		//do nothing;	
	}
	
	public virtual void ReStart()
	{
		//do nothing;	
	}
	
	protected void PlayClipSound()
	{
		if(playOnHit)
		{
			AudioSource.PlayClipAtPoint(playOnHit, singleTransform.position);
		}
	}
}