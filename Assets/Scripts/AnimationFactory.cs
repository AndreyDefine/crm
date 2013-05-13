using UnityEngine;
using System.Collections;

/// <summary>
/// Provides some basic useful animations.
/// </summary>
public static class AnimationFactory {
  
	    static void DeleteClipIfExists(GameObject obj, string clipName) {
        if (obj.animation [clipName] != null) {
            Component comp = obj.GetComponent<Animation> ();
            GameObject.DestroyImmediate (comp);
			//Debug.LogError("Trying to add animation that exists");
            obj.AddComponent (typeof(Animation));
        }
    }
	
    public static void FlyInXYZ(Abstract obj, Vector3 moveTo, float secs, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
    
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.x, secs, moveTo.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.y, secs, moveTo.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.z, secs, moveTo.z);
            
        curveX.AddKey (0.4f * secs, obj.singleTransform.localPosition.x - 0.55f * (obj.singleTransform.localPosition.x - moveTo.x));
        curveX.AddKey (0.7f * secs, obj.singleTransform.localPosition.x - 1.1f * (obj.singleTransform.localPosition.x - moveTo.x));
            
        curveY.AddKey (0.4f * secs, obj.singleTransform.localPosition.y - 0.55f * (obj.singleTransform.localPosition.y - moveTo.y));
        curveY.AddKey (0.7f * secs, obj.singleTransform.localPosition.y - 1.1f * (obj.singleTransform.localPosition.y - moveTo.y));
            
        //curveZ.AddKey (0.4f * secs, obj.singleTransform.localPosition.z - 0.55f * (obj.singleTransform.localPosition.z - moveTo.z));
        //curveZ.AddKey (0.7f * secs, obj.singleTransform.localPosition.z - 1.1f * (obj.singleTransform.localPosition.z - moveTo.z));
            
        animClip.SetCurve ("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localPosition.z", curveZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void FlyOutXYZ(Abstract obj, Vector3 moveTo, float secs, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
    
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.x, secs, moveTo.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.y, secs, moveTo.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.z, secs, moveTo.z);
            
        curveX.AddKey (0.4f * secs, obj.singleTransform.localPosition.x + 0.1f * (obj.singleTransform.localPosition.x - moveTo.x));
        curveX.AddKey (0.7f * secs, obj.singleTransform.localPosition.x - 0.55f * (obj.singleTransform.localPosition.x - moveTo.x));
            
        curveY.AddKey (0.4f * secs, obj.singleTransform.localPosition.y + 0.1f * (obj.singleTransform.localPosition.y - moveTo.y));
        curveY.AddKey (0.7f * secs, obj.singleTransform.localPosition.y - 0.55f * (obj.singleTransform.localPosition.y - moveTo.y));
           
        //curveZ.AddKey (0.5f * secs, obj.singleTransform.localPosition.z + 0.1f * (obj.singleTransform.localPosition.z - moveTo.z));
        //curveZ.AddKey (0.7f * secs, obj.singleTransform.localPosition.z - 0.55f * (obj.singleTransform.localPosition.z - moveTo.z));
            
        animClip.SetCurve ("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localPosition.z", curveZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    /// <summary>
    /// Adds the animation to GameObject, if it has no one.
    /// </summary>
    /// <param name='obj'>
    /// Object.
    /// </param>
    static void AddAnimation(GameObject obj) {
        if (obj.GetComponent<Animation> () == null) {
            obj.AddComponent (typeof(Animation));
        } else {
            obj.animation.Stop ();
        }
    }
	
	public static void FlyXYZ(Abstract obj, Vector3 moveTo, float secs, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
    
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.Linear(0, obj.singleTransform.localPosition.x, secs, moveTo.x);
        AnimationCurve curveY = AnimationCurve.Linear(0, obj.singleTransform.localPosition.y, secs, moveTo.y);
        AnimationCurve curveZ = AnimationCurve.Linear(0, obj.singleTransform.localPosition.z, secs, moveTo.z);
        animClip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Bounce(Abstract obj, float secs, float scaleTo, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveScaleX = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.x, secs, scaleTo);
        AnimationCurve curveScaleY = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.y, secs, scaleTo);
        AnimationCurve curveScaleZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.z, secs, scaleTo);
            
        curveScaleX.AddKey (0f * secs, obj.singleTransform.localScale.x * 0.1f);
        curveScaleX.AddKey (0.3f * secs, obj.singleTransform.localScale.x * 1.2f);
        curveScaleX.AddKey (0.55f * secs, obj.singleTransform.localScale.x * 0.6f);
        curveScaleX.AddKey (0.75f * secs, obj.singleTransform.localScale.x * 1f);
            
        curveScaleY.AddKey (0f * secs, obj.singleTransform.localScale.y * 0.1f);
        curveScaleY.AddKey (0.3f * secs, obj.singleTransform.localScale.y * 1.2f);
        curveScaleY.AddKey (0.55f * secs, obj.singleTransform.localScale.y * 0.6f);
        curveScaleY.AddKey (0.75f * secs, obj.singleTransform.localScale.y * 1f);
        
        curveScaleZ.AddKey (0f * secs, obj.singleTransform.localScale.z * 0.1f);
        curveScaleZ.AddKey (0.3f * secs, obj.singleTransform.localScale.z * 1.2f);
        curveScaleZ.AddKey (0.55f * secs, obj.singleTransform.localScale.z * 0.6f);
        curveScaleZ.AddKey (0.75f * secs, obj.singleTransform.localScale.z * 1f);
            
        animClip.SetCurve ("", typeof(Transform), "localScale.x", curveScaleX);
        animClip.SetCurve ("", typeof(Transform), "localScale.y", curveScaleY);
        animClip.SetCurve ("", typeof(Transform), "localScale.z", curveScaleZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Bounce2(Abstract obj, float secs, float scaleTo, float bounceTo, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveScaleX = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.x, secs, scaleTo);
        AnimationCurve curveScaleY = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.y, secs, scaleTo);
        AnimationCurve curveScaleZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.z, secs, scaleTo);
            
        curveScaleX.AddKey (0.3f * secs, obj.singleTransform.localScale.x * bounceTo);
        curveScaleY.AddKey (0.3f * secs, obj.singleTransform.localScale.y * bounceTo);
        curveScaleZ.AddKey (0.3f * secs, obj.singleTransform.localScale.z * bounceTo);
            
        animClip.SetCurve ("", typeof(Transform), "localScale.x", curveScaleX);
        animClip.SetCurve ("", typeof(Transform), "localScale.y", curveScaleY);
        animClip.SetCurve ("", typeof(Transform), "localScale.z", curveScaleZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Stamp(Abstract obj, float secs, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.x, secs, obj.singleTransform.localPosition.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.y, secs, obj.singleTransform.localPosition.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.z, secs, obj.singleTransform.localPosition.z);
        
        AnimationCurve curveRotateX = AnimationCurve.EaseInOut (0, obj.singleTransform.localRotation.x, secs, obj.singleTransform.localRotation.x);
        AnimationCurve curveRotateY = AnimationCurve.EaseInOut (0, obj.singleTransform.localRotation.y, secs, obj.singleTransform.localRotation.y);
        AnimationCurve curveRotateZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localRotation.z, secs, obj.singleTransform.localRotation.z);
        AnimationCurve curveRotateW = AnimationCurve.EaseInOut (0, obj.singleTransform.localRotation.w, secs, obj.singleTransform.localRotation.w);

        curveX.AddKey (0.3f * secs, obj.singleTransform.localPosition.x - 0.03f);
        curveY.AddKey (0.3f * secs, obj.singleTransform.localPosition.y + 0.04f);
        
        curveRotateX.AddKey (0.3f * secs, obj.singleTransform.localRotation.x + 0.04f);
        curveRotateY.AddKey (0.3f * secs, obj.singleTransform.localRotation.y + 0.035f);
            
        animClip.SetCurve ("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localPosition.z", curveZ);
        
