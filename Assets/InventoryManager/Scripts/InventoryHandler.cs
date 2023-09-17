using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
    public CharacterData characterData;

    public void AddItemToInventory(ItemData itemData) {
        characterData.AddItem(itemData);
    }

}
