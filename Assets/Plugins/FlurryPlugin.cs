using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FlurryPlugin
{

	/* Interface to native implementation */
	
	[DllImport ("__Internal")]
	private static extern void _FlurryStartSession (string inKey);

	[DllImport ("__Internal")]
	private static extern void _FlurryLogEvent (string EventName);
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	private static AndroidJavaObject _flurryAndroidPlugin;
	#endif
	private static string androidApiKey = "VX8CPY9YQBH4B3TH9KYP";
	private static string iosApiKey="CHKK77RVN3PJ5RZKTQTJ";
	
	/* Public interface for use inside C# / JS code */
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryStartSession ()
	{
		// Call plugin only when running on real device
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_FlurryStartSession(iosApiKey);
		}
		#if UNITY_ANDROID && !UNITY_EDITOR
			else if (Application.platform == RuntimePlatform.Android) {
			using (var pluginClass = new AndroidJavaClass( "com.ifree.flurryplugin.FlurryAndroidPlugin" ))
				_flurryAndroidPlugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			_flurryAndroidPlugin.Call ("setApiKey", androidApiKey); 
			bool started = _flurryAndroidPlugin.Call<bool> ("onStart"); 
		}
		#endif
		else {
			Debug.Log ("FlurryPlugin: Application.platform==RuntimePlatform.OSXEditor||RuntimePlatform.AndroidEditor!!!");
		}
	}
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryLogEvent (string EventName)
	{
		// Call plugin only when running on real device
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_FlurryLogEvent (EventName);
		}
		#if UNITY_ANDROID && !UNITY_EDITOR
		else if (Application.platform == RuntimePlatform.Android) {
			bool logged = _flurryAndroidPlugin.Call<bool>("logEvent",EventName);
		}
		#endif
		else{
			Debug.Log ("FlurryPlugin: LogEvent=" + EventName);
		}
	}
}
