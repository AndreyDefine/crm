using UnityEngine;
using System.Collections;

public class CollectMagnit : BaseOneNumberMission {
	
	public override void NotifyMagnitCollected (int magnit)
	{
		base.AddNumber(magnit);
	}
}
