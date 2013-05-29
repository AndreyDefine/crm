using UnityEngine;
using System.Collections;

public class BaseFermaMission : BaseOneNumberMission {
	public override void NotifyPostDropped (int post)
	{
		base.AddNumber(post);
	}
}
