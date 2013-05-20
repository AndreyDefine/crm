using UnityEngine;
using System.Collections;

public class JumpOverCaws : BaseOneNumberMission {
	
	public override void NotifyJumpOverCaw (int caws)
	{
		base.AddNumber(caws);
	}
}
