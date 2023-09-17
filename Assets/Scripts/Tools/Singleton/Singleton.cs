using UnityEngine;

/// <summary>
/// Will not persist between secenes
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour
    where T : Component {
    private static T instance;

    public static T Instance {
        get {
            if(instance == null) {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if(objs.Length > 0)
                    instance = objs[0];
                if(objs.Length > 1) {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if(instance != null) {
                    return instance;
                }
                GameObject obj = new() {
                    hideFlags = HideFlags.HideAndDontSave
                };
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }
}

/// <summary>
/// Will persist between scenes. Call base.Awake() in Awake() method of derived class
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonPersistent<T> : MonoBehaviour
    where T : Component {
    public static T Instance { get; private set; }

    public virtual void Awake() {
        if(Instance == null) {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }
}
