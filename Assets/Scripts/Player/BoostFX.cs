using UnityEngine;
using System.Collections;

public class BoostFX : Abstract {

	public ParticleSystem fx;
	
	public void Play(){
		fx.Play();
	}
	
}