using UnityEngine;
using System.Collections;

public class RunJump : BaseOneNumberMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
