using UnityEngine;
using System.Collections;

public class TutorialLeft : BaseTutorialMission {
	
	public override void NotifyLeft (int left)
	{
		base.AddNumber(left);
	}
}
