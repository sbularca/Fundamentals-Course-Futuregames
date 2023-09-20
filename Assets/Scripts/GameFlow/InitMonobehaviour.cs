using System;
using UnityEngine;

public class InitMonobehaviour : MonoBehaviour {
    public GameState gameStatePrefab;

    private void Awake() {
        var gameState = Instantiate(gameStatePrefab);
        gameState.Initialize();
        // here you can initialize other services as well knowing that your game state is now active
    }
}
