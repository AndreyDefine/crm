using UnityEngine;
using System.Collections;

/// <summary>
/// Provides some basic useful animations.
/// </summary>
public static class ParticleFactory {
	public static void loadBoostCollected(GameObject obj){
		GameObject particle = GameObject.Instantiate(Resources.Load("Cartoon FX/CFX Prefabs (Mobile)/Misc/CFXM_Flash")) as GameObject;
		particle.transform.parent = obj.transform;
		particle.transform.position = obj.transform.position;
	}
}
