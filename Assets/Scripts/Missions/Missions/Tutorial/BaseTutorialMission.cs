using UnityEngine;
using System.Collections;

public class BaseTutorialMission : BaseOneNumberMission {
	public Abstract tutorialPrefab;
	Abstract tutorial;
	
	public override void SetActive ()
	{
		base.SetActive ();
		tutorial = Instantiate(tutorialPrefab) as Abstract;
	}
}
