using UnityEngine;
using System.Collections;

public class GuiPointsAndMoney : Abstract {

	public CrmFont points;
	public CrmFont money;
	
	public void InitPointsAndMoney(){
		int pointsValue=GlobalOptions.GetGuiLayer().GetPoints();
		int moneyValue=GlobalOptions.GetGuiLayer().GetMoney();
		points.text = string.Format("{0}",pointsValue);
		money.text = string.Format("{0}",moneyValue);
	}
}
