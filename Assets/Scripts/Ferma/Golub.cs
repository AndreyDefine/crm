using UnityEngine;
using System.Collections;

public class Golub : Abstract {
	void DestroyGameObjectOnEvent()
	{
		Destroy(gameObject);
	}
}
