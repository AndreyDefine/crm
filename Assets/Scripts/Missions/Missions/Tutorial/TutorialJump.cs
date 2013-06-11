using UnityEngine;
using System.Collections;

public class TutorialJump : BaseTutorialMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
