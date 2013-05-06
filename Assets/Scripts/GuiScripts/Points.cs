using UnityEngine;
using System.Collections;

public class Points : Abstract {

	public tk2dTextMesh tk2dTextMeshPoints;
	private int points;
	
	public void AddPoints(int addPoints){
		this.points+=addPoints;
		tk2dTextMeshPoints.text = string.Format ("{0:00000000}", this.points);
		tk2dTextMeshPoints.Commit();
	}
	
	public void SetPoints(int points){
		this.points = points;
		AddPoints(0);
	}
	
	public int GetPoints(){
		return points;
	}
}
