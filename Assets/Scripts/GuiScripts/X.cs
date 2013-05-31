using UnityEngine;
using System.Collections;

public class X : Abstract {
	public tk2dTextMesh leftOf;
	public CrmFont x2Value;
	public void Position(){
		singleTransform.localPosition = new Vector3(-0.19f-leftOf.text.Length*0.08f, singleTransform.localPosition.y, singleTransform.localPosition.z);//govnocod
	}
	
	public void SetValue(int x){
		x2Value.text = string.Format("x{0}", x);
	}
}
