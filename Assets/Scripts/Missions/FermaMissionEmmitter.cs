using UnityEngine;
using System.Collections;

public class FermaMissionEmmitter : BaseMissionEmmitter, IMissionListener
{
	
	private int priority=0;
	protected override int GetPriority ()
	{
		return priority;
	}
	
	private string misionEmmittedTag {
		get {
			return this.name+"emmitted";
		}
	}
	
	private string lastMissionEmmitTimeTag {
		get {
			return this.name + "emmit_time";
		}
	}
	
	public long emmitPeriod = 15*60;//in seconds
	public Slot[] slots;
	private ArrayList emmittedMissionsPrefabs = new ArrayList ();//Миссии, которые уже взяты для заводика, они могут выполниться за один забег
	
	private long _lastMissionEmmitTime;

	public long lastMissionEmmitTime {
		get {
			string lastEmmitTime = PlayerPrefs.GetString (lastMissionEmmitTimeTag, "");
			_lastMissionEmmitTime = lastEmmitTime.Equals ("") ? GlobalOptions.GetLongFromDateTime (System.DateTime.MinValue) : long.Parse (lastEmmitTime);
			return _lastMissionEmmitTime;
		}
		set {
			_lastMissionEmmitTime = value;
			PlayerPrefs.SetString (lastMissionEmmitTimeTag, _lastMissionEmmitTime.ToString ());
		}
	}
	
	private ArrayList listeners = new ArrayList();
	
	public void AddFermaMissionEmmitterListener(IFermaMissionEmmitterListener listener){
		listeners.Add(listener);	
	}
	
	public void RemoveFermaMissionEmmitterListener(IFermaMissionEmmitterListener listener){
		listeners.Remove(listener);	
	}
	
	void Start ()
	{
		this.name = this.name.Replace ("(Clone)", "");
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionCurrentTag);
		Hashtable emittedMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionEmmittedTag);
		for (int i=0; i<missions.Length; i++) {
			Mission missionPrefab = missions [i];
			string id = missionPrefab.name;
			missionPrefab.SetId(id);
			if (!IsMissionFinished (id)) {
				prefabKeyHashTable [missionPrefab] = id;
				if (currentMissionsKeyData.ContainsKey (id)) {
					Mission mission = InstantiateMission (missionPrefab);	
					mission.Unserialize (currentMissionsKeyData [id].ToString ());
					currentMissions.Add (mission);
				} else if(emittedMissionsKeyData.ContainsKey (id)){
					emmittedMissionsPrefabs.Add(missionPrefab);
				}else{
					availableMissionsPrefabs.Add (missionPrefab);
				}
			} else {
				finishedMissionsNumber++;
			}
		}
		//инициализируем cлоты (куплены они или нет)
		for (int i=0; i<slots.Length; i++) {
			Slot slot = slots [i];
			slot.Init ();
		}
		EmmitMissions(false);
	}
		
	private void TryToGetNextFromEmmitted(bool force = false){
		if(!force&&(GlobalOptions.gameState!=GameStates.GAME||IsTimeOut())){
			return;
		}
		if(currentMissions.Count==0){//инициализируем текущую
				if(emmittedMissionsPrefabs.Count>0){
					Mission missionPrefab = (Mission)emmittedMissionsPrefabs [0];
					Mission currentMission = InstantiateMission (missionPrefab);
					emmittedMissionsPrefabs.RemoveAt(0);
					currentMissions.Add(currentMission);
					CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);//save
				CurrentMissionsSerializer.SaveCurrentMissions (emmittedMissionsPrefabs, misionEmmittedTag);
			}
		}
	}
	public void EmmitMissions (bool force = false)
	{
		TryToGetNextFromEmmitted();
		
		if(force||canEmmitMissions()){
			Mission currentMission = null;
			TryToGetNextFromEmmitted(true);
			if(currentMissions.Count==0){//инициализируем текущую
				currentMission = GetOneMissionObject();	
				if(currentMission!=null){
					currentMissions.Add(currentMission);
				}
				CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionCurrentTag);
			}
			//emmitted
			int numberOfBoughtSlots = NumberOfBoughtSlots();
			if(emmittedMissionsPrefabs.Count<numberOfBoughtSlots-1){
				for(int i=emmittedMissionsPrefabs.Count;i<numberOfBoughtSlots-1;i++){
					if(availableMissionsPrefabs.Count>0){
						Mission missionPrefab = (Mission)availableMissionsPrefabs [0];
						availableMissionsPrefabs.Remove (missionPrefab);
						emmittedMissionsPrefabs.Add(missionPrefab);
					}
				}
				CurrentMissionsSerializer.SaveCurrentMissions (emmittedMissionsPrefabs, misionEmmittedTag);
			}
			lastMissionEmmitTime = GlobalOptions.GetLongFromDateTime (System.DateTime.UtcNow);
			MissionsUpdated();
		}
		Debug.LogWarning(currentMissions.Count);
	}
	
	protected void MissionsUpdated(){
		for(int i=0;i<listeners.Count;i++){
			((IFermaMissionEmmitterListener)listeners[i]).MissionsUpdated(this);
		}
	}
	
	private int NumberOfBoughtSlots(){
		int number = 0;
		for(int i=0;i<slots.Length;i++){
			if(slots[i].bought){
				number++;
			}
		}
		return number;
	}
	
	protected bool canEmmitMissions ()
	{
		long curTime = GlobalOptions.GetLongFromDateTime (System.DateTime.UtcNow);
		return curTime - lastMissionEmmitTime > emmitPeriod;
	}
	
	void Update ()
	{
		EmmitMissions();
	}
	
	public override ArrayList GetCurrentMissions ()
	{
		return currentMissions;
	}
	
	public ArrayList GetAvailableMissionsPrefabs ()
	{
		return availableMissionsPrefabs;
	}
	
	public void MissionProgressChanged (Mission mission)
	{
		if (!mission.oneLife) {
			CurrentMissionsSerializer.SaveMissionData (mission);
		}
	}
	
	public override int GetCountFinishedMissions ()
	{
		return finishedMissionsNumber;
	}
	
	public ArrayList GetEmmittedMissionsPrefabs(){
		return emmittedMissionsPrefabs;
	}
	
	public long GetNextEmmitInSeconds(){
		long curTime = GlobalOptions.GetLongFromDateTime (System.DateTime.UtcNow);
		return emmitPeriod-curTime+lastMissionEmmitTime;
	}
}
