using UnityEngine;
using System.Collections;

public class ParticleChangeScale : Abstract {
	
	float firstMinScale,firstMaxScale;
	
	bool flagInited=false;

	// Use this for initialization
	void Update () {
		if(!flagInited&&ZoomMap.instance)
		{
			ZoomMap.instance.AddToParticleChangeScaleList(this);
			firstMinScale=particleEmitter.minSize;
			firstMaxScale=particleEmitter.maxSize;
			flagInited=true;
		}
	}
	
	public void ChangeScale(float inscale)
	{
		particleEmitter.minSize=firstMinScale*inscale;
		particleEmitter.maxSize=firstMaxScale*inscale;
	}
}
