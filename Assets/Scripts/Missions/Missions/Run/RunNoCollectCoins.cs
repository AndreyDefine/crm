using UnityEngine;
using System.Collections;

public class RunNoCollectCoins : BaseOneNumberMission {
	
	public override void NotifyMetersRunned (int addMeters)
	{
		base.AddNumber(addMeters);
	}
	
	public override void NotifyCoinsCollected (int coins)
	{
		base.ResetNumber();
	}
}
