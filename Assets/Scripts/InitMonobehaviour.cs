using UnityEngine;

public class InitMonobehaviour : MonoBehaviour{

    [SerializeField] private GameState gameStatePrefab;

    [SerializeField] private int variableName;
    // Standard way of initializing the game
    private void Awake() {
        // initialize the network service
        LoadeNetworkService();

        //intialize the game state
        LoadGameState();

        // intialize Scene Manager

        // initialize Input Manager

        //initialize UI
        LoaderService();
    }

    private void LoadGameState() {
            var gameState = Instantiate(gameStatePrefab);
        gameState.Initialize();
    }
    private void LoadeNetworkService() {

    }
    private void LoaderService() {
    }
}
