using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[Serializable]
public class ItemData {
    public Item item;
    public bool isSoulbound;
    public bool isEquipped;
    public bool canBeDropped;
}

[CreateAssetMenu(fileName = "Inventory", menuName = "Data/Inventory")]
public class Inventory : ScriptableObject {
    public List<ItemData> items;

    private ItemData cachedFoundItem;
    private readonly Item tempItem = CreateInstance<Item>();

    public bool HasItem(Item item) {
        foreach (ItemData itemData in items) {
            if (itemData.item == item) {
                cachedFoundItem = itemData;
                return true;
            }
        }
        Debug.Log("Item not found in inventory");
        return false;
    }

    //  sometimes in code we check for an item and getting imediately if it exists
    //  this getter will make sure to return the item from the cached HasItem method
    public ItemData GetItem(Item item) {
        if (cachedFoundItem?.item == item) {
            return cachedFoundItem;
        }
        return HasItem(item) ? cachedFoundItem : null;
    }

    public ItemData GetItem(string itemName) {
        tempItem.itemName = itemName;
        return GetItem(tempItem);
    }
}
