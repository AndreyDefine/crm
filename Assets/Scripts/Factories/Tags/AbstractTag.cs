using UnityEngine;
using System.Collections;
using  System.Globalization;

public class AbstractTag : Abstract{	
	protected AbstractElementFactory abstractElementFactory;
	
	private AbstractEnemy[] enemyScripts;
	
	public void addFactory(AbstractElementFactory inabstractElementFactory){
		abstractElementFactory=inabstractElementFactory;
		enemyScripts = GetComponentsInChildren<AbstractEnemy>();
	}
	
	public virtual void DeleteFromUsed(){
		abstractElementFactory.DeleteCurrent(gameObject);
	}
	
	public virtual void ReStart()
	{
		if(enemyScripts.Length>0)
		{
			for(int i=0;i<enemyScripts.Length;i++)
			{
				enemyScripts[i].ReStart();
			}
		}
		else 
		{
			//Debug.Log (singleTransform.GetChild(0).gameObject.name);
			//Debug.Log ("AbstractEnemy Not Found");
		}
		
	}
}