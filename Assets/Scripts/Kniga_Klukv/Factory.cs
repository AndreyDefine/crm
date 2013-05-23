using UnityEngine;
using System.Collections;

public class Factory : SpriteTouch {
	
	public int price;
	public string levelToLoad="";
	public bool initBought = false;
	public int needLevel;
	public int playingLevelNumber;
	
	private Factories facotries;
	private bool play;
	private static string FACTORY_DATA_TAG = "factory_data_";
	private Vector3 initScale;
	public DialogFermaBuy dialogFermaBuyPrefab;
	public DialogFermaPlay dialogFermaPlayPrefab;
	private DialogFerma dialogFerma;
	private bool dialogFermaShown = false;
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
	
	protected override void Start(){
		base.Start();
		initScale = singleTransform.localScale;
		_bought = initBought||PlayerPrefs.GetInt(GetBoughtTag(),0)==1;
		SetBought(bought);
		AnimationFactory.AttentionLoop(this,2f,1.05f,"Attention");
	}
	
	public void SetFactories(Factories factories){
		this.facotries = factories;	
	}
	
	public void SetBought(bool b){
		bought = b;
		if(b){
			GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,1f);
		}else{
			GetComponent<tk2dSprite>().color = new Color(0.5f,0.5f,0.5f,1f);
		}
	}
	
	public override bool TouchBegan(Vector2 position,int fingerId) {
		bool isTouchHandled=base.TouchBegan(position,fingerId);
		if(isTouchHandled){	
			initScale = singleTransform.localScale;
			singleTransform.localScale = initScale*1.05f;
		}

		return isTouchHandled;
	}
	
	public override void TouchMoved(Vector2 position, int fingerId)
	{
		base.TouchMoved (position, fingerId);
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			singleTransform.localScale = initScale*1.05f;
		}
		else
		{
			singleTransform.localScale = initScale;
		}
	}
	
	public override void TouchEnded (Vector2 position, int fingerId)
	{
		singleTransform.localScale = initScale;
		base.TouchEnded (position, fingerId);
		bool isTouchHandled=MakeDetection(position);
		if(isTouchHandled){	
			MakeOnTouch();
		}
	}
	
	virtual protected void MakeOnTouch(){
		if(bought){
			ShowPlayDialog();
		}else{
			ShowBuyDialog();
		}
	}
		
	private void ShowDialog(DialogFerma dialogFermaPrefab){
		facotries.DialogOpened(this);
		if(dialogFermaShown){
			return;		
		}
		dialogFermaShown = true;
		dialogFerma = Instantiate(dialogFermaPrefab) as DialogFerma;
		dialogFerma.SetFactory(this);
		dialogFerma.ShowForPosition(new Vector3(0f, 0f, singleTransform.position.z-0.01f));
	}
	
	public void ShowBuyDialog(){
		ShowDialog(dialogFermaBuyPrefab);
	}
	
	public void ShowPlayDialog(){
		ShowDialog(dialogFermaPlayPrefab);
	}
	
	public void Buy(){
		bought = true;
		SetBought(true);
		PersonInfo.AddCoins(-price);
		CloseDialog();
	}
	
	public void Cancel(){
		CloseDialog();
	}
	
	public void CloseDialog(){
		if(dialogFermaShown){
			dialogFerma.CloseDialog();
			dialogFermaShown = false;
		}	
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
			GlobalOptions.PlayingLevelNumber=playingLevelNumber;
			GlobalOptions.SavePrefsLastPlayed();
			screenLoader.LoadScreenByName(screenToShow);
			Debug.Log (GlobalOptions.PlayingLevelNumber);
		}
	}
}
