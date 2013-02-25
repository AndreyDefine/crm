using UnityEngine;
using System.Collections;

public class BearAnimation3D : Abstract{
	
	public Vector3 initPos;
	//walking
	public GameObject[] clothes;
	protected ArrayList clothesList=new ArrayList();
	GameObject walkingBear;
	
	private string curAnimationName="Take 001";
	
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
			for(int i=0;i<clothesList.Count;i++)
			{
				(clothesList[i] as GameObject).animation.CrossFade(inAnimationName);
			}
		}
	}
	
	public void Walk () {
		PlayAnimationForName("Take 001");
	}
	
	public void Dead() {
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation.Stop();
		}
	}
	
	public void Idle() {
		//PlayAnimationForName("idle");
	}
	
	public void Jump() {
		//animation.CrossFade("jump");
	}
	
	public void SetWalkSpeed(float inspeed) {
		
		for(int i=0;i<clothesList.Count;i++)
		{
			(clothesList[i] as GameObject).animation[curAnimationName].speed=inspeed;
		}
	}
}
