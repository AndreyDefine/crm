using UnityEngine;
using System.Collections;

public class GolubLeft : Abstract {
	public GameObject LeftPost;
	
	void DestroyGameObjectOnEvent()
	{
		Destroy(gameObject);
	}
	
	void ShowLeftPost()
	{
		LeftPost.GetComponent<tk2dAnimatedSprite>().Play("Action1");
	}
}
