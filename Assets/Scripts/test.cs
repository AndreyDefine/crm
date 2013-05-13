using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public Color color;

    // Use this for initialization
    void Start() {
        tk2dSprite sprite = gameObject.GetComponent<tk2dSprite>();
        sprite.color = color;
    }
}
