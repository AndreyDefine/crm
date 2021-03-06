using UnityEngine;
using System.Collections;

public class ObstacleSetDisplayer : MonoBehaviour {

// Attach this to a GUIText to make a frames/second indicator.
//
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
//
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.
 
    public  float updateInterval = 0.5F;
	private WorldFactory worldFactoryScript;
	float timeleft;
 
    void Start() {
        if (!guiText) {
            Debug.Log ("UtilityFramesPerSecond needs a GUIText component!");
            enabled = false;
            return;
        }
        timeleft = updateInterval;
    }
 
    void Update() {
		if(!worldFactoryScript)
		{
			//Get world factory script
			GameObject worldFactory=null;
			worldFactory=GlobalOptions.GetWorldFactory();
			if(worldFactory)
			{
				worldFactoryScript=worldFactory.GetComponent<WorldFactory>();
			}
		}
		
		if(!worldFactoryScript)
			return;
		
        timeleft -= Time.deltaTime;
    
        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0) {
			timeleft = updateInterval;
            string format = worldFactoryScript.GetCurrentObstacleSet();
            guiText.text = format;
			guiText.material.color = Color.blue;
        }
    }
}
