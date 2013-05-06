using UnityEngine;
using System.Collections;


public class BoostNotifier : BaseNotifier
{
	public Abstract iconPlace;
	
	public void SetBoost(Boost boost){

	}
	
	public override void FlyInStopped(){
		FlyInEnd();
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
	
	public void SetIco(BoostIco boostIco){
		boostIco.singleTransform.parent = iconPlace.singleTransform;
		boostIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
}
