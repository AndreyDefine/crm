using UnityEngine;
using System.Collections;

public class TutorialRight : BaseTutorialMission {
	
	public override void NotifyRight (int right)
	{
		base.AddNumber(right);
	}
}
