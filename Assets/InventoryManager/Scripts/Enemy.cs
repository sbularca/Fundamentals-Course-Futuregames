using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public CharacterData characterData;

    public Action<ItemData> onPickup;

    public void AddItemToInventory(ItemData itemData) {
        characterData.AddItem(itemData);
    }
}
