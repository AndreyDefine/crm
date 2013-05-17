using UnityEngine;
using System.Collections;

public class Money : Abstract {

	public CrmFont crmFont;
	private int money;
	
	void Awake(){
		Vector3 pos=new Vector3(GlobalOptions.Vsizex-15,GlobalOptions.Vsizey-92,singleTransform.position.z);
		pos=GlobalOptions.NormalisePos(pos);
		pos=Cameras.GetGUICamera().ScreenToWorldPoint(pos);
			
		singleTransform.position=pos;	
	}
	
	public void AddMoney(int addMoney){
		this.money+=addMoney;
		crmFont.text = string.Format ("{0}", this.money);
	}
	
	public void SetMoney(int money){
		this.money = money;
		AddMoney(0);
	}
	
	public int GetMoney(){
		return money;
	}
}
