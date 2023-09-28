using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour, ISaveable {
    private const string healthKey = "health";

    [SerializeField] private int startingHealth = 20;

    private PlayerPrefsSave saveSystem;

    public int Health {
        get => saveSystem.Load<int>(healthKey);
        set => saveSystem.Save(healthKey, value);
    }

    private void Awake() {
        saveSystem = PlayerPrefsSave.Instance;
        LoadSavedData();
    }

    private void Start() {
        // you always load health in Start() and not in Awake
        Health = 50;
    }

    private void LoadSavedData() {
        Debug.Log(Health);
        if(Health == -1) {
            Debug.Log("will force default health");
            Health = startingHealth;
        }
        Debug.Log(Health);
    }

    private void OnDisable() {
        PlayerPrefsSave.Instance.SaveToDisk();
    }
    public string ID { get; set; }
    public void Save() {
        SaveManager.Instance.SaveToDisk(Health);
    }
    public void Load() {
        Health = SaveManager.Instance.LoadFromDisk<int>();
    }
    public void Register() {
        SaveManager.objectsToSave.Add(this);
    }
}
