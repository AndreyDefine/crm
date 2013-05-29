using UnityEngine;
using System.Collections;

public class FermaMissionEmmitter : BaseMissionEmmitter, IMissionListener
{
	private string misionFinishedTag{
		get{
			return this.name;
		}
	}
	
	private string misionBoughtTag{
		get{
			return this.name+"_bought_";
		}
	}
	
	private string lastMissionEmmitTimeTag{
		get{
			return this.name+"_emmitte";
		}
	}
	private int missionMaxCount = 1;
	public Mission[] missions;
	private ArrayList availableMissionsPrefabs = new ArrayList ();
	private ArrayList availableNotBoughtMissionsPrefabs = new ArrayList ();
	Hashtable prefabKeyHashTable = new Hashtable ();
	private ArrayList currentMissions = new ArrayList ();
	private ArrayList thisLifeFinishedMissions = new ArrayList ();
	private int finishedMissionsNumber = 0;
	float curTime;
	
	private long _lastMissionEmmitTime;
    public long lastMissionEmmitTime {
        get {
			string lastEmmitTime = PlayerPrefs.GetString(lastMissionEmmitTimeTag,"");
			_lastMissionEmmitTime = lastEmmitTime.Equals("")?GlobalOptions.GetLongFromDateTime(System.DateTime.MinValue):long.Parse(lastEmmitTime);
            return _lastMissionEmmitTime;
        }
        set {
            _lastMissionEmmitTime = value;
			PlayerPrefs.SetString(lastMissionEmmitTimeTag,_lastMissionEmmitTime.ToString());
        }
    }
	
	void Start ()
	{
		//PlayerPrefs.DeleteAll();//TODO: delete this
		curTime = Time.time;
		
		//Ищем только миссии, которые еще не выполнялись и текущие тоже ищем
		Hashtable currentMissionsKeyData = CurrentMissionsSerializer.GetCurrentMissionsKeyData (misionFinishedTag + "data_");
		for (int i=0; i<missions.Length; i++) {
			Mission missionPrefab = missions [i];
			string id = missionPrefab.name;
			if (!IsMissionFinished (id)) {
				prefabKeyHashTable [missionPrefab] = id;
				if (currentMissionsKeyData.ContainsKey (id)) {
					Mission mission = InstantiateMission (missionPrefab);	
					mission.Unserialize (currentMissionsKeyData [id].ToString ());
					currentMissions.Add (mission);
					mission.SetActive ();
				} else {
					if(IsMissionBought(id)){
						availableMissionsPrefabs.Add (missionPrefab);
					}else{
						availableNotBoughtMissionsPrefabs.Add (missionPrefab);
					}
				}
			}else{
				finishedMissionsNumber++;
			}
		}
	}
	
	public override int GetCountMissions ()
	{
		return missions.Length;
	}
	
	public bool IsMissionFinished (string id)
	{
		return PlayerPrefs.GetInt (misionFinishedTag + id, 0) != 0;
	}
	
	public void SetMissionFinished (string id)
	{
		PlayerPrefs.SetInt (misionFinishedTag + id, 1);
	}
	
	public bool IsMissionBought (string id)
	{
		return PlayerPrefs.GetInt (misionBoughtTag + id, 0) != 0;
	}
	
	public void SetMissionBought (string id)
	{
		PlayerPrefs.SetInt (misionBoughtTag + id, 1);
	}
	
	public override void LevelBegin ()
	{
		/*for(int i=0;i<thisLifeFinishedMissions.Count;i++){
			Destroy(((Mission)thisLifeFinishedMissions[i]).gameObject);
		}
		thisLifeFinishedMissions.Clear();
		if (GetCurrentMissions ().Count == 0) {
			canEmmitMission = true;
		}*/
	}
		
	void Update ()
	{
		/*if (GlobalOptions.gameState != GameStates.GAME) {//TODO: check
			curTime = Time.time;	
			return;
		}
		
		if (Time.time - curTime > missionRespaunTime) {
			if (canEmmitMission && currentMissions.Count < missionMaxCount && availableMissionsPrefabs.Count != 0) {
				canEmmitMission = false;
				AddOneMissionObject ();
			}
			curTime = Time.time;
		}*/
	}
	
	private void AddOneMissionObject ()
	{
		//int randomIndex = Random.Range (0, availableMissions.Count);
		int randomIndex = 0;
		Mission missionPrefab = (Mission)availableMissionsPrefabs [randomIndex];
		Mission mission = InstantiateMission (missionPrefab);
		currentMissions.Add (mission);
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionFinishedTag + "data_");
		GlobalOptions.GetGuiLayer ().AddMission (mission);
		availableMissionsPrefabs.Remove (missionPrefab);
	}
	
	private Mission InstantiateMission (Mission missionPrefab)
	{
		string id = (string)prefabKeyHashTable [missionPrefab];
		Mission mission = (Instantiate (missionPrefab) as Mission);
		mission.AddMissionListener (this);
		mission.SetId (id);
		return mission;
	}
	
	public override ArrayList GetCurrentMissions ()
	{
		return currentMissions;
	}
	
	public ArrayList GetAvailableNotBoughtMissionsPrefabs(){
		return availableNotBoughtMissionsPrefabs;
	}
	
	public ArrayList GetAvailableMissionsPrefabs(){
		return availableMissionsPrefabs;
	}
	
	public void BuyMission(Mission missionPrefab){
		PersonInfo.AddCoins(-missionPrefab.coinPrice);
		PersonInfo.AddGold(-missionPrefab.goldPrice);
		availableMissionsPrefabs.Add(missionPrefab);
		SetMissionBought(missionPrefab.name);
		availableNotBoughtMissionsPrefabs.Remove(missionPrefab);
		lastMissionEmmitTime = GlobalOptions.GetLongFromDateTime(System.DateTime.UtcNow);
	}
	
	public override ArrayList GetThisLifeFinishedMissions ()
	{
		return thisLifeFinishedMissions;
	}
	
	public void MissionFinished (Mission mission)
	{
		finishedMissionsNumber++;
		SetMissionFinished (mission.GetId ());
		currentMissions.Remove (mission);
		thisLifeFinishedMissions.Add(mission);
		CurrentMissionsSerializer.SaveCurrentMissions (currentMissions, misionFinishedTag + "data_");
		CurrentMissionsSerializer.RemoveMissionData (mission);
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
}
