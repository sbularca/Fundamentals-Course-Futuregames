using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressableLoader : MonoBehaviour {
    public AssetReference sceneToLoad;

    public AsyncOperationHandle<SceneInstance> handle;
    private List<AsyncOperationHandle<GameObject>> objectsHandles = new List<AsyncOperationHandle<GameObject>>();
    private GameObject cubeObject;

    private void Awake() {
        handle = Addressables.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        handle.Completed += OnLoadComplete;
    }

    //The scene loaded callback
    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> sceneInstance) {
        Debug.Log($"Scene {sceneInstance.Result.Scene.name} Loaded");
        SceneManager.SetActiveScene(sceneInstance.Result.Scene);

        LoadTheSceneObjects();
    }

    private void LoadTheSceneObjects() {
        var populateScene = FindObjectOfType<PopulateScene>();

        //Load the cube
        var cubeHandle = Addressables.LoadAssetAsync<GameObject>(populateScene.cubeReference);
        var sphereHandle = Addressables.InstantiateAsync(populateScene.sphereReference);
        var cylinderHandle = Addressables.InstantiateAsync(populateScene.cylinderReference);

        objectsHandles.Add(cubeHandle);
        objectsHandles.Add(sphereHandle);
        objectsHandles.Add(cylinderHandle);

        cubeHandle.Completed += (ophandle) => {
            var cube = ophandle.Result;
            cubeObject = Instantiate(cube);
        };
        populateScene.cleanMemoryButton.onClick.AddListener(Clean);
    }

    private void Clean() {
        //Release the scene
        //Addressables.Release(handle);
        //Release the objects
        foreach (var opHandle in objectsHandles) {
            if(opHandle.Result.name == "Cube") {
                Destroy(cubeObject);
            }
            Addressables.Release(opHandle);
        }

        //Unload the scene
        SceneManager.UnloadSceneAsync(handle.Result.Scene);
    }
}
