using UnityEngine;
using System.Collections;

public abstract class BaseMissionEmmitter : Abstract, IMissionEmmitter, IMissionNotify, IMissionListener
{
	protected ArrayList availableMissionsPrefabs = new ArrayList ();
	protected Hashtable prefabKeyHashTable = new Hashtable ();
	protected ArrayList currentMissions = new ArrayList ();
	protected ArrayList thisLifeFinishedMissions = new ArrayList ();
	public string missionEmmitterName;
	protected int finishedMissionsNumber = 0;
	public Mission[] missions;
	public virtual void LevelBegin(){
		for(int i=0;i<thisLifeFinishedMissions.Count;i++){
			Destroy(((Mission)thisLifeFinishedMissions[i]).gameObject);
		}
		thisLifeFinishedMissions.Clear();
	}
	
	protected Mission GetOneMissionObject ()
	{
		if(availableMissionsPrefabs.Count==0){
			return null;
		}
		//int randomIndex = Random.Range (0, availableMissions.Count);
		Mission missionPrefab = (Mission)availableMissionsPrefabs [0];
		Mission mission = InstantiateMission (missionPrefab);
		//CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
		availableMissionsPrefabs.Remove (missionPrefab);
		return mission;
	}
	
	public abstract ArrayList GetCurrentMissions();
	public abstract int GetCountFinishedMissions();
	protected abstract int GetPriority ();
	protected float timeOutTime = 0;
	
	protected bool IsTimeOut(){
		return Time.time<timeOutTime;
	}
	
	protected string misionCurrentTag {
		get {
			return this.name+"current";
		}
	}
	
	protected string misionFinishedTag{
		get{
			return this.name+"finished";
		}
	}
	
	public int GetCountMissions ()
	{
		return missions.Length;
	}
	
	public ArrayList GetThisLifeFinishedMissions ()
	{
		return thisLifeFinishedMissions;
	}
	
	public float GetProgress ()
	{
		return GetCountFinishedMissions()/(float)GetCountMissions();
	}
	
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
	//IMissionListener
	public void MissionProgressChanged (Mission mission)
	{
		if (!mission.oneLife) {
			CurrentMissionsSerializer.SaveMissionData (mission);
		}
	}
	
	public void MissionFinished (Mission mission)
	{
		finishedMissionsNumber++;
		GlobalOptions.GetPlayerScript().Yahoo();
		SetMissionFinished (mission.GetId ());
		currentMissions.Remove (mission);
		thisLifeFinishedMissions.Add (mission);
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
		CurrentMissionsSerializer.RemoveMissionData (mission);
		timeOutTime = Time.time+Random.Range(5f,10f);
	}
	
	public void MissionActivated (Mission mission)
	{
		CurrentMissionsSerializer.SaveMissionData (mission);
	}
	
	public bool IsMissionFinished (string id)
	{
		return PlayerPrefs.GetInt (misionFinishedTag + id, 0) != 0;
	}
	
	public void SetMissionFinished (string id)
	{
		PlayerPrefs.SetInt (misionFinishedTag + id, 1);
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
	
	public void NotifySlideUnderSomething (int something)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifySlideUnderSomething (something);
		}
	}
	
	public void NotifyPointsAdded (int points)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyPointsAdded (points);
		}
	}
	
	public void NotifyJump (int jump)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyJump (jump);
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
	
	public void NotifyScarecrowDeath (int scarecrowDeath)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyScarecrowDeath (scarecrowDeath);
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
	
	//drop
	public void NotifyPostDropped (int post)
	{
		ArrayList missions = GetActiveCurrentMissions ();
		for (int i=0; i<missions.Count; i++) {
			Mission mission = (Mission)missions [i];
			mission.NotifyPostDropped (post);
		}
	}
	
	//Корректное создание миссии
	//Только так она создастся правильно
	protected Mission InstantiateMission (Mission missionPrefab)
	{
		string id = (string)prefabKeyHashTable [missionPrefab];
		Mission mission = (Instantiate (missionPrefab) as Mission);
		mission.singleTransform.parent = singleTransform;
		mission.AddMissionListener (this);
		mission.SetId (id);
		mission.SetPriority(GetPriority());
		if(GlobalOptions.gameState==GameStates.GAME){
			GlobalOptions.GetGuiLayer ().AddMission (mission);//notify
		}
		return mission;
	}
}
