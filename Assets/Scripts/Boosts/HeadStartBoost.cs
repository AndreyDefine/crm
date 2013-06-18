using UnityEngine;
using System.Collections;

public class HeadStartBoost : BaseTimerBoost {
	public float time = 5f;
	public override float GetMaxTime ()
	{
		return time;
	}

}
