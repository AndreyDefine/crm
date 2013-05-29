using UnityEngine;
using System.Collections;

public class FactoryBuilding : Abstract {
	
	public bool flagAddAnimation=true;
	
	bool animationAdded = false;

	public void SetActive(bool a){	
		if(!animationAdded&&flagAddAnimation){
			AnimationFactory.AttentionXThenYLoop(this,2f,1.05f,"AttentionXThenYLoop", "AttentionXThenYLoopStop",false);
			animationAdded = true;
		}
		if(a){
			if(flagAddAnimation)
			{
				animation.Play("AttentionXThenYLoop");	
			}
		}else{
			if(flagAddAnimation)
			{
				animation.Stop();
			}
		}
	}
	
	public void AttentionXThenYLoopStop(){
	}
}
