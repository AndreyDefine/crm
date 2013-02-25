using UnityEngine;
using System.Collections;
using  System.Globalization;

public class AbstractTag : Abstract{	
	private AbstractElementFactory abstractElementFactory;
	
	private AbstractEnemy enemyScript;
	
	public void addFactory(AbstractElementFactory inabstractElementFactory){
		abstractElementFactory=inabstractElementFactory;
		enemyScript=GetComponentInChildren<AbstractEnemy>();
	}
	
	public virtual void DeleteFromUsed(){
		abstractElementFactory.DeleteCurrent(gameObject);
	}
	
	public virtual void ReStart()
	{
		if(enemyScript)
		{
			enemyScript.ReStart();
		}
		else 
		{
			Debug.Log (singleTransform.GetChild(0).gameObject.name);
			Debug.Log ("AbstractEnemy Not Found");
		}
		
	}
}