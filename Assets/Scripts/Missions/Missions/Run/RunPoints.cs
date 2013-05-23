using UnityEngine;
using System.Collections;

public class RunPoints : BaseOneNumberMission {

	public override void NotifyPointsAdded (int points)
	{
		base.AddNumber(points);
	}
}
