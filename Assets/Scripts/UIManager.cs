using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour {
    public UICanvasRefrences uiCanvasRefrencesPrefab;
    public UICanvasRefrences uiCanvasRefrences;

    private Transform player;

    public UIManager() {
        Debug.Log("UIManager constructor called");
    }
    public void Initialize(Transform player) {
        this.player = player;
        uiCanvasRefrences = Instantiate(uiCanvasRefrencesPrefab);
        gameObject.SetActive(uiCanvasRefrences.gameObject);
        Assert.IsNull(uiCanvasRefrences);

        uiCanvasRefrences.button1.onClick.AddListener(() => {
            Debug.Log("Button 1 was clicked");
        });
    }

    private void OnDisable() {
        Destroy(player.gameObject);
    }
}
