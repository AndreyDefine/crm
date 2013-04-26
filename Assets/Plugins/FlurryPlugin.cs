using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FlurryPlugin {

	/* Interface to native implementation */
	
	[DllImport ("__Internal")]
	private static extern void _FlurryStartSession (string ApiKey);
	[DllImport ("__Internal")]
	private static extern void _FlurryLogEvent(string EventName);
	
	/* Public interface for use inside C# / JS code */
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryStartSession(string ApiKey)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			_FlurryStartSession(ApiKey);
		}
		else
		{
			Debug.Log ("FlurryPlugin: Application.platform==RuntimePlatform.OSXEditor!!!");
		}
	}
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryLogEvent(string EventName)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			_FlurryLogEvent(EventName);
		}
		else
		{
			Debug.Log ("FlurryPlugin: LogEvent="+EventName);
		}
	}
}