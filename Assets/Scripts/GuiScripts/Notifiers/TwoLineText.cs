using UnityEngine;
using System.Collections;


public class TwoLineText : Abstract
{
	public CrmFont crmFontPrefab;
	public float yMove = 0.08f;
	
	public string text {
        set {
			
			string [] split = value.Split(new char [] {'\\'});
			if(split.Length == 1){
				CrmFont font = Instantiate(crmFontPrefab) as CrmFont;
				font.text = split[0];
				font.singleTransform.parent = singleTransform;
				font.singleTransform.localPosition = Vector3.zero;
			}else{
					CrmFont font = Instantiate(crmFontPrefab) as CrmFont;
					font.text = split[0];
					font.singleTransform.parent = singleTransform;
					font.singleTransform.localPosition = new Vector3(0f,yMove,0f);	
					
					CrmFont font2 = Instantiate(crmFontPrefab) as CrmFont;
					font2.text = split[1];
					font2.singleTransform.parent = singleTransform;
					font2.singleTransform.localPosition = new Vector3(0f,-yMove,0f);	
			}
        }
    }
}
