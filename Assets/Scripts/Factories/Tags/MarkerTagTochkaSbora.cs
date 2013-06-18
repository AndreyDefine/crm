using UnityEngine;
using System.Collections;

public class MarkerTagTochkaSbora : MarkerTag {
	
	public GameObject Yashik;

	public override void ApplyRotation(Quaternion inRotation,Quaternion inRotationParent)
	{
		//No Rotation
		Vector3 angles=inRotation.eulerAngles-inRotationParent.eulerAngles;
		angles.y=0;
		singleTransform.rotation=Quaternion.Euler(angles+inRotationParent.eulerAngles);
		
		//place yashik
		Transform curTransformYashik=Yashik.transform;
		if(singleTransform.localPosition.x>2)
		{
			curTransformYashik.localPosition=new Vector3(1f,curTransformYashik.localPosition.y,curTransformYashik.localPosition.z);
		}
		else
		{
			curTransformYashik.localPosition=new Vector3(-1f,curTransformYashik.localPosition.y,curTransformYashik.localPosition.z);
		}
	}
}

