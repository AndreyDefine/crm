using UnityEngine;
using System.Collections;

public class MissionFlyingIco : Abstract {
	MissionIco missionIco;
	CurrentMissionNotifier currentMissionNotifier;
	public void SetMissionIco(MissionIco missionIco){
		this.missionIco = missionIco;
		missionIco.singleTransform.parent = singleTransform;
		missionIco.singleTransform.localPosition = Vector3.zero;
	}
	
	public MissionIco GetMissionIco(){
		return missionIco;
	}
	
	public void SetCurrentMissionNotifier(CurrentMissionNotifier currentMissionNotifier){
		this.currentMissionNotifier = currentMissionNotifier;
	}
	
	public CurrentMissionNotifier GetCurrentMissionNotifier(){
		return currentMissionNotifier;
	}
	
	public void FlyXYZ(Vector3 inPostion){
		AnimationFactory.FlyXYZ(this, inPostion,0.8f,"flyXYZ", "FlyXYZStopped");
	}
	
	public void FlyXYZStopped(){
		currentMissionNotifier.MissionFlyingIcoFlyStopped();
	}
}
