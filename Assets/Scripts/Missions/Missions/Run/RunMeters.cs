using UnityEngine;
using System.Collections;

public class RunMeters : BaseOneNumberMission {
	
	public override void NotifyMetersRunned (int addMeters)
	{
		base.AddNumber(addMeters);
	}
}
