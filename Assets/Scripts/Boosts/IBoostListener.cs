using UnityEngine;
using System.Collections;

public interface IBoostListener{
	void BoostProgressChanged(Boost boost);
	void BoostFinished(Boost boost);
}
