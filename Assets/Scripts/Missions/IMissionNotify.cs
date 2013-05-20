using UnityEngine;
using System.Collections;

public interface IMissionNotify{
	//run
	void NotifyMetersRunned (int meters);
	void NotifyJumpOverCaw (int caws);
	//collect
	void NotifyCoinsCollected(int coins);
	void NotifyPostCollected(int post);
	void NotifyVodkaCollected(int vodka);
	void NotifyMagnitCollected(int magnit);
	void NotifyX2Collected(int x2);
	void NotifySenoDeath(int senoDeath);
	void NotifyTraktorDeath(int traktorDeath);
}
