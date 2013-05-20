using UnityEngine;
using System.Collections;

public class CollectX2 : BaseOneNumberMission {
	
	public override void NotifyX2Collected (int x2)
	{
		base.AddNumber(x2);
	}
}
