using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FlurryPlugin
{

	/* Interface to native implementation */
	
	[DllImport ("__Internal")]
	private static extern void _FlurryStartSession ();

	[DllImport ("__Internal")]
	private static extern void _FlurryLogEvent (string EventName);
	
	//Android
	private static AndroidJavaObject _flurryAndroidPlugin;
	private static string androidApiKey = "VX8CPY9YQBH4B3TH9KYP";
	
	/* Public interface for use inside C# / JS code */
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryStartSession ()
	{
		// Call plugin only when running on real device
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_FlurryStartSession ();
		} else if (Application.platform == RuntimePlatform.Android) {
			using (var pluginClass = new AndroidJavaClass( "com.ifree.flurryplugin.FlurryAndroidPlugin" ))
				_flurryAndroidPlugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			_flurryAndroidPlugin.Call ("setApiKey", androidApiKey); 
			bool started = _flurryAndroidPlugin.Call<bool> ("onStart"); 
		} else {
			Debug.Log ("FlurryPlugin: Application.platform==RuntimePlatform.OSXEditor!!!");
		}
	}
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryLogEvent (string EventName)
	{
		// Call plugin only when running on real device
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_FlurryLogEvent (EventName);
		} else if (Application.platform == RuntimePlatform.Android) {
			bool logged = _flurryAndroidPlugin.Call<bool>("logEvent",EventName);
		}else{
			Debug.Log ("FlurryPlugin: LogEvent=" + EventName);
		}
	}
}
