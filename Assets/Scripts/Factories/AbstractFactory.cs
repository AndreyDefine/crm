using UnityEngine;
using System.Collections;

public class AbstractFactory : Abstract {
	public Vector3 initialPos;
	public bool drawMode=false;

	protected Vector3 oldObjectPos;
	
	protected int numberOfTerrains;
	protected Vector3 whereToBuildLocal;
	protected float drawnPerspective;
	protected GameObject Player;
	protected float terrainLength;
	
	// Use this for initialization
	void Start () {		
		LoadLevelFromGlobalOptions();
	}
	
	public void LoadLevelFromGlobalOptions()
	{
		Player=GlobalOptions.GetPlayer();
		drawnPerspective=GlobalOptions.globalPerspective;
		
		oldObjectPos=initialPos;
		
		init();
		PreloadTerrains();
	}
	
	public virtual void ReStart(){
		oldObjectPos=initialPos;
		int i;
		
		for(i=0;i<=numberOfTerrains;i++)
		{
			AddNextTerrain(false);
		}
	}
	
	public virtual void init(){
		//do nothing
	}
	
	private void PreloadTerrains() {
		int i;
		AddObjectsInPulls(false);
		for(i=0;i<=numberOfTerrains;i++)
		{
			AddNextTerrain(false);
		}
	}
	
	public virtual void AddObjectsInPulls(bool FlagCoRoutine){
	}
	
	public virtual void AddNextTerrain(bool FlagCoRoutine)
	{
	}
	
	public virtual void DeleteOneFirstTerrain()
	{
	}
	
	public virtual float GetTerrainLength()
	{
		return terrainLength;
	}
	
	public virtual float GetLastTerrainLength()
	{
		return terrainLength;
	}
	
	public virtual void TestPlayerTurn(GameObject terrainToDel){
		//do nothing
	}
	
	public virtual Vector3 GetPrevTerrainPos()
	{
		return new Vector3(0,0,0);
	}
	
	public virtual Vector3 GetLastTerrainPos()
	{
		return new Vector3(0,0,0);
	}
	
	public virtual Vector3 GetCurTerrainPos()
	{
		return new Vector3(0,0,0);
	}
}
