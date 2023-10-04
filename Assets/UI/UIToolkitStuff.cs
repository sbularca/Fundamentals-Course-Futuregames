using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class UIToolkitStuff : MonoBehaviour {
    UIDocument buttonDocument;
    private Button uiButton;

    private void Start() {
        buttonDocument = GetComponent<UIDocument>();
        uiButton = buttonDocument.rootVisualElement.Q<Button>("FirstButton");

        Assert.IsNull(uiButton);
    }
}
