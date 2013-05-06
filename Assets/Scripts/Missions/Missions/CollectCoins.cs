using UnityEngine;
using System.Collections;

public class CollectCoins : BaseOneNumberMission {
	
	public override void NotifyCoinsCollected (int addCoins)
	{
		base.AddNumber(addCoins);
	}
}
