using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor {

    private void OnEnable() {
        // here you subscribe to the Editor Update cycle
        //EditorApplication.update += UpdateEditorMethod;
    }
    private void UpdateEditorMethod() {
        Debug.Log("This message is updated on editor update Update");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        return; //remove this to test code

        // this runs when inspector active and unity wants
        Debug.Log("Saved changed from change");

        // this checks if there is a change in the inspector data
        EditorGUI.BeginChangeCheck();
        AssetDatabase.SaveAssets();
        Debug.Log("Saved changed from change");
        EditorGUI.EndChangeCheck();

        //this adds a button that saves this asset
        if(GUILayout.Button("Save Asset")) {
            AssetDatabase.SaveAssets();
        }
    }
}
