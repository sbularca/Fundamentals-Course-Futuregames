﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class HandleColorOfMagic : MonoBehaviour {

    [SerializeField] private List<CreatureData> allCreaturesData;

    private int randomValue;
    private void Start() {

        var blackIsMyColor = new PreferBlack();
        var redIsMyColor = new PreferRed();

        randomValue = 5;
        // Debug.Log(blackIsMyColor.GetDefaultColor());
        // Debug.Log(blackIsMyColor.GetMyMagicColor());
        // Debug.Log(redIsMyColor.GetDefaultColor());
        // Debug.Log(redIsMyColor.GetMyMagicColor());

        ConstructorsExample.DoSomething();
    }
}
