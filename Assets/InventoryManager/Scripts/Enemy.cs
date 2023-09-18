using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public CharacterData characterData;

    public Action<ItemData> onPickup;

    public void AddItemToInventory(ItemData itemData) {
        characterData.AddItem(itemData);
    }
}
