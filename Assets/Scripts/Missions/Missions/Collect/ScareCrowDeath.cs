using UnityEngine;
using System.Collections;

public class ScareCrowDeath : BaseOneNumberMission {
	public override void NotifyScarecrowDeath (int scarecrowDeath)
	{
		base.AddNumber(scarecrowDeath);
	}
}
