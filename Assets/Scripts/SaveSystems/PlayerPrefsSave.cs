using System;
using UnityEngine;

public class PlayerPrefsSave : MonoBehaviour {
    public static PlayerPrefsSave Instance { get; private set; }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this);
    }

    /// <summary>
    ///  Saves a value of type T from the Player Prefs
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    public void Save<T>(string key, T value) {
        object obj = value;
        if(typeof(T) == typeof(int)) {
            PlayerPrefs.SetInt(key, (int)obj);
            return;
        }
        if(typeof(T) == typeof(string)) {
            PlayerPrefs.SetString(key, (string)obj);
            return;
        }
        if(typeof(T) == typeof(float)) {
            PlayerPrefs.SetFloat(key, (float)obj);
            return;
        }

        Debug.LogError("You have specified the wrong type");
    }

    /// <summary>
    /// Loads a value of type T from the Player Prefs
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Load<T>(string key) {
        object obj = null;
        if(PlayerPrefs.HasKey(key)) {
            if(typeof(T) == typeof(int)) {
                obj = PlayerPrefs.GetInt(key);
            }
            if(typeof(T) == typeof(string)) {
                obj = PlayerPrefs.GetString(key);
            }
            if(typeof(T) == typeof(float)) {
                obj = PlayerPrefs.GetFloat(key);
            }
        }
        if(obj == null) {
            Debug.LogError("You have specified the wrong type");
        }
        return (T)obj;
    }

    public void SaveToDisk() {
        PlayerPrefs.Save();
    }

    public void DeleteKey(string key) {
        PlayerPrefs.DeleteKey(key);
    }

    public void ClearSavedData() {
        PlayerPrefs.DeleteAll();
    }

    private void OnDestroy() {
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}
