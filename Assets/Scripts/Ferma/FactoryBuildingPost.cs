using UnityEngine;
using System.Collections;

public class FactoryBuildingPost : FactoryBuildingWihtSpriteAnimation {
	
	public GameObject LeftPost;
	
	public override void SetActive(bool a){	
		if(a){
			if(flagAddAnimation)
			{
				LeftPost.GetComponent<tk2dAnimatedSprite>().Play("Action1");
			}
		}else{
			if(flagAddAnimation)
			{
				LeftPost.GetComponent<tk2dAnimatedSprite>().Stop();
			}
		}
	}

}
