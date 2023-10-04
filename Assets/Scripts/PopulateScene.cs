using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class PopulateScene : MonoBehaviour {
    [Header("Asset references")]
    public AssetReferenceT<GameObject> cubeReference;
    public AssetReference sphereReference;
    public AssetReferenceT<GameObject> cylinderReference;

    [Header("UI references")]
    public Button cleanMemoryButton;
}
