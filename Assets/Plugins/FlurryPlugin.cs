using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FlurryPlugin
{

	/* Interface to native implementation */
	#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void _FlurryStartSession (string inKey);

	[DllImport ("__Internal")]
	private static extern void _FlurryLogEvent (string EventName);
	#endif
	
	#if UNITY_ANDROID
	private static AndroidJavaObject _flurryAndroidPlugin;
	#endif
	private static string androidApiKey = "VX8CPY9YQBH4B3TH9KYP";
	private static string iosApiKey="CHKK77RVN3PJ5RZKTQTJ";
	
	/* Public interface for use inside C# / JS code */
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryStartSession ()
	{
		// Call plugin only when running on real device
		#if UNITY_IPHONE
			_FlurryStartSession(iosApiKey);
		#elif UNITY_ANDROID
			using (var pluginClass = new AndroidJavaClass( "com.ifree.flurryplugin.FlurryAndroidPlugin" ))
					_flurryAndroidPlugin = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			_flurryAndroidPlugin.Call ("setApiKey", androidApiKey); 
			bool started = _flurryAndroidPlugin.Call<bool> ("onStart"); 
		#else
			Debug.Log ("FlurryPlugin: Application.platform==RuntimePlatform.OSXEditor||RuntimePlatform.AndroidEditor!!!");
		#endif
	}
	
	// Starts lookup for some bonjour registered service inside specified domain
	public static void FlurryLogEvent (string EventName)
	{
		// Call plugin only when running on real device
		#if UNITY_IPHONE
			_FlurryLogEvent (EventName);
		#elif UNITY_ANDROID
			bool logged = _flurryAndroidPlugin.Call<bool>("logEvent",EventName);
		#else
			Debug.Log ("FlurryPlugin: LogEvent=" + EventName);
		#endif
	}
}
