using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public static GameState instance;
    public GameSettings gameSettings;

    public void Initialize() {
        if(instance == null) {
            instance = this;
            // this insure the object will remain persistent between scene loads
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadSettings();
            LoadMainMenu();

            return;
        }

        //makes sure we have only one instance of this in the game
        Destroy(gameObject);
    }

    private void LoadSettings() {
        gameSettings = Resources.Load<GameSettings>("GameSettings");
    }

    private void LoadMainMenu() {
        //load the menu scene here, for example
        SceneManager.LoadSceneAsync(gameSettings.mainMenuSceneName, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        if (scene.name == gameSettings.mainMenuSceneName) {
            Debug.Log("Game Started. Main menu Scene loaded");
        }
    }
}
