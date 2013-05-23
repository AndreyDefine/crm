using UnityEngine;
using System.Collections;

public class ScreenGameOverControllerScript : BaseScriptWithDialogScript {
	
	public override void ShowOnScreen ()
	{
		base.ShowOnScreen ();
		PersonInfo.AddCoins(GlobalOptions.GetGuiLayer().GetMoney());
	}
	//end Screen Controller To Show Methods
}
