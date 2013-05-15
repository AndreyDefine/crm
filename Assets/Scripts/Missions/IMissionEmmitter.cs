using UnityEngine;
using System.Collections;

public interface IMissionEmmitter{
	void LevelBegin();
	void NotifyCoinsCollected(int coins);
	void NotifyMetersRunned(int meter);
	ArrayList GetCurrentMissions();
	ArrayList GetThisLifeFinishedMissions();
}
