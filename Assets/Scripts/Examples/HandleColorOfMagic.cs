using System;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;

public class HandleColorOfMagic : MonoBehaviour {

    [SerializeField] private List<CreatureData> allCreaturesData;

    private void Awake() {
        Debug.Log("called");
    }
    private void OnEnable() {
        Debug.Log("OnEnable");
    }
    private int randomValue;
    private void Start() {

        Debug.Log("Start");
        // var blackIsMyColor = new PreferBlack();
        // var redIsMyColor = new PreferRed();
        //
        // randomValue = 5;
        // // Debug.Log(blackIsMyColor.GetDefaultColor());
        // // Debug.Log(blackIsMyColor.GetMyMagicColor());
        // // Debug.Log(redIsMyColor.GetDefaultColor());
        // // Debug.Log(redIsMyColor.GetMyMagicColor());
        //
        // ConstructorsExample.DoSomething();
    }

    private void Update() {
        Debug.Log("called");
    }

    private void OnDisable() {
        Debug.Log("OnDisable");
    }
}
