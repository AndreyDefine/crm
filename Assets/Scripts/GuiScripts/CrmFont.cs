using UnityEngine;
using System.Collections;

public class CrmFont : Abstract {

	public tk2dTextMesh[] textMeshes;
	
	private string _text = "";
    public string text {
        get {
            return _text;
        }
        set {
            _text = value;
			for(int i=0;i<textMeshes.Length;i++){
				textMeshes[i].text = text;
				textMeshes[i].Commit();
			}
        }
    }
	
    public Color color {
        set {
			for(int i=0;i<textMeshes.Length;i++){
				textMeshes[i].color = value;
				textMeshes[i].Commit();
			}
        }
    }
}
