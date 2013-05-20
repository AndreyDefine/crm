using UnityEngine;
using System.Collections;

public class SenoDeath : BaseOneNumberMission {
	
	public override void NotifySenoDeath (int senoDeath)
	{
		base.AddNumber(senoDeath);
	}
}
