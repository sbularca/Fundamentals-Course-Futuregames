using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTest : MonoBehaviour {

    public GameObject player;
    private void OnEnable() {
        Debug.Log("I have been enabled and will register");
    }


}
