using UnityEngine;
using System.Collections;

public class CompleteFX : Abstract {
	public Abstract complete;

	public void Play(){
		complete.singleTransform.localScale = new Vector3(0f,0f,0f);
		AnimationFactory.ScaleInXYZ(complete, new Vector3(1f,1f,1f),0.4f, "completeScaleIn");
	}
}
