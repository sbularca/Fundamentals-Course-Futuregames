using UnityEngine;

public class InitNoScene {
    private GameSettings gameSettings;

    [RuntimeInitializeOnLoadMethod]
    public static void InitGame() {
        // using resource load
        var gameStatePrefab = Resources.Load<GameState>("GameState");
        var gameState = GameObject.Instantiate(gameStatePrefab);
        gameState.Initialize();

        // create the actual game object and add component from code
        // var go = new GameObject();
        // go.name = "GameState";
        // var gameState = go.AddComponent<GameState>();
        // gameState.Initialize();
    }
}
