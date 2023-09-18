using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class Init : MonoBehaviour {
    [SerializeField] private InitializatonData initializatonData;
    private void Awake() {
        SceneManager.LoadScene(initializatonData.sceneToLoad);
    }
}
