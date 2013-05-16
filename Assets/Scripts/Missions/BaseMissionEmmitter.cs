using UnityEngine;
using System.Collections;

public abstract class BaseMissionEmmitter : Abstract, IMissionEmmitter
{
	public abstract void LevelBegin();
	public abstract void NotifyCoinsCollected(int coins);
	public abstract void NotifyMetersRunned(int meter);
	public abstract ArrayList GetCurrentMissions();
	public abstract ArrayList GetThisLifeFinishedMissions();
	public abstract int GetCountFinishedMissions();
}
