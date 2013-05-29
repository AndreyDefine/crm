using UnityEngine;
using System.Collections;

public class Ferma : Abstract {
	
	private Hashtable placesHashtable = new Hashtable();
	public DialogFermaBuy dialogFermaBuyPrefab;
	public DialogFermaPlay dialogFermaPlayPrefab;
	private DialogFerma dialogFerma;	
	private bool dialogFermaShown = false;
	
	public FermaLocationPlace[] places;
	
	void Start(){
		for(int i=0;i<places.Length;i++){
			FermaLocationPlace fact = places[i];
			placesHashtable.Add(fact.name,fact);	
			fact.SetFerma(this);
		}
		
		FermaLocationPlace place = placesHashtable[PersonInfo.lastFactoryName] as FermaLocationPlace;
		if(place!=null){
			GetComponent<ZoomMap>().SetPos(new Vector3(-place.singleTransform.localPosition.x,-place.singleTransform.localPosition.y,singleTransform.position.z));
			//place.ShowPlayDialog();
		}
	}
	
	public void DialogOpened(FermaLocationPlace place){
		for(int i=0;i<places.Length;i++){
			FermaLocationPlace plac = places[i];
			if(place!=plac){
				plac.Cancel();
			}
		}
	}
	
	public void CloseDialog(){
		if(dialogFermaShown){
			dialogFerma.CloseDialog();
			dialogFermaShown = false;
		}	
	}
	
	private void ShowDialog(DialogFerma dialogFermaPrefab, FermaLocationPlace place){
		if(dialogFermaShown){
			return;		
		}
		dialogFermaShown = true;
		dialogFerma = Instantiate(dialogFermaPrefab) as DialogFerma;
		dialogFerma.SetFermaLocationPlace(place);
		dialogFerma.singleTransform.localPosition = Vector3.zero;
		dialogFerma.Show();
	}
	
	public void ShowBuyDialog(FermaLocationPlace place){
		ShowDialog(dialogFermaBuyPrefab, place);
	}
	
	public void ShowPlayDialog(FermaLocationPlace place){
		ShowDialog(dialogFermaPlayPrefab, place);
	}
	
}
