using UnityEngine;
using System.Collections;

public class Money : Abstract {

	public tk2dTextMesh tk2dTextMeshMoney;
	private int money;
	
	public void AddMoney(int addMoney){
		this.money+=addMoney;
		tk2dTextMeshMoney.text = string.Format ("{0:00000}", this.money);
		tk2dTextMeshMoney.Commit();
	}
	
	public void SetMoney(int money){
		this.money = money;
		AddMoney(0);
	}
	
	public int GetMoney(){
		return money;
	}
}
