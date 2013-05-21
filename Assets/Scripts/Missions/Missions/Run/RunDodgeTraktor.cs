using UnityEngine;
using System.Collections;

public class RunDodgeTraktor : BaseOneNumberMission {
	
	public override void NotifyDodgeTractor (int traktor)
	{
		base.AddNumber(traktor);
	}
}
