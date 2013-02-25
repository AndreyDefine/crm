using UnityEngine;
using System.Collections;

public class Feets : Abstract {
	
	public float oneShag;
	public GameObject FootLeft;
	public GameObject FootRight;
	public Vector3 leftFootSmex;
	public Vector3 rightFootSmex;
	bool whatFoot;
	private float localTime;
	
	

	void Start () {
		whatFoot=true;
		localTime=0;
	}
	
	private void Update () {
		if(GlobalOptions.gameState==GameStates.GAME&&GlobalOptions.gameState==GameStates.GAME&&
			(GlobalOptions.playerStates!=PlayerStates.LEFT&&
			GlobalOptions.playerStates!=PlayerStates.RIGHT&&
			GlobalOptions.playerStates!=PlayerStates.JUMP)){
			
			if(Time.time-localTime>oneShag/GlobalOptions.playerVelocity){
				Vector3 pos=singleTransform.position;
				addOneFoot(pos, whatFoot);
				localTime=Time.time;				
				//change foot
				whatFoot=!whatFoot;
			}
		}
	}
	
	private void  addOneFoot(Vector3 inpos, bool isLeft){
		GameObject newFoot;
		Vector3 smex;
		if(isLeft){
			newFoot	= Instantiate(FootLeft) as GameObject;
			smex=leftFootSmex;
		}else{
			smex=rightFootSmex;
			newFoot	= Instantiate(FootRight) as GameObject;
		}
		
		//smex=GlobalOptions.NormalizeVector3Smex(smex,GlobalOptions.whereToGo);
		inpos+=smex;
		newFoot.transform.position=inpos;
		newFoot.transform.rotation=transform.rotation;
		//GlobalOptions.rotateTransformForWhere(newFoot.transform,GlobalOptions.whereToGo);
		//Vector3 Rotation newFoot.transform.rotation;
		//newFoot.transform.Rotate(new Vector3(0,90,0));
		
		//newFoot.transform.Translate(inpos);
		Destroy(newFoot,7/GlobalOptions.playerVelocity);
	}
}
