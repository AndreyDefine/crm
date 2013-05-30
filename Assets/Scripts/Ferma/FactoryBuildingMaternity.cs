using UnityEngine;
using System.Collections;

public class FactoryBuildingMaternity : FactoryBuildingWihtSpriteAnimation {
	
	public GameObject SmallBear;
	
	protected void Start()
	{
		anim.animationEventDelegate=OnTrigger;
	}

	private void OnTrigger(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int i) {
        switch (frame.eventInfo) {
        case "trigger1":
            SmallBear.animation.Play();
            break;
        }
    }

}
