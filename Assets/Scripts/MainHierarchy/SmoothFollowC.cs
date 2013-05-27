using UnityEngine;
using System.Collections;

public class SmoothFollowC : MonoBehaviour {
	
	public Transform target;
	public Player player;
	// The distance in the x-z plane to the target
	public float distance=10;
	// the height we want the camera to be above the target
	
	public float height=5;
	
	// How much we 
	public float heightDamping = 2.0f;
	public float xDamping = 5.0f;
	public float rotationDamping = 3.0f;
	
	// Update is called once per frame
	void LateUpdate () {
			// Early out if we don't have a target
		if (!target)
			return;
		
		if(player)
		{
			heightDamping=player.PlaceBearToControl();
		} 		
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		float wantedX=target.position.x;
			
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		float currentX = transform.position.x;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
	
		// Damp the height
		if(heightDamping>0)
		{
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		}
		
		//Damp the x
		
		currentX = Mathf.Lerp (currentX, wantedX, xDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
	
		// Set the height of the camera
		transform.position = new Vector3 (currentX,currentHeight,transform.position.z);
		//if(player)
		//{
		//	transform.position+=player.GetCameraDopSmex();
		//}
		
		// Always look at the target
		//transform.LookAt (target);
	}

	
}