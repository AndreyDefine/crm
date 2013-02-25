using UnityEngine;
using System.Collections;

public class Music : Abstract {
    public static Music instance = null;
	// Use this for initialization
	void Start () {
        if(instance!=null){
			instance.audio.pitch=GlobalOptions.startMusicPitch;
            Destroy(gameObject);
			Debug.Log ("One Music Destroyed");
            return;
        }
	    instance = this;
		//добавим в глобал обжект
		GlobalOptions.MainThemeMusicScript=this;
		GlobalOptions.startMusicPitch=audio.pitch;
        DontDestroyOnLoad (gameObject);
        //if(GlobalOptions.isSound()){
            audio.Play();
        //}
	}
    
    public void Pause(){
        audio.Pause();
    }
    
    public void Play(){
        audio.Play();
    }
	
	public void SetMusicPitch(float inPitch)
	{
		audio.pitch=inPitch;
	}
}
