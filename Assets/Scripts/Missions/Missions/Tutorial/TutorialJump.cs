using UnityEngine;
using System.Collections;

public class TutorialJump : BaseOneNumberMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
