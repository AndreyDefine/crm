using UnityEngine;
using System.Collections;

public enum MissionStates{NOT_ACTIVE, ACTIVE, FINISHED}

public class Mission : Abstract, IMissionNotify {
	
	private int priority = 0;
	public int GetPriority(){
		return priority;
	}
	public void SetPriority(int priority){
		this.priority = priority;
	}
	
	protected MissionStates state = MissionStates.NOT_ACTIVE;
	public MissionIco iconPrefab;
	public string missionName="";
	public string missionFinishedText="";
	public bool oneLife;
	public int coinAward;
	public int goldAward;
	
	private ArrayList missionListeners = new ArrayList();
	private string id;
	
	public virtual void SetActive(){
		if(state!=MissionStates.ACTIVE){
			state = MissionStates.ACTIVE;
			MissionActivated();
		}
		state = MissionStates.ACTIVE;		
	}
	
	public virtual void Restart(){
		
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
	
	public void RemoveMissionListener(IMissionListener missionListener){
		missionListeners.Remove(missionListener);	
	}
	
	protected void MissionFinished(){
		PersonInfo.AddCoins(coinAward);
		PersonInfo.AddGold(goldAward);
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
	
	protected void MissionActivated(){
		for(int i=0;i<missionListeners.Count;i++){
			((IMissionListener)missionListeners[i]).MissionActivated(this);
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
	
	public virtual void NotifyPostCollected(int post){
		
	}
	
	public virtual void NotifyVodkaCollected(int vodka){
		
	}
	
	public virtual void NotifyMagnitCollected (int magnit)
	{
	}
	
	public virtual void NotifySenoDeath (int senoDeath)
	{
	}
	
	public virtual void NotifyTraktorDeath (int traktorDeath)
	{

	}
	
	public virtual void NotifyX2Collected (int x2)
	{
	}
	
	public virtual void NotifyScarecrowDeath (int scarecrowDeath)
	{
		
	}
	//tutorial
	public virtual void NotifySlide (int slide)
	{
	}
	
	public virtual void NotifyRight (int right)
	{
	}
	
	public virtual void NotifyLeft (int left)
	{
	}
	//run
	public virtual void NotifyMetersRunned (int meters){
	}
	
	public virtual void NotifySlideUnderSomething (int something)
	{
	}
	
	public virtual void NotifyJump (int jump)
	{
		
	}
	
	public virtual void NotifyPointsAdded (int points)
	{
	}
	
	public virtual void NotifyJumpOverCaw (int caws)
	{
	}
	
	public virtual void NotifySlideUnderRope (int rope)
	{
	}
	
	public virtual void NotifyJumpOverHaystack (int haystack)
	{
	}
	
	public virtual void NotifyJumpOverDrova (int drova)
	{
		
	}
	
	public virtual void NotifyDodgeBaran (int baran)
	{
		
	}
	
	public virtual void NotifyDodgeTractor (int tractor)
	{
	}
	//drop
	public virtual void NotifyPostDropped (int post)
	{
		
	}
	
	public virtual string Serialize(){
		return " ";
	}
	
	public virtual void Unserialize(string data){
		//do nothing
	}
	
}
