using UnityEngine;
using System.Collections;

public class ScreenGameOverControllerScript : BaseScriptWithDialogScript {
	private void UpdatePoints()
	{
		/*int points=GlobalOptions.GetGuiLayer().GetPoints();
		tk2dTextMesh textMesh;
		textMesh = GuiScore.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0}", points);
		textMesh.Commit();*/
	}
	
	private void UpdateMoney()
	{
		/*int money=GlobalOptions.GetGuiLayer().GetMoney();
		tk2dTextMesh textMesh;
		textMesh = GuiMoney.GetComponent<tk2dTextMesh>();
		textMesh.text = string.Format ("{0:00000000}", money);
		textMesh.Commit();*/
	}
	
	//end Screen Controller To Show Methods
}
