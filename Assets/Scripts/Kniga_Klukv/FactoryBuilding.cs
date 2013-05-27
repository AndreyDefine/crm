using UnityEngine;
using System.Collections;

public class FactoryBuilding : Abstract {
	
	bool animationAdded = false;

	public void SetActive(bool a){
		if(!animationAdded){
			AnimationFactory.AttentionXThenYLoop(this,2f,1.05f,"AttentionXThenYLoop", "AttentionXThenYLoopStop",false);
			animationAdded = true;
		}
		if(a){
			animation.Play("AttentionXThenYLoop");	
		}else{
			animation.Stop();	
		}
	}
	
	public void AttentionXThenYLoopStop(){
	}
}
