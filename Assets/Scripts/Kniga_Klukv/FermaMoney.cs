using UnityEngine;
using System.Collections;

public class FermaMoney : GlobalMoney {

	void Start(){
		SetMoney(PersonInfo.coins);
	}
}