        animClip.SetCurve ("", typeof(Transform), "localRotation.x", curveRotateX);
        animClip.SetCurve ("", typeof(Transform), "localRotation.y", curveRotateY);
        animClip.SetCurve ("", typeof(Transform), "localRotation.z", curveRotateZ);
        animClip.SetCurve ("", typeof(Transform), "localRotation.w", curveRotateW);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Shake(Abstract obj, float secs, int numberShake, float intensity, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.x, secs, obj.singleTransform.localPosition.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.y, secs, obj.singleTransform.localPosition.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.z, secs, obj.singleTransform.localPosition.z);
        
        int n = numberShake + 2;
        float iterval = 1 / ((float)n - 1);
        for (int i = 1; i<n-1; i++) {
            curveX.AddKey (iterval * i * secs, obj.singleTransform.localPosition.x + Random.Range (-intensity, intensity));
            curveY.AddKey (iterval * i * secs, obj.singleTransform.localPosition.y + Random.Range (-intensity, intensity));
        }
            
        animClip.SetCurve ("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localPosition.z", curveZ);

        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Next(Abstract obj, float secs, float movexTo, int times, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.x, secs, obj.singleTransform.localPosition.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.y, secs, obj.singleTransform.localPosition.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localPosition.z, secs, obj.singleTransform.localPosition.z);
        
        
        int n = times + 2;
        float iterval = 1 / ((float)n - 1);
        bool odd = true;
        for (int i = 1; i<n-1; i++) {
            if (odd) {
                curveX.AddKey (iterval * i * secs, obj.singleTransform.localPosition.x + movexTo);
            } else {
                curveX.AddKey (iterval * i * secs, obj.singleTransform.localPosition.x);    
            }
            odd = !odd;
        }
            
        animClip.SetCurve ("", typeof(Transform), "localPosition.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localPosition.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localPosition.z", curveZ);
        
        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void Attention(Abstract obj, float secs, float scale, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
         
		AnimationClip animClip = new AnimationClip ();
		AnimationCurve scaleX = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.x, secs, obj.singleTransform.localScale.x);
        AnimationCurve scaleY = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.y, secs, obj.singleTransform.localScale.y);
        AnimationCurve scaleZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.z, secs, obj.singleTransform.localScale.z);
            
        scaleX.AddKey (0.2f * secs, scale);
            
        scaleY.AddKey (0.2f * secs, scale);
           
        scaleZ.AddKey (0.2f * secs, scale);
            
        animClip.SetCurve ("", typeof(Transform), "localScale.x", scaleX);
        animClip.SetCurve ("", typeof(Transform), "localScale.y", scaleY);
        animClip.SetCurve ("", typeof(Transform), "localScale.z", scaleZ);
        
        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
    
    public static void AttentionLoop(Abstract obj, float secs, float scale, string clipName, string stopFunctionName = null, bool play = true) {
        AddAnimation (obj.gameObject);
        DeleteClipIfExists (obj.gameObject, clipName);
           
        AnimationClip animClip = new AnimationClip ();
        AnimationCurve curveX = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.x, secs, obj.singleTransform.localScale.x);
        AnimationCurve curveY = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.y, secs, obj.singleTransform.localScale.y);
        AnimationCurve curveZ = AnimationCurve.EaseInOut (0, obj.singleTransform.localScale.z, secs, obj.singleTransform.localScale.z);
                
        curveX.AddKey (secs/2, scale);    
        curveY.AddKey (secs/2, scale);    
        curveZ.AddKey (secs/2, scale);    
            
        animClip.SetCurve ("", typeof(Transform), "localScale.x", curveX);
        animClip.SetCurve ("", typeof(Transform), "localScale.y", curveY);
        animClip.SetCurve ("", typeof(Transform), "localScale.z", curveZ);
        
        if (stopFunctionName != null && stopFunctionName != "None") {
            AnimationEvent eventStop = new AnimationEvent ();
            eventStop.functionName = stopFunctionName;
            eventStop.time = secs;
            animClip.AddEvent (eventStop);
        }

        obj.animation.AddClip (animClip, clipName);
        obj.animation [clipName].layer = 1;
        obj.animation.wrapMode = WrapMode.Loop;
        if (play) {
            obj.animation.Play (clipName);
        }
    }
}
