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
		Transform curtransform,newobjTransform;
		
		//add all clothes to bear walking
		walkingBear=singleTransform.FindChild("WalkingBear").gameObject;
		curtransform=walkingBear.transform;
		curtransform.parent=singleTransform;
		for (int i=0;i<clothes.Length;i++)
		{
			newobj	= Instantiate (clothes[i]) as GameObject;
			newobjTransform=newobj.transform;
			newobjTransform.parent=curtransform;
			newobjTransform.Translate(initPos+singleTransform.position);
			clothesList.Add(newobj.transform.GetChild(0).gameObject);
			
			//make other invisible
			if(i!=0){
				newobj.GetComponent<MeshRenderer>().enabled=false;
			}
		}
		
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation["jump1"].layer=1;
			(clothesList[i] as GameObject).animation["jump2"].layer=1;
			(clothesList[i] as GameObject).animation["left"].layer=1;
			(clothesList[i] as GameObject).animation["right"].layer=1;
			(clothesList[i] as GameObject).animation["down"].layer=1;
			(clothesList[i] as GameObject).animation["yahoo"].layer=1;
			(clothesList[i] as GameObject).animation["stumble"].layer=1;
			(clothesList[i] as GameObject).animation["walk"].layer=0;
			(clothesList[i] as GameObject).animation["walk_alt1"].layer=1;
			(clothesList[i] as GameObject).animation["idle"].layer=0;
			(clothesList[i] as GameObject).animation["death"].layer=0;
			
			
			(clothesList[i] as GameObject).animation["posilka_left"].layer=1;
			(clothesList[i] as GameObject).animation["posilka_right"].layer=1;
			
			
			/////////////////////////////////
			(clothesList[i] as GameObject).animation["down"].speed=0.5f;
			(clothesList[i] as GameObject).animation["yahoo"].speed=1f;
			(clothesList[i] as GameObject).animation["stumble"].speed=1.5f;
			(clothesList[i] as GameObject).animation["jump1"].speed=0.7f;
			(clothesList[i] as GameObject).animation["jump2"].speed=0.8f;
			(clothesList[i] as GameObject).animation["left"].speed=1f;
			(clothesList[i] as GameObject).animation["right"].speed=1f;
			(clothesList[i] as GameObject).animation["death"].speed=0.9f;
			(clothesList[i] as GameObject).animation["walk"].speed=1.5f;
			(clothesList[i] as GameObject).animation["walk_alt1"].speed=1.5f;
			(clothesList[i] as GameObject).animation["idle"].speed=1f;
			
			(clothesList[i] as GameObject).animation["posilka_left"].speed=1f;
			(clothesList[i] as GameObject).animation["posilka_right"].speed=1f;
			
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
		string vspname="";
		for (int j=0;j<inAnimationName.Length;j++)
		{
			if(char.IsDigit(inAnimationName[j]))
			{
				break;
			}
			vspname+=inAnimationName[j];
		}
		if(curAnimationName!=vspname)
		{
			curAnimationName=vspname;
			for(int i=0;i<clothesList.Count;i++)
			{
				(clothesList[i] as GameObject).animation.CrossFade(inAnimationName);
			}
		}
	}
	
	private void PlayAnimationForName(string inAnimationName)
	{
		string vspname="";
		for (int j=0;j<inAnimationName.Length;j++)
		{
			if(char.IsDigit(inAnimationName[j]))
			{
				break;
			}
			vspname+=inAnimationName[j];
		}
		if(curAnimationName!=vspname)
		{
			curAnimationName=vspname;
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
		switch (Random.Range(0,2))
		{
			case 0:	CrossFadeAnimationForName("jump1"); break;
			case 1: CrossFadeAnimationForName("jump2"); break;
		}
	}
	
	public void WalkAlt() {
		switch (Random.Range(0,1))
		{
			case 0:	CrossFadeAnimationForName("walk_alt1"); break;
		}
	}
	
	public void Stumble() {
		//Debug.Log("Stumble");
		CrossFadeAnimationForName("stumble");
	}
	
	public void Down() {
		PlayAnimationForName("down");
		//Debug.Log ("Down");
	}
	
	public void Yahoo() {
		PlayAnimationForName("yahoo");
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
