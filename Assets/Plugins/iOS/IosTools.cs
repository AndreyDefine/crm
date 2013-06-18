using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IOS
public class IosTools
{
	[DllImport("__Internal")]
	private static extern float IosTools_GetScreenScale();
	
	public static float GetScreenScale()
	{
		return IosTools_GetScreenScale();	
	}
	
}
#endif
