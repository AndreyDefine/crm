using UnityEngine;
using System.Collections;

public class FlurryInit : MonoBehaviour {
	void Start () {
		init();
	}
	
	//initFlurry
	private void init()
	{
		FlurryPlugin.FlurryStartSession("CHKK77RVN3PJ5RZKTQTJ");
	}
}
