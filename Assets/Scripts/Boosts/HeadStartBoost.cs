using UnityEngine;
using System.Collections;

public class HeadStartBoost : BaseTimerBoost {
	public static float longTime = 5f;
	public override float GetMaxTime ()
	{
		return longTime;
	}

}
