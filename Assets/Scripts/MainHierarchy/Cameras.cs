using UnityEngine;
using System.Collections;

public static class Cameras {
    static float pixelPerUnit;

    public static Camera GetGUICamera() {
        foreach (Camera camera in Camera.allCameras) {
            if (camera.name.Equals ("GUICamera")) {
                return camera;
            }
        }
        return null;
    }

    public static float GuiCameraPixelPerUnit() {
            Camera camera = GetGUICamera ();
            Vector3 p1 = camera.WorldToScreenPoint (new Vector3 (0, 0, 0));
            Vector3 p2 = camera.WorldToScreenPoint (new Vector3 (0, 1f, 0));
            pixelPerUnit=Mathf.Abs(p2.y - p1.y);
        return pixelPerUnit;
    }
    
    public static Vector2 cameraBoundsInUnits(Camera camera, float z){
        Vector3 p1 = camera.ViewportToWorldPoint (new Vector3 (1, 1, z));
        Vector3 p2 = camera.ViewportToWorldPoint (new Vector3 (0.5f, 1f, z));       
        float x = Mathf.Sqrt(Mathf.Pow((p1.x - p2.x),2f)+Mathf.Pow((p1.y - p2.y),2f)+Mathf.Pow((p1.z - p2.z),2f));
        
        Vector3 p3 = camera.ViewportToWorldPoint (new Vector3 (1, 1, z));
        Vector3 p4 = camera.ViewportToWorldPoint (new Vector3 (1f, 0.5f, z));    
        float y = Mathf.Sqrt(Mathf.Pow((p3.x - p4.x),2f)+Mathf.Pow((p3.y - p4.y),2f)+Mathf.Pow((p3.z - p4.z),2f));
        return new Vector2(x,y);
    }
}