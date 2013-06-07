using UnityEngine;
using System.Collections;

public class FermaLocationPlace : Abstract {
	
	public int price;
	public string levelToLoad="";
	public string factoryName = "";
	public bool initBought = false;
	public int needLevel;
	
	public FermaMissionEmmitter missionEmmitterPrefab;
	
	private FermaMissionEmmitter _missionEmmitter=null;
	public FermaMissionEmmitter missionEmmitter{
		get{
			return _missionEmmitter;
		}
	}
	
	public Factory factory;
	public FermaNight night=null;
	public FermaLight fermaLight=null;
	
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
	
	private string GetLastMissionEmmitTimeTag(){
		return GetFactoryTag()+"last_mission_emmit";
	}
	
	protected void Start(){
		_missionEmmitter = Instantiate(missionEmmitterPrefab) as FermaMissionEmmitter;
		_missionEmmitter.singleTransform.parent = singleTransform;
		
		_bought = initBought||PlayerPrefs.GetInt(GetBoughtTag(),0)==1;
		
		
		factory.SetFermaLocationPlace(this);
		factory.SetActive(bought);
		if(night!=null){
			night.SetFermaLocationPlace(this);
			night.SetActive(!bought);
		}
		if(fermaLight!=null){
			fermaLight.SetActive(!bought);
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
		if(PersonInfo.TryToBuy(price, 0)){
			bought = true;
			night.TurnOffNight();
			CloseDialog();
		}
	}
	
	public void NightOff(){
		fermaLight.SetActive(false);
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
			screenLoader=GlobalOptions.GetScreenLoader();
			GlobalOptions.loadingLevel=levelToLoad;
			screenLoader.LoadScreenByName(screenToShow);
		}
	}
}
