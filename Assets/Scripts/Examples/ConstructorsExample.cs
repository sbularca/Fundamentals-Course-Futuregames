using Examples;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorsExample : MonoBehaviour {
    public int nrOfLives = 3;

    private static string test = "test";
    private void Start() {
        DataClass dataClass = new DataClass(nrOfLives);

        //dataClass.CurrentNrOfLives = 12000;
#if SEB_QUEST
        Debug.Log(dataClass.CurrentNrOfLives);
#endif
        dataClass.NumberOfLives = 3;
        dataClass.SetNumberOfLives(3);

        var nrLives = dataClass.NumberOfLives;
        nrLives = dataClass.GetNrOfLives();
    }

    public static void DoSomething() {
        Debug.Log(test);
        // do something
    }
}
