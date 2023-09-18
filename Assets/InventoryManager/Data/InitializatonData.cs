using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "InitializationData", menuName = "Database/InitializationData")]
public class InitializatonData : ScriptableObject {
    public string sceneToLoad;
    public Object cutscene;
}
