using UnityEngine;
using System.Collections;

public abstract class BaseMissionEmmitter : Abstract, IMissionEmmitter, IMissionNotify
{
	public abstract void LevelBegin();
	public abstract ArrayList GetCurrentMissions();
	public abstract ArrayList GetThisLifeFinishedMissions();
	public abstract int GetCountFinishedMissions();
	
	public void RestartActiveMissions ()
	{
		ArrayList activeMissions = GetActiveCurrentMissions ();
		for (int i=0; i<activeMissions.Count; i++) {
			((Mission)activeMissions [i]).Restart();
		}
	}
	
	private ArrayList GetActiveCurrentMissions ()
	{
		ArrayList currentMissions = GetCurrentMissions();
		ArrayList activeMissions = new ArrayList ();
		for (int i=0; i<currentMissions.Count; i++) {
			if (((Mission)currentMissions [i]).GetState () == MissionStates.ACTIVE) {
				activeMissions.Add (currentMissions [i]);
			}
		}
		return activeMissions;	
	}
	//run
	public void NotifyMetersRunned (int meter)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyMetersRunned (meter);
		}
	}
	
	public void NotifyJumpOverCaw (int caws)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyJumpOverCaw (caws);
		}
	}
	
	public void NotifyJumpOverDrova (int drova)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyJumpOverDrova (drova);
		}
	}
	
	public void NotifySlideUnderRope (int rope)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifySlideUnderRope (rope);
		}
	}
	
	public void NotifyJumpOverHaystack (int haystack)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyJumpOverHaystack (haystack);
		}
	}
	
	public void NotifyDodgeBaran (int baran)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyDodgeBaran (baran);
		}
	}
	
	public void NotifyDodgeTractor (int tractor)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyDodgeTractor (tractor);
		}
	}
	
	//collect
	public void NotifyPostCollected (int post)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyPostCollected (post);
		}
	}
	
	public void NotifyVodkaCollected (int vodka)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyVodkaCollected (vodka);
		}
	}
	
	public void NotifyCoinsCollected (int coins)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyCoinsCollected (coins);
		}
	}
	
	public void NotifyMagnitCollected (int magnit)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyMagnitCollected (magnit);
		}
	}
	
	public void NotifyX2Collected (int x2)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyX2Collected (x2);
		}
	}
	
	public void NotifySenoDeath (int senoDeath)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifySenoDeath (senoDeath);
		}
	}
	
	public void NotifyTraktorDeath (int traktorDeath)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyTraktorDeath (traktorDeath);
		}
	}
}
