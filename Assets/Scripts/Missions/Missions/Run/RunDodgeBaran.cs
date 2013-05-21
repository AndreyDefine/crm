using UnityEngine;
using System.Collections;

public class RunDodgeBaran : BaseOneNumberMission {
	
	public override void NotifyDodgeBaran (int baran)
	{
		base.AddNumber(baran);
	}
}
