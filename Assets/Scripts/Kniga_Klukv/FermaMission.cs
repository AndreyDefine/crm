using UnityEngine;
using System.Collections;

public class FermaMission : GuiButtonBase {
	
	public tk2dSprite plus;
	public CrmFont emmitTime;
	
	public void SetActive (bool b)
	{
		getTouches = b;
		if(b){
			emmitTime.gameObject.SetActiveRecursively(false);
			GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
			plus.gameObject.active = true;
		}else{
			emmitTime.gameObject.SetActiveRecursively(true);
			GetComponent<tk2dSprite>().color = new Color(0.5f,0.5f,0.5f,1f);
			plus.gameObject.active = false;
		}
	}
}
