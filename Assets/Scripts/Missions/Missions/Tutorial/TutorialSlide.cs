using UnityEngine;
using System.Collections;

public class TutorialSlide : BaseTutorialMission {
	
	public override void NotifyJump (int jump)
	{
		base.AddNumber(jump);
	}
}
