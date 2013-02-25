using UnityEngine;
using System.Collections;

public class QualitySwitcher : Abstract {    
    void Awake() {
        if (!GlobalOptions.qualitySeted) {
            GlobalOptions.qualitySeted = true;
            SetCorrectQuality ();
        }
    }
    
    void SetCorrectQuality() {
        /*string[] names = QualitySettings.names;
        int i = 0;
        while (i < names.Length) {
            Debug.Log (names [i]);
            i++;
        }*/
		
#if UNITY_ANDROID
		float screenScale=Screen.height/GlobalOptions.Vsizey;
		if(screenScale>0.6f)
		{
			QualitySettings.SetQualityLevel (0);
		}
		if(screenScale<=0.6f&&screenScale>0.32f)
		{
			QualitySettings.SetQualityLevel (2);
		}
		
		if(screenScale<=0.32f)
		{
			QualitySettings.SetQualityLevel (3);
		}	
#elif UNITY_IPHONE
		//слабые айфоны
        QualitySettings.SetQualityLevel(iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS ? 1 : 0);
#else
        QualitySettings.SetQualityLevel (0);
#endif
    }
}
