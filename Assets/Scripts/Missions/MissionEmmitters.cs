using UnityEngine;
using System.Collections;

public class MissionEmmitters : Abstract, IMissionEmmitter, IMissionNotify
{
	ArrayList currentMissionEmmitters = new ArrayList();
	
	public TutorialMissionEmmitter tutorialMissionEmmitter;
	
	public BaseMissionEmmitter collectMissionEmmitter;
	public BaseMissionEmmitter runMissionEmmitter;
	
	private FermaMissionEmmitter fermaMissionEmmitter = null;
	
	void Awake(){
		Init();
	}
	
	public ArrayList GetCurrentMissionEmmitters(){
		return currentMissionEmmitters;
	}
	
	public void Init(){
		currentMissionEmmitters.Clear();
		if(PersonInfo.tutorial){
			currentMissionEmmitters.Add(tutorialMissionEmmitter);
		}else{
			if(fermaMissionEmmitter!=null){
				currentMissionEmmitters.Add(fermaMissionEmmitter);
			}
			currentMissionEmmitters.Add(collectMissionEmmitter);
			currentMissionEmmitters.Add(runMissionEmmitter);
		}
	}
	
	public void LevelBegin ()
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).LevelBegin();
		}
	}	
	
	public void RestartActiveMissions ()
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).RestartActiveMissions();
		}
	}
	
	public void NotifyCoinsCollected(int coins){
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyCoinsCollected(coins);
		}
	}
	
	public void NotifyPostCollected (int post)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyPostCollected(post);
		}
	}
	
	public void NotifyVodkaCollected (int vodka)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyVodkaCollected(vodka);
		}
	}
	
	public void NotifyMagnitCollected (int magnit)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyMagnitCollected(magnit);
		}
	}
	
	public void NotifyX2Collected (int x2)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyX2Collected(x2);
		}
	}
	
	public void NotifySenoDeath (int senoDeath)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifySenoDeath(senoDeath);
		}
	}
	
	public void NotifyTraktorDeath (int traktorDeath)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyTraktorDeath(traktorDeath);
		}
	}
	
	public ArrayList GetCurrentMissions(){
		ArrayList currentMissions = new ArrayList();
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			currentMissions.AddRange(((BaseMissionEmmitter)currentMissionEmmitters[i]).GetCurrentMissions());
		}
		return currentMissions;
	}
	
	public ArrayList GetThisLifeFinishedMissions ()
	{
		ArrayList thisLifeFinishedMissions = new ArrayList();
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			thisLifeFinishedMissions.AddRange(((BaseMissionEmmitter)currentMissionEmmitters[i]).GetThisLifeFinishedMissions());
		}
		return thisLifeFinishedMissions;
	}
	
	public void NotifyScarecrowDeath (int scarecrowDeath)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyScarecrowDeath(scarecrowDeath);
		}
	}
	
	//tutorial
	public void NotifySlide (int slide)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifySlide(slide);
		}
	}
	
	public void NotifyRight (int right)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyRight(right);
		}
	}
	
	public void NotifyLeft (int left)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyLeft(left);
		}
	}
	//run
	public void NotifyMetersRunned(int meter){
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyMetersRunned(meter);
		}
	}
	
	public void NotifySlideUnderSomething (int something)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifySlideUnderSomething(something);
		}
	}
	
	public void NotifyJump (int jump)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyJump(jump);
		}
	}
	
	public void NotifyPointsAdded (int points)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyPointsAdded(points);
		}
	}
	
	public void NotifyJumpOverCaw (int caws)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyJumpOverCaw(caws);
		}
	}
	
	public void NotifyJumpOverDrova (int drova)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyJumpOverDrova(drova);
		}
	}
	
	public void NotifySlideUnderRope (int rope)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifySlideUnderRope(rope);
		}
	}
	
	public void NotifyJumpOverHaystack (int haystack)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyJumpOverHaystack(haystack);
		}
	}
	
	public void NotifyDodgeBaran (int baran)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyDodgeBaran(baran);
		}
	}
	
	public void NotifyDodgeTractor (int tractor)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyDodgeTractor(tractor);
		}
	}
	
	//drop
	public void NotifyPostDropped (int post)
	{
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			((BaseMissionEmmitter)currentMissionEmmitters[i]).NotifyPostDropped(post);
		}
	}
	
	public int GetCountFinishedMissions ()
	{
		int finishedMissionsNumber = 0;
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			finishedMissionsNumber+=((BaseMissionEmmitter)currentMissionEmmitters[i]).GetCountFinishedMissions();
		}
		return finishedMissionsNumber;
	}
	
	public int GetCountMissions ()
	{
		int missionsNumber = 0;
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			missionsNumber+=((BaseMissionEmmitter)currentMissionEmmitters[i]).GetCountMissions();
		}
		return missionsNumber;
	}
	
	public float GetProgress ()
	{
		int missionsNumber = 0;
		int finishedMissionsNumber = 0;
		for(int i=0;i<currentMissionEmmitters.Count;i++){
			missionsNumber+=((BaseMissionEmmitter)currentMissionEmmitters[i]).GetCountMissions();
			finishedMissionsNumber+=((BaseMissionEmmitter)currentMissionEmmitters[i]).GetCountFinishedMissions();
		}
		return finishedMissionsNumber/(float)missionsNumber;
	}
	
	public void SetFermaMissionEmmitter(FermaMissionEmmitter missionEmmitter){
		fermaMissionEmmitter = missionEmmitter;
		missionEmmitter.singleTransform.parent = singleTransform;
		Init();
	}
	
	public FermaMissionEmmitter GetFermaMissionEmmitter(){
		return fermaMissionEmmitter;
	}
}
