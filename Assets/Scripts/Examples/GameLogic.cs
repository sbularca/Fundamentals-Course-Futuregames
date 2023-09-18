using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogicData {
    public static Queue<GameLogicData> gameDataQueue = new Queue<GameLogicData>();
    public static UnityEvent<GameLogicData> gameDataEvent = new UnityEvent<GameLogicData>();
    public int currentHealth = 100;
}

public class GameLogic : MonoBehaviour {

    private GameLogicData gameLogicData;

    public void Initialize(GameLogicData gameLogicData) {
        if(gameLogicData == null) {
            this.gameLogicData = new GameLogicData();
        }
        this.gameLogicData = gameLogicData;
    }

    private void SetHealth(int health) {
        gameLogicData.currentHealth = health;
        GameLogicData.gameDataQueue.Enqueue(gameLogicData);
        GameLogicData.gameDataEvent.Invoke(gameLogicData);
        Logger.Log("Health is " + health);
    }
}

public class GameUI : MonoBehaviour{

    private void Start() {
        GameLogicData.gameDataEvent.AddListener(UpdateUI);
    }

    private void UpdateUI(GameLogicData gameLogicData) {
        var text = gameLogicData.currentHealth.ToString();
    }

    private void Update() {
        if(GameLogicData.gameDataQueue.Count > 0) {
            var gameData = GameLogicData.gameDataQueue.Dequeue();
            Logger.Log("message");
        }
    }
}
