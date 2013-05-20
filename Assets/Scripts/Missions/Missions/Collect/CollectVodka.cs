using UnityEngine;
using System.Collections;

public class CollectVodka : BaseOneNumberMission {
	
	public override void NotifyVodkaCollected (int vodka)
	{
		base.AddNumber(vodka);
	}
}
