using UnityEngine;
using System.Collections;

public class MarkerTag : Abstract {

	public virtual void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		singleTransform.rotation=inRotation;
	}
}
