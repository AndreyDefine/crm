using UnityEngine;
using System.Collections;

public class GuiResurrection : GuiBaseTwinkling {
	
	protected override void MakeOnStop()
	{
		GlobalOptions.GetGuiLayer().ShowGameOverScreen();
	}
}
