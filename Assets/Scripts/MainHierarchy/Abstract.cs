using UnityEngine;
using System.Collections;

/// <summary>
/// Base class.
/// </summary>
public class Abstract : MonoBehaviour {

    private Transform _singleTransform = null;
    public Transform singleTransform {
        get {
            if (_singleTransform == null) {
                _singleTransform = transform;
            }
            return _singleTransform;
        }
        protected set {
            _singleTransform = value;
        }
    }
	
	private Renderer _singleRenderer = null;
    public Renderer singleRenderer {
        get {
            if (_singleRenderer == null) {
                _singleRenderer = renderer;
            }
            return _singleRenderer;
        }
        protected set {
            _singleRenderer = value;
        }
    }
	
	public Transform GetParent(int number){
		Transform current = singleTransform;
		for(int i=0;i<number;i++){
			current = current.parent;
			if(current == null){
				return null;
			}
		}
		return current;
	}
	
}


