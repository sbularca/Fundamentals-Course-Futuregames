using UnityEditor;
using UnityEngine;

public class InitScript {

    [RuntimeInitializeOnLoadMethod]
    private static void InitGame() {
        var go = new GameObject();
        go.name = "GameState";
        var gameState = go.AddComponent<GameState>();
        gameState.Initialize();
    }
}
