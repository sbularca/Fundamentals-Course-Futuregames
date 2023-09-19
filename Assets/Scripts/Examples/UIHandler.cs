using Examples;
using UnityEngine;
public class UIHandler : IInitialize, ITick, IDisposable {
    private readonly InputHandler inputHandler;
    private readonly GameMenuController uiPrefab;

    public UIHandler(InputHandler inputHandler, GameMenuController uiPrefab) {
        this.inputHandler = inputHandler;
        this.uiPrefab = uiPrefab;
    }

    public void Initialize() {
        var instantiated = Object.Instantiate(uiPrefab);
    }

    public void Tick() {
        if(inputHandler.grabbed) {
            //do something
        }
    }

    public void Dispose() {
    }
}
