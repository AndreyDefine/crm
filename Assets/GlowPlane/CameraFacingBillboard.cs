using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{
    Camera referenceCamera;
 
    void Update()
    {
        transform.LookAt(transform.position + referenceCamera.transform.rotation * Vector3.back,
            referenceCamera.transform.rotation * Vector3.up);
    }
	
	void  Awake ()
	{
		// if no camera referenced, grab the main camera
		if (!referenceCamera)
			referenceCamera = Camera.main; 
	}
}