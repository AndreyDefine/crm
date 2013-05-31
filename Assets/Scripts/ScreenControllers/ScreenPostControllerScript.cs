using UnityEngine;
using System.Collections;

public class ScreenPostControllerScript : Abstract,ScreenControllerToShow {
	
	public GameObject GUIPostPicture; 
	public GameObject GUIPostNumber;
	
	private GameObject PostPicture;
	private CrmFont PostNumber;
	
	public virtual void ShowOnScreen()
	{
		PostPicture=Instantiate(GUIPostPicture) as GameObject;
		
		
		PostNumber=(Instantiate(GUIPostNumber) as GameObject).GetComponent<CrmFont>();
		PostNumber.transform.position=new Vector3(0,-0.5f,0);
		PostNumber.text="You Collected "+PersonInfo.post+"\npostal boxes!!!";
	}
	
	public virtual void HideOnScreen()
	{
		if(PostPicture)
		{
			Destroy(PostPicture);
			Destroy(PostNumber.gameObject);
		}
	}
	//end Screen Controller To Show Methods
}
