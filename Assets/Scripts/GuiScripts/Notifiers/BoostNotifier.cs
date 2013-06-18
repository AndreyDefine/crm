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
	
	
	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		boost.RemoveBoostListener(this);
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
	
	public void BoostFinished (Boost boost)//Переписать бы надо, а что делать(
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
		if(boost.GetType()==typeof(HeadStartBoost)){
			GlobalOptions.GetGuiLayer().StopHeadStart();
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
		progress.SetProgressColor(p);
	}	
	
}
