using UnityEngine;
using System.Collections;

public class TutorialSlide : BaseTutorialMission {
	
	public override void NotifySlide (int slide)
	{
		base.AddNumber(slide);
	}
}
