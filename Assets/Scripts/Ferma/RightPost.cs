using UnityEngine;
using System.Collections;

public class RightPost : Abstract {
	public GameObject[] Golubs;
	public GameObject EmmisionPoint;
	
	protected void Start()
	{
		GetComponent<tk2dAnimatedSprite>().animationEventDelegate=OnTrigger;
	}

	private void OnTrigger(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int i) {
        switch (frame.eventInfo) {
        case "trigger1":
			int RandIndex=Random.Range(0,Golubs.Length);
			GameObject Golub;
			Golub=Instantiate(Golubs[RandIndex]) as GameObject;
			Golub.transform.position=EmmisionPoint.transform.position;
			Golub.transform.localScale=ZoomMap.instance.gameObject.transform.localScale;
			Golub.transform.parent=ZoomMap.instance.gameObject.transform;
            break;
        }
    }
}
