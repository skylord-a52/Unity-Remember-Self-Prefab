using System;
using UnityEditor;
using UnityEditor.Search;
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
            pathProperty.stringValue = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
            Target.MarkChanged();
        }
        // if the user sets to null
        else if (prefab == null && Target.Prefab != null) {
            pathProperty.stringValue = "";
            Target.MarkChanged();
        }
        // if it changes from non-null to non-null
        else if (Target.Prefab != null && prefab.GetInstanceID() != Target.Prefab.GetInstanceID()) {
            pathProperty.stringValue = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
            Target.MarkChanged();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
