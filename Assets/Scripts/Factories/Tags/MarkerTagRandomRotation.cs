using UnityEngine;
using System.Collections;

public class MarkerTagRandomRotation : MarkerTag {

	public override void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		Vector3 angles=new Vector3(0,0,0);
		angles.y=Random.Range(0,360);
		angles.z=Random.Range(0,40)-20;
		angles.x=Random.Range(0,40)-20;
		transform.rotation=Quaternion.Euler(angles);
	}
}

