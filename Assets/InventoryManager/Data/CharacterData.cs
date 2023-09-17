using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character")]
public class CharacterData : ScriptableObject {
    public string characterName;

    public Inventory inventory;
    [Range(0, 60)]
    public int level;
    [Range(0, 1000)]
    public int health;
    [Range(0, 100)]
    public int mana;
    [Range(0, 100)]
    public int attack;
    [Range(0, 100)]
    public int defense;
    [Range(0, 100)]
    public int magicAttack;
    [Range(0, 100)]
    public int magicDefense;
    [Range(0, 1000)]
    public int visionRange;
    public bool hasNightVision;

    public void AddItem(ItemData itemData) {
        inventory.items.Add(itemData);
    }

    private void OnValidate() {
        characterName = name;
    }
}
