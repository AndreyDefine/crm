using UnityEngine;
using System.Collections;

public class Factories : Abstract {
	
	private Hashtable factoriesHashtable = new Hashtable();
	
	public Factory[] factories;
	
	void Start(){
		for(int i=0;i<factories.Length;i++){
			factoriesHashtable.Add(factories[i].name,factories[i]);		
		}
		
		Factory factory = factoriesHashtable[PersonInfo.lastFactoryName] as Factory;
		if(factory!=null){
			singleTransform.position = new Vector3(-factory.singleTransform.localPosition.x,-factory.singleTransform.localPosition.y,singleTransform.position.z);
		}
	}
}
