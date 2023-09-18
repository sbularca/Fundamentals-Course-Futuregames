using UnityEngine;

public static class Logger {
    public static void Log(string message) {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }
}
