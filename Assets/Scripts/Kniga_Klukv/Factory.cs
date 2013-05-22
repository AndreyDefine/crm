using UnityEngine;
using System.Collections;

public class Factory : SpriteTouch {
	
	public int price;
	private static string FACTORY_DATA_TAG = "factory_data";
	private Vector3 initScale;
	public DialogFerma dialogFermaPrefab;
	public float smX = 0f;
	public float smY = 0.5f;
	
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
		_bought = PlayerPrefs.GetInt(GetBoughtTag(),0)==1;
		SetBought(bought);
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
		DialogFerma dialogFerma = Instantiate(dialogFermaPrefab) as DialogFerma;
		dialogFerma.singleTransform.parent = singleTransform.parent;
		dialogFerma.ShowForPosition(new Vector3(singleTransform.position.x+smX, singleTransform.position.y+smY, singleTransform.position.z-0.01f));
	}
}
