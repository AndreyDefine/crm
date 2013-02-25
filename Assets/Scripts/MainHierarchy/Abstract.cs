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
}


