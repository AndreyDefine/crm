using UnityEngine;
using System.Collections;

public class MarkerTagNoRotation : MarkerTag {

	public override void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		Vector3 angles=inRotation.eulerAngles-inRotationParent.eulerAngles;
		angles.y=angles.y>20?20:angles.y;
		angles.y=angles.y<-20?-20:angles.y;
		transform.rotation=Quaternion.Euler(angles+inRotationParent.eulerAngles);
	}
}
