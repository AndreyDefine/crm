using UnityEngine;
using System.Collections;

public class RunOverHaystack : BaseOneNumberMission {
	
	public override void NotifyJumpOverHaystack (int haystack)
	{
		base.AddNumber(haystack);
	}
}
