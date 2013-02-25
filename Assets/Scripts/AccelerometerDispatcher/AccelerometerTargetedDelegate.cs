using UnityEngine;
using System.Collections;

public interface AccelerometerTargetedDelegate{
	bool Accelerate(Vector3 acceleration,int infingerId);
}
