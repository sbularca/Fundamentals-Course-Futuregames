using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using File = System.IO.File;

public class SaveManager : MonoBehaviour {

    public static string savePath = $"{Application.dataPath}/SavedGames/save.json";
    public static SaveManager Instance { get; private set; }

    public static List<ISaveable> objectsToSave = new List<ISaveable>();

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(Instance);
            return;
        }
        Destroy(this);
    }

    public void SaveAllData() {
        for(int i = 0; i < objectsToSave.Count; i++) {
            objectsToSave[i].Save();
        }
    }

    public void LoadAllData() {
        // objects to save should be converted in a dictionary that takes the ID as a key and the object as data
        // The when loading, the load data should be calsled based on the ID. The ID should probably be the instance ID of that object which is unique
    }

    public void SaveToDisk<T>(T data) {
        var converted = JsonUtility.ToJson(data);
        if (File.Exists(savePath)) {
            File.WriteAllText(savePath, converted);
            return;
        }
        File.Create(savePath);
        SaveToDisk(data);
    }

    public T LoadFromDisk<T>() {
        object value = "";
        if(!File.Exists(savePath)) {
            Debug.Log("File does not exist");
            return (T)value;
        }
        T data = JsonUtility.FromJson<T>(savePath);
        return data;
    }
}
