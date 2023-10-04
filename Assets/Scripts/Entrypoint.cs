using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Entrypoint : MonoBehaviour {
    [SerializeField] public Transform playerPrefab;
    [SerializeField] public UIManager uiManagerPrefab;

    [SerializeField] private UIDocument panelUI;

    private void Awake() {
        // var player = Instantiate(playerPrefab);
        // var uiManager = Instantiate(uiManagerPrefab);
        //
        // uiManager.Initialize(player);

        var panel = panelUI.rootVisualElement;
        var title = panel.Q<Label>("Title");
        var button1 = panel.Q<Button>("Button1");

        title.text = "Hello World";
        button1.RegisterCallback<ClickEvent>(ev => {
            Debug.Log("Button 1 was clicked");
        });
    }
}

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SomeScript))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
    }
}

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Car))]
public class Car_Inspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        var label = new Label("This is a label");
        root.Add(label);

        var button = new Button(() => Debug.Log("Button pressed"));
        button.text = "Click me";
        root.Add(button);

        return root;
    }
}
