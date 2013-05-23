using UnityEngine;
using System.Collections;

public class RunSlideUnderSomething : BaseOneNumberMission {
	public override void NotifySlideUnderSomething (int something)
	{
		base.AddNumber(something);
	}
}
