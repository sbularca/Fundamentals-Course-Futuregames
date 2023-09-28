using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]

public class GameSettings : ScriptableObject {

    [Header("Scenes")]
    public string mainMenuSceneName;
    public List<string> gameLevelsNames;

    [Header("Other Settings")]
    public bool isGamePaused;
    public bool isLevelRuning;

}
