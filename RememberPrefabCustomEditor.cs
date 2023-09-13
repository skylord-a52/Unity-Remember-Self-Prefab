using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RememberPrefab))]
[CanEditMultipleObjects]
public class RememberPrefabCustomEditor : UnityEditor.Editor {
    
    private RememberPrefab Target;
    private SerializedProperty pathProperty;

    void OnEnable() {
        Target = (RememberPrefab) target;
        pathProperty = serializedObject.FindProperty("_path");
    }
    
    public override void OnInspectorGUI() {

        GameObject prefab;

        GUIContent label = new GUIContent("Prefab");
        var rect = EditorGUILayout.GetControlRect();
        label = EditorGUI.BeginProperty(rect, label, pathProperty);
        using (new EditorGUI.DisabledScope(Application.isPlaying)) {
            prefab = (GameObject) EditorGUI.ObjectField(rect, label, Target.Prefab, typeof(GameObject), false);
        }
        EditorGUI.EndProperty();
        
        GUILayout.Label($"Asset pathProperty: {Target.Path}");
        string idString = Target.Prefab == null ? "null" : Target.Prefab.GetInstanceID().ToString();
        GUILayout.Label($"Instance ID: {idString}");
        
        // if this is the first time it's set
        if (Target.Prefab == null && prefab != null) {
            AttemptChange(prefab);
        }
        // if the user sets to null
        else if (prefab == null && Target.Prefab != null) {
            pathProperty.stringValue = "";
            Target.MarkChanged();
        }
        // if it changes from non-null to non-null
        else if (Target.Prefab != null && prefab.GetInstanceID() != Target.Prefab.GetInstanceID()) {
            AttemptChange(prefab);
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    private void AttemptChange(GameObject prefab) {

        var newPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
        
        if (!newPath.Contains("Resources/")) {
            Debug.LogWarning("Prefabs stored in RememberPrefab must be inside of a Resources folder");
            return;
        }

        var splitPaths = newPath.Split("Resources/");
        var reducedPath = splitPaths.Last();

        splitPaths = reducedPath.Split(".");
        reducedPath = splitPaths[0];

        pathProperty.stringValue = reducedPath;
        Target.MarkChanged();
    }
}
