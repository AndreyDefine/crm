using UnityEngine;
using System.Collections;

/// <summary>
/// Base class.
/// </summary>
public class DropPosilka : Abstract {
	
	public void FlyXYZRotateXYZStop()
	{
		GlobalOptions.GetPlayerScript().RemovePosilka();
		Destroy(gameObject,0.1f);
	}

}


