using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

/// <summary>
/// Use this class to create a request for loading an asset
/// </summary>
/// <typeparam name="T"></typeparam>
public class AssetRequest<T> {
    public T result;
    public AsyncOperationHandle handle;
}

/// <summary>
/// Use this class to create a request for loading a scene
/// </summary>
public class SceneRequest {
    public LoadSceneMode loadSceneMode;
    public bool setActiveOnLoad;
    public AsyncOperationHandle handle;
    public SceneInstance sceneInstance;
}

public static class AddressablesUtility {
    private static readonly List<AsyncOperationHandle> currentActiveHandlers = new();

    /// <summary>
    /// Loads and asset by a string ID/Name/key, which is specified in the AddressablesGroups
    /// </summary>
    public static async void LoadAsset<T>(string key, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        await LoadAssetAsync<T>(key, request, callback, priority);
    }

    /// <summary>
    /// Loads and asset by an Asset Reference of type T which can be referenced in a component if it exists in the Addressables/Bundles Groups
    /// </summary>
    public static async void LoadAsset<T>(AssetReferenceT<T> assetReferenceT, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        await LoadAssetAsync<T>(assetReferenceT, request, callback, priority);
    }

    /// <summary>
    /// Loads and asset by a standard type of Asset Reference which can be referenced in a component if it exists in the Addressables/Bundles Groups
    /// </summary>
    public static async void LoadAsset<T>(AssetReference assetReference, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        await LoadAssetAsync(assetReference, request, callback, priority);
    }

    /// <summary>
    /// Loads a scene async
    /// </summary>
    public static async void LoadScene(AssetReference assetReference, SceneRequest request, Action callback, ThreadPriority priority = ThreadPriority.Normal) {
        await LoadSceneAsync(assetReference, request, callback, priority);
    }

    /// <summary>
    /// Checks if the specific key exists in the Addressable bundles/Groups
    /// </summary>
    public static async void GetKeyExists(string id, AssetRequest<bool> request) {
        await GetKeyExistAsync(id, request);
    }

    /// <summary>
    /// Release the handle and unloads the resource from memory
    /// </summary>
    public static void DisposeAsset(AsyncOperationHandle handle) {
        if(currentActiveHandlers.Contains(handle)) {
            Addressables.Release(handle);
            currentActiveHandlers.Remove(handle);
            return;
        }
        Debug.LogError("[AddressableHelper] No such handle available..");
    }

    /// <summary>
    /// TASK that loads and asset by a string ID/Name/key, which is specified in the AddressablesGroups
    /// </summary>
    public static async Task LoadAssetAsync<T>(string key, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        var hasKey = new AssetRequest<bool>();
        await GetKeyExistAsync(key, hasKey);
        if(!hasKey.result) {
            return;
        }
        var currentPriority = Application.backgroundLoadingPriority;
        SetThreadPriority(priority);
        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogError($"[AddressableHelper] Failed to load {key} : {handle.OperationException.Message}");
            SetThreadPriority(currentPriority);
            return;
        }

        currentActiveHandlers.Add(handle);
        request.result = handle.Result;
        request.handle = handle;
        SetThreadPriority(currentPriority);
        callback?.Invoke(request);
    }

    /// <summary>
    /// TASK that loads and asset by an Asset Reference of type T which can be referenced in a component if it exists in the Addressables/Bundles Groups
    /// </summary>
    public static async Task LoadAssetAsync<T>(AssetReferenceT<T> assetReferenceT, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        if(assetReferenceT == null) {
            Debug.LogError($"[AddressableHelper] Missing the asset reference...");
            return;
        }
        var currentPriority = Application.backgroundLoadingPriority;
        SetThreadPriority(priority);
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReferenceT);
        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogError($"[AddressableHelper] Failed to load {assetReferenceT.AssetGUID} : {handle.OperationException.Message}");
            SetThreadPriority(currentPriority);
            return;
        }
        currentActiveHandlers.Add(handle);
        request.result = handle.Result;
        request.handle = handle;
        SetThreadPriority(currentPriority);
        callback?.Invoke(request);
    }

    /// <summary>
    /// TASK that loads and asset by a standard type of Asset Reference which can be referenced in a component if it exists in the Addressables/Bundles Groups
    /// </summary>
    public static async Task LoadAssetAsync<T>(AssetReference assetReference, AssetRequest<T> request, Action<AssetRequest<T>> callback = null, ThreadPriority priority = ThreadPriority.Normal) where T : Object {
        if(assetReference == null) {
            Debug.LogError($"[AddressableHelper] Missing the asset reference...");
            return;
        }
        var currentPriority = Application.backgroundLoadingPriority;
        SetThreadPriority(priority);
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogError($"[AddressableHelper] Failed to load {assetReference.AssetGUID} : {handle.OperationException.Message}");
            SetThreadPriority(currentPriority);
            return;
        }
        currentActiveHandlers.Add(handle);
        request.result = handle.Result;
        request.handle = handle;
        SetThreadPriority(currentPriority);
        callback?.Invoke(request);
    }

    /// <summary>
    /// TASK that loads a scene async
    /// </summary>
    public static async Task LoadSceneAsync(AssetReference sceneReference, SceneRequest request, Action callback = null, ThreadPriority priority = ThreadPriority.Normal) {
        if(sceneReference == null) {
            Debug.LogError($"[AddressableHelper] Missing the scene reference...");
        }
        var currentPriority = Application.backgroundLoadingPriority;
        SetThreadPriority(priority);
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference, request.loadSceneMode, request.setActiveOnLoad);
        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogError($"[AddressableHelper] Failed to load {sceneReference.AssetGUID} : {handle.OperationException.Message}");
            SetThreadPriority(currentPriority);
            return;
        }

        currentActiveHandlers.Add(handle);
        request.handle = handle;
        request.sceneInstance = handle.Result;
        SetThreadPriority(currentPriority);
        callback?.Invoke();
    }

    /// <summary>
    /// TASK that checks if the specific key exists in the Addressable bundles/Groups
    /// </summary>
    public static async Task GetKeyExistAsync(string id, AssetRequest<bool> request) {
        var asyncKeyExists = Addressables.LoadResourceLocationsAsync(id);
        await asyncKeyExists.Task;

        if(asyncKeyExists.Status == AsyncOperationStatus.Failed) {
            Debug.LogWarning($"[AddressableUtility] Failed to find {id} : {asyncKeyExists.OperationException.Message}");
            return;
        }
        request.result = asyncKeyExists.Result.Count > 0;
    }
    
    private static void SetThreadPriority(ThreadPriority priority) {
        Application.backgroundLoadingPriority = priority;
    }
}