using UnityEngine;
using System.Collections;

public class SlideUnderRope : BaseOneNumberMission {
	public override void NotifySlideUnderRope (int rope)
	{
		base.AddNumber(rope);
	}
}
