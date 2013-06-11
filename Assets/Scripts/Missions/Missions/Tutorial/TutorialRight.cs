using UnityEngine;
using System.Collections;

public class TutorialRight : BaseTutorialMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
