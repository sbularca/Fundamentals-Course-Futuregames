using DefaultNamespace;
using UnityEngine;

public class InitNoScene {
    private GameSettings gameSettings;
    public static MyLogger myLogger = new ();

    [RuntimeInitializeOnLoadMethod]
    public static void InitGame() {

        myLogger.Log("message");
        // using resource load
        // var gameStatePrefab = Resources.Load<GameState>("GameState");
        // var gameState = GameObject.Instantiate(gameStatePrefab);
        // gameState.Initialize();

        // create the actual game object and add component from code
        // var go = new GameObject();
        // go.name = "GameState";
        // var gameState = go.AddComponent<GameState>();
        // gameState.Initialize();
    }
}
