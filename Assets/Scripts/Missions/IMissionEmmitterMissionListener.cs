using UnityEngine;
using System.Collections;

public interface IMissionEmmitterListener{
	void HasMissions(BaseMissionEmmitter missionEmmitter);
	void NoMissions(BaseMissionEmmitter missionEmmitter);
}
