using UnityEngine;
using System.Collections;

public interface IMissionNotify{
	//tutorial
	void NotifySlide (int slide);
	void NotifyRight (int right);
	void NotifyLeft (int left);
	//run
	void NotifyMetersRunned (int meters);
	void NotifyJump (int jump);
	void NotifyPointsAdded (int points);
	void NotifyJumpOverCaw (int caws);
	void NotifyJumpOverDrova (int drova);
	void NotifyJumpOverHaystack (int haystack);
	void NotifySlideUnderRope (int rope);
	void NotifySlideUnderSomething (int something);
	void NotifyDodgeBaran (int baran);
	void NotifyDodgeTractor (int tractor);
	//collect
	void NotifyCoinsCollected(int coins);
	void NotifyPostCollected(int post);
	void NotifyVodkaCollected(int vodka);
	void NotifyMagnitCollected(int magnit);
	void NotifyX2Collected(int x2);
	void NotifySenoDeath(int senoDeath);
	void NotifyTraktorDeath(int traktorDeath);
	void NotifyScarecrowDeath(int scarecrowDeath);
	//posilka
	void NotifyPostDropped(int post);
}
