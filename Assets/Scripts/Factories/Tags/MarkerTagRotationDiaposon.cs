using UnityEngine;
using System.Collections;

public class MarkerTagRotationDiaposon : MarkerTag {
	
	public float minYRotation=-20;
	public float maxYRotation= 20;

	public override void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		Vector3 angles=inRotation.eulerAngles-inRotationParent.eulerAngles;
		angles.y=angles.y>minYRotation?minYRotation:angles.y;
		angles.y=angles.y<-minYRotation?-minYRotation:angles.y;
		transform.rotation=Quaternion.Euler(angles+inRotationParent.eulerAngles);
	}
}
