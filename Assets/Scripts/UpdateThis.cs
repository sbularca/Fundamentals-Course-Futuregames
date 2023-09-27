using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UpdateThis : MonoBehaviour {

    public int level;
    public GameLogicData gameLogicData;
    public InitMonobehaviour initMonobehaviour;
    private List<string> logMesages = new ();

    private void Start() {
        Debug.Assert(initMonobehaviour != null);
        Debug.Log("message");
        Application.logMessageReceived += ApplicationOnlogMessageReceived;
        Assert.IsNotNull(initMonobehaviour);
    }
    private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type) {
        logMesages.Add(stacktrace);
        Debug.Log($"This is our message: condition: {condition}, \n stacktrace {stacktrace}, \n type {type}");
    }
    private void Update() {
        if(level >= 100) { // this is a bug
            return;
        }

        level++;

        if(level == 100) {
            Debug.Log("Maximum level reached");
        }

        //gameLogicData.currentHealth = 5;
    }

    private void OnDestroy() {
        // NetworkService.Upload(logMessages)
    }
}
