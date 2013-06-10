using UnityEngine;
using System.Collections;

public class MarkerTagNoRotation : MarkerTag {

	public override void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		Vector3 angles=inRotation.eulerAngles-inRotationParent.eulerAngles;
		angles.y=0;
		singleTransform.rotation=Quaternion.Euler(angles+inRotationParent.eulerAngles);
	}
}
