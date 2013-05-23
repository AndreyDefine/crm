using UnityEngine;
using System.Collections;

public class RunSlideUnderRope : BaseOneNumberMission {
	public override void NotifySlideUnderRope (int rope)
	{
		base.AddNumber(rope);
	}
}
