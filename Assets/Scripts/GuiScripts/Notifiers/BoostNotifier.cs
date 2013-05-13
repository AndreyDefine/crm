using UnityEngine;
using System.Collections;


public class BoostNotifier : BaseNotifier,IBoostListener
{
	public Abstract iconPlace;
	public CutOut progress;
	private Boost boost;
	
	public Boost GetBoost(){
		return boost;
	}
	
	public void SetBoost(Boost boost){
		this.boost = boost;
		BoostIco boostIco = Instantiate(boost.iconPrefab) as BoostIco;
		boostIco.singleTransform.parent = iconPlace.singleTransform;
		boostIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
		boost.AddBoostListener(this);
		
	}
	
	public override void FlyInStopped(){
		FlyInEnd();
	}
	
	public override void FlyOutStopped(){
		FlyOutEnd();
	}
	
	public void SetIco(BoostIco boostIco){
		boostIco.singleTransform.parent = iconPlace.singleTransform;
		boostIco.singleTransform.localPosition = new Vector3(0f,0f,-0.01f);
	}
	
	public void BoostFinished (Boost boost)
	{
		FlyOut();
		if(boost.GetType()==typeof(VodkaBoost)){
			GlobalOptions.GetGuiLayer().StopVodka();
		}
		if(boost.GetType()==typeof(MagnitBoost)){
			GlobalOptions.GetGuiLayer().StopMagnit();
		}
		if(boost.GetType()==typeof(X2Boost)){
			GlobalOptions.GetGuiLayer().StopX2();
		}
		Destroy(boost.gameObject);
	}
	
	public void BoostProgressChanged (Boost boost)
	{
		SetProgress(boost.GetProgress());
	}
	
	public void SetProgress(float p){
		progress.CutOutTop(p);
		//хаки череваты последтсвиями, для таких вот CutOut цвет менять так
		progress.SetColor(new Color(1f-p,p,0f,1f));
	}	
	
}
