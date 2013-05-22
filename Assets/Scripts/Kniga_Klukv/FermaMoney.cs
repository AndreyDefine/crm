using UnityEngine;
using System.Collections;

public class FermaMoney : Abstract {

	public CrmFont crmFont;
	
	void Start(){
		Vector3 pos=new Vector3(GlobalOptions.Vsizex-15,GlobalOptions.Vsizey-37,singleTransform.position.z);
		pos=GlobalOptions.NormalisePos(pos);
		pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
		singleTransform.position=pos;	
	}
	
	public void SetMoney(int money){
		crmFont.text = string.Format ("{0}", money);
	}
}
