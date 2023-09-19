using Examples;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Color = System.Drawing.Color;

public class Executor : MonoBehaviour {

    [SerializeField] private int someNumbers;
    [SerializeField] private UnityEngine.Color color;
    [SerializeField] private AnimationCurve animationCurve;

    public int SomeNumbers => someNumbers;

    public GameMenuController gameMenuControllerPrefabss;

    private InputHandler inputHandler;
    private UIHandler uiHandler;

    private void Start() {
        inputHandler = new InputHandler();
        uiHandler = new UIHandler(inputHandler, gameMenuControllerPrefabss);

        inputHandler.Initialize();
        uiHandler.Initialize();
    }

    private void Update() {
        inputHandler.Tick();
        uiHandler.Tick();
    }

    private void OnDestroy() {
        inputHandler.Dispose();
        uiHandler.Dispose();
    }
}
