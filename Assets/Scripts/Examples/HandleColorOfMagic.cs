using System;
using System.Collections.Generic;
//using UnityEditor.Scripting;
using UnityEngine;

public class HandleColorOfMagic : MonoBehaviour {

    [SerializeField] private List<CreatureData> allCreaturesData;
    private void Start() {

        var blackIsMyColor = new PreferBlack();
        var redIsMyColor = new PreferRed();

        // Debug.Log(blackIsMyColor.GetDefaultColor());
        // Debug.Log(blackIsMyColor.GetMyMagicColor());
        // Debug.Log(redIsMyColor.GetDefaultColor());
        // Debug.Log(redIsMyColor.GetMyMagicColor());

        ConstructorsExample.DoSomething();
    }
}
