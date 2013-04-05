using UnityEngine;
using System.Collections;

public class MarkerTag : MonoBehaviour {

	public virtual void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		transform.rotation=inRotation;
	}
}
