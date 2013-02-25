using UnityEngine;
using System.Collections;

public class BearAnimation : Abstract{
	
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
		walkingBear=new GameObject();
		walkingBear.name="WalkingBear";
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
	
	private void PlayAnimationForName(string inAnimationName)
	{
		if(curAnimationName!=inAnimationName)
		{
			curAnimationName=inAnimationName;
			tk2dAnimatedSprite animBear;
			for(int i=0;i<clothesList.Count;i++)
			{
				animBear=(clothesList[i] as GameObject).GetComponent<tk2dAnimatedSprite>();
				animBear.Play(inAnimationName);
			}
		}
	}
	
	public void Walk () {
		PlayAnimationForName("walk");
	}
	
	public void Dead() {
		PlayAnimationForName("dead");
	}
	
	public void Idle() {
		PlayAnimationForName("idle");
	}
	
	public void Jump() {
		//animation.CrossFade("jump");
	}
	
	public void SetWalkSpeed(float inspeed) {
		/*
		tk2dAnimatedSprite animBear;
		for(int i=0;i<clothesList.Count;i++)
		{
			animBear=(clothesList[i] as GameObject).GetComponent<tk2dAnimatedSprite>();
		}
		*/
	}
}
