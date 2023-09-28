using System;
using UnityEngine;

[Serializable]
public class PlayerData : ISaveable {
    public string playerName;
    public int health;
    public float iq;

    public string ID { get; set; }

    private PlayerData playerData;

    private PlayerData(string playerName, int health, float iq) {
        this.playerName = playerName;
        this.health = health;
        this.iq = iq;

        playerData = CreateInstance("MeMeMe", 5, 73.21f);
        Register();
    }

    public static PlayerData CreateInstance(string playerName, int health, float iq) {
        return new PlayerData(playerName, health, iq);
    }

    public void Save() {
        SaveManager.Instance.SaveToDisk<PlayerData>(playerData);
    }

    public void Load() {
        playerData = SaveManager.Instance.LoadFromDisk<PlayerData>();
    }
    public void Register() {
        SaveManager.objectsToSave.Add(this);
    }
}
