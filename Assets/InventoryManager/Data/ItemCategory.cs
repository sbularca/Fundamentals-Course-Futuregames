using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCategory", menuName = "Data/ItemCategory")]
public class ItemCategory : ScriptableObject {
    public string categoryName;

    private void OnValidate() {
        AssetDatabase.SaveAssets();
        categoryName = name;
    }
}
