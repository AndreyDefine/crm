using UnityEngine;
using System.Collections;

public class TraktorDeath : BaseOneNumberMission {
	
	public override void NotifyTraktorDeath (int senoDeath)
	{
		base.AddNumber(senoDeath);
	}
}
