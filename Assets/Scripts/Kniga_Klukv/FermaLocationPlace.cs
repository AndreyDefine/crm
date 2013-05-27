using UnityEngine;
using System.Collections;

public class FermaLocationPlace : Abstract {
	
	public int price;
	public string levelToLoad="";
	public string factoryName = "";
	public bool initBought = false;
	public int needLevel;
	
	public Factory factory;
	public FermaNight night=null;
	public FermaLight light=null;
	
	private Ferma ferma;
	private bool play;
	private static string FACTORY_DATA_TAG = "factory_data_";
	
	private bool _bought;
    public bool bought {
        get {
            return _bought;
        }
        set {
            _bought = value;
			PlayerPrefs.SetInt(GetBoughtTag(),value?1:0);
        }
    }
	
	private string GetFactoryTag(){
		return FACTORY_DATA_TAG+name;
	}
	
	private string GetBoughtTag(){
		return GetFactoryTag()+"bought";
	}
	
	protected void Start(){
		_bought = initBought||PlayerPrefs.GetInt(GetBoughtTag(),0)==1;
		factory.SetFermaLocationPlace(this);
		factory.SetActive(bought);
		if(night!=null){
			night.SetFermaLocationPlace(this);
			night.SetActive(!bought);
		}
		if(light!=null){
			light.SetActive(!bought);
		}
	}
	
	public void SetFerma(Ferma ferma){
		this.ferma = ferma;	
	}
			
	public void ShowBuyDialog(){
		ferma.ShowBuyDialog(this);
	}
	
	public void ShowPlayDialog(){
		ferma.ShowPlayDialog(this);
	}
	
	public void Buy(){
		bought = true;
		night.TurnOffNight();
		PersonInfo.AddCoins(-price);
		CloseDialog();
	}
	
	public void NightOff(){
		light.SetActive(false);
		factory.SetActive(true);
	}
	
	public void Cancel(){
		CloseDialog();
	}
	
	public void CloseDialog(){
		ferma.CloseDialog();	
	}
	
	public void Play(){
		play = true;
		PersonInfo.lastFactoryName = name;
		CloseDialog();
	}
	
	public void DialogClosed(){
		if(play){
			play = false;
			string screenToShow="ScreenGame";
			ScreenLoader screenLoader;
			screenLoader=GameObject.Find("/ScreenLoader").GetComponent("ScreenLoader")as ScreenLoader;
			GlobalOptions.loadingLevel=levelToLoad;
			screenLoader.LoadScreenByName(screenToShow);
		}
	}
}
