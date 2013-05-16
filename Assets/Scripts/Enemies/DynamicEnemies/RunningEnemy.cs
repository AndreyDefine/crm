using UnityEngine;
using System.Collections;


public class RunningEnemy : AbstractEnemy {	
	public override void ReStart()
	{
		Animation animationScriptRun=GetComponentInChildren<Animation>();
		if(animationScriptRun)
		{
			animationScriptRun.Stop();
			
			Debug.Log ("RunningEnemy");
		}
	}
}