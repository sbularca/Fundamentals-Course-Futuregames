using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsCatalogue", menuName = "Data/ItemsCatalogue", order = 1)]
public class ItemsCatalogue : ScriptableObject {
    public ItemCategory itemCategory;
    public List<Item> items;

    public Item GetItem(string itemName) {
        foreach (Item item in items) {
            if (item.itemName == itemName) {
                return item;
            }
        }
        return null;
    }
}
