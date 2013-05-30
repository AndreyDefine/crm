using UnityEngine;
using System.Collections;

public interface IMissionListener{
	void MissionProgressChanged(Mission mission);
	void MissionFinished(Mission mission);
	void MissionActivated(Mission mission);
}
