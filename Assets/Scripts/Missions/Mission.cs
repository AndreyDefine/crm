using UnityEngine;
using System.Collections;

public enum MissionStates{NOT_ACTIVE, ACTIVE, FINISHED}

public class Mission : Abstract {
	
	private MissionStates state = MissionStates.NOT_ACTIVE;
	public MissionIco iconPrefab;
	public string missionName="";
	public string description="";
	public bool oneLife;
	
	private ArrayList missionListeners = new ArrayList();
	private string id;
	
	public void SetActive(){
		state = MissionStates.ACTIVE;
	}
	
	public MissionStates GetState(){
		return state;
	}
	
	public void SetId(string id){
		this.id = id;
	}
	
	public string GetId(){
		return id;
	}
	
	public void AddMissionListener(IMissionListener missionListener){
		missionListeners.Add(missionListener);	
	}
	
	protected void MissionFinished(){
		this.state = MissionStates.FINISHED;
		for(int i=0;i<missionListeners.Count;i++){
			((IMissionListener)missionListeners[i]).MissionFinished(this);
		}
	}
	
	protected void MissionProgressChanged(){
		for(int i=0;i<missionListeners.Count;i++){
			((IMissionListener)missionListeners[i]).MissionProgressChanged(this);
		}
	}
	
	public virtual string GetProgressRepresentation(){
		return "0";
	}
	
	public virtual string GetLongProgressRepresentation(){
		return "0/1";
	}
	/// <summary>
	/// Gets the progress.
	/// </summary>
	/// <returns>
	/// 0-1
	/// </returns>
	public virtual float GetProgress(){
		return 0;
	}
	
	public virtual void NotifyCoinsCollected(int coins){
	}
	
	public virtual void NotifyMetersRunned (int meters){
	}
	
	public virtual string Serialize(){
		return " ";
	}
	
	public virtual void Unserialize(string data){
		//do nothing
	}
	
}
