using UnityEngine;
using System.Collections;

public class CollectPost : BaseOneNumberMission {
	
	public override void NotifyPostCollected (int post)
	{
		base.AddNumber(post);
	}
}
