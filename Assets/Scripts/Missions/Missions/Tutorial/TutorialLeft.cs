using UnityEngine;
using System.Collections;

public class TutorialLeft : BaseTutorialMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
