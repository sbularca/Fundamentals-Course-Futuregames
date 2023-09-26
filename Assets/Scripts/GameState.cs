using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// game state service
public class GameState : MonoBehaviour {
    public static GameState instance;
    private GameSettings gameSettings;

    public void Initialize() {
        if(instance == null) {
            instance = this;
            // in order to keep the object persistent, we add the following line
            DontDestroyOnLoad(gameObject);
            LogGameState("Game Initialized");

            //we proceed with other initializations here
            LoadSettings();
            LoadMainMenu();
            return;
        }
        LogGameState("Duplicate instance instantiated, destroying...");
        Destroy(gameObject);

    }

    private void Awake() {
        // we subscribe to this default unity event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LoadSettings() {
        // we load a scriptable object containing more game settings like what scenes should be loaded next, etc
        // game state can give access to this instance to other services as well if necesary (or InitGame can do the same thing)
        gameSettings = Resources.Load<GameSettings>("GameSettings");
    }


    private void LoadMainMenu() {
        //load the menu scene here, for example
        SceneManager.LoadSceneAsync(gameSettings.mainMenuSceneName, LoadSceneMode.Single);
    }

    // this execute when a scene is laoded if subscribed - SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        if (scene.name == gameSettings.mainMenuSceneName) {
            Debug.Log("Game Started. Main menu Scene loaded");
        }
    }

    private void LogGameState(string message) {
        Debug.Log(message);
    }
}
