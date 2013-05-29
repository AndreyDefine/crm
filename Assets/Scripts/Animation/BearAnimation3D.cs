using UnityEngine;
using System.Collections;

public class BearAnimation3D : Abstract{
	
	public Vector3 initPos;
	//walking
	public GameObject[] clothes;
	protected ArrayList clothesList=new ArrayList();
	GameObject walkingBear;
	
	private string curAnimationName="walk";
	
	void Start ()
	{
		GameObject newobj;
		Transform curtransform;
		
		//add all clothes to bear walking
		walkingBear=singleTransform.FindChild("WalkingBear").gameObject;
		curtransform=walkingBear.transform;
		curtransform.parent=singleTransform;
		for (int i=0;i<clothes.Length;i++)
		{
			newobj	= Instantiate (clothes[i]) as GameObject;
			newobj.transform.parent=curtransform;
			newobj.transform.Translate(initPos+singleTransform.position);
			clothesList.Add(newobj);
			
			//make other invisible
			if(i!=0){
				newobj.GetComponent<MeshRenderer>().enabled=false;
			}
		}
		
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation["jump"].layer=1;
			(clothesList[i] as GameObject).animation["left"].layer=1;
			(clothesList[i] as GameObject).animation["right"].layer=1;
			(clothesList[i] as GameObject).animation["down"].layer=1;
			(clothesList[i] as GameObject).animation["stumble"].layer=1;
			(clothesList[i] as GameObject).animation["walk"].layer=0;
			(clothesList[i] as GameObject).animation["death"].layer=0;
			
			
			(clothesList[i] as GameObject).animation["posilka_left"].layer=1;
			(clothesList[i] as GameObject).animation["posilka_right"].layer=1;
			
			(clothesList[i] as GameObject).animation["down"].speed=0.5f;
			(clothesList[i] as GameObject).animation["stumble"].speed=1.5f;
			(clothesList[i] as GameObject).animation["jump"].speed=0.7f;
			(clothesList[i] as GameObject).animation["left"].speed=1f;
			(clothesList[i] as GameObject).animation["right"].speed=1f;
			(clothesList[i] as GameObject).animation["death"].speed=0.9f;
			
			(clothesList[i] as GameObject).animation["posilka_left"].speed=1.2f;
			(clothesList[i] as GameObject).animation["posilka_right"].speed=1.2f;
			
			//(clothesList[i] as GameObject).animation["down"].weight=1;
		}
	}
	
	public void Restart(){
		for (int i=1;i<clothesList.Count;i++)
		{
			GameObject cap=clothesList[i] as GameObject;
			cap.GetComponent<MeshRenderer>().enabled=false;
		}
	}
	
	public void ShowCap(){
		
		for (int i=0;i<clothesList.Count;i++)
		{
			GameObject cap=clothesList[i] as GameObject;
			if(cap.GetComponent<MeshRenderer>().enabled==false)
			{
				cap.GetComponent<MeshRenderer>().enabled=true;
				break;
			}
		}
	}
	
	private void CrossFadeAnimationForName(string inAnimationName)
	{
		if(curAnimationName!=inAnimationName)
		{
			curAnimationName=inAnimationName;
			for(int i=0;i<clothesList.Count;i++)
			{
				(clothesList[i] as GameObject).animation.CrossFade(inAnimationName);
			}
		}
	}
	
	private void PlayAnimationForName(string inAnimationName)
	{
		if(curAnimationName!=inAnimationName)
		{
			curAnimationName=inAnimationName;
			for(int i=0;i<clothesList.Count;i++)
			{
				(clothesList[i] as GameObject).animation.Play(inAnimationName);
			}
		}
	}
	
	public void Walk () {
		if(GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			PlayAnimationForName("death");
		}
		else
		{
			CrossFadeAnimationForName("walk");
		}
		//Debug.Log ("Walk");
	}
	
	public void Right () {
		CrossFadeAnimationForName("right");
		//Debug.Log ("Right");
	}
	
	public void Left () {
		CrossFadeAnimationForName("left");
		//Debug.Log ("Left");
	}
	
	public void Dead() {
		PlayAnimationForName("death");
	}
	
	public void Idle() {
		CrossFadeAnimationForName("idle");
	}
	
	public void Jump() {
		CrossFadeAnimationForName("jump");
	}
	
	public void Stumble() {
		//Debug.Log("Stumble");
		CrossFadeAnimationForName("stumble");
	}
	
	public void Down() {
		PlayAnimationForName("down");
		//Debug.Log ("Down");
	}
	
	public void Posilka_Right() {
		CrossFadeAnimationForName("posilka_right");
	}
	
	public void Posilka_Left() {
		CrossFadeAnimationForName("posilka_left");
	}
	
	public void SetWalkSpeed(float inspeed) {
		
		/*for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation["walk"].speed=inspeed*1.5f;
		}*/
	}
	
	public void StopAnimation()
	{
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation.enabled=false;
		}
	}
	
	public void ResumeAnimation()
	{
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation.enabled=true;
		}
	}
}
