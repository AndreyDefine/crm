using UnityEngine;
using System.Collections;

public class RunJumpOverDrova : BaseOneNumberMission {
	
	public override void NotifyJumpOverDrova (int drova)
	{
		base.AddNumber(drova);
	}
}
