using UnityEngine;
using System.Collections;

public class FactoryBuildingWihtSpriteAnimation : FactoryBuilding {	
	private tk2dAnimatedSprite _anim = null;

	public tk2dAnimatedSprite anim{
		get{
			if(_anim==null)
			{
				_anim=GetComponent<tk2dAnimatedSprite>();
			}
			return _anim;
		}
		protected set{
			_anim=value;
		}
	}
	
	public void PlayAnimation()
	{
		anim.Play("Action1");
	}

	public override void SetActive(bool a){	
		if(a){
			if(flagAddAnimation)
			{
				anim.Play("Action1");
			}
		}else{
			if(flagAddAnimation)
			{
				anim.Stop();
			}
		}
	}
}
