using UnityEngine;
using System.Collections;

public enum BoostStates{NOT_ACTIVE, ACTIVE, FINISHED}

public class Boost : Abstract {
	public BoostIco iconPrefab;
	private BoostStates state = BoostStates.NOT_ACTIVE;
	private ArrayList boostListeners = new ArrayList();
	
	public BoostStates GetState(){
		return state;
	}
	
	public void AddBoostListener(IBoostListener boostListener){
		boostListeners.Add(boostListener);
	}
	
	protected void BoostFinished(){
		this.state = BoostStates.FINISHED;
		for(int i=0;i<boostListeners.Count;i++){
			((IBoostListener)boostListeners[i]).BoostFinished(this);
		}
	}
	
	protected void BoostProgressChanged(){
		for(int i=0;i<boostListeners.Count;i++){
			((IBoostListener)boostListeners[i]).BoostProgressChanged(this);
		}
	}
	
	/// <summary>
	/// Get the progress.
	/// </summary>
	/// <returns>
	/// The progress 0-1
	/// </returns>
	public virtual float GetProgress(){
		return 0;
	}
	
	public virtual void SetActive(){
		state = BoostStates.ACTIVE;	
	}
}
