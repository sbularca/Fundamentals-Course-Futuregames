using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order =-1)]
public class Item : ScriptableObject {
    [SerializeField] ItemCategory itemCategory;
    public string itemName;
    [Range(0, 10)]
    public int itemDurability;
    [Range(0, 10000)]
    public int itemValue;
    [Range(0f, 25f)]
    public float itemWeight;
    [Range(0, 100)]
    public int itemAttack;
    [Range(0, 100)]
    public int itemDefense;
    [Range(0, 100)]
    public int itemMagicAttack;
    [Range(0, 100)]
    public int itemMagicDefense;

    public string GetCategoryName() {
        return itemCategory.categoryName;
    }
}
