using UnityEngine;
using System.Collections;

public class GuiProgress : Abstract {
	
	public CutOut progress;

	public void SetProgress(float p){
		progress.CutOutRight(p);
		progress.SetProgressColor(p);
	}
}
