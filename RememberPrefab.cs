using UnityEditor;
using UnityEngine;

/// <summary>
/// Component that contains a single variable -- a reference to a prefab object.
/// This must be set by other code, or in the inspector.
/// </summary>
public class RememberPrefab: MonoBehaviour {

    [SerializeField]
    private string _path;
    public string Path {
        get => _path;
        set {
            _hasChanged = true;
            _path = value;
        }
    }
    
    private GameObject _prefabCached;
    private bool _hasChanged = false;

    public GameObject Prefab {
        get {
            if (string.IsNullOrEmpty(_path)) {
                return null;
            }
            
            if (_prefabCached == null || _hasChanged) {
                _prefabCached = (GameObject) AssetDatabase.LoadAssetAtPath(Path, typeof(GameObject));
                _hasChanged = false;
            }
            
            return _prefabCached;
        }
    }

    public void MarkChanged() {
        _hasChanged = true;
    }
}
