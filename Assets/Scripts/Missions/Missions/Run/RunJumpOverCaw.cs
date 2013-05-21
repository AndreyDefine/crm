using UnityEngine;
using System.Collections;

public class RunJumpOverCaw : BaseOneNumberMission {
	
	public override void NotifyJumpOverCaw (int caws)
	{
		base.AddNumber(caws);
	}
}
