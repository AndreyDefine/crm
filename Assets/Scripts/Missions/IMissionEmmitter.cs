using UnityEngine;
using System.Collections;

public interface IMissionEmmitter{
	void RestartActiveMissions();
	void LevelBegin();
	ArrayList GetCurrentMissions();
	ArrayList GetThisLifeFinishedMissions();
	int GetCountFinishedMissions();
	int GetCountMissions();
	float GetProgress();
}
