// Rasmus Rönnqvist, Game Programmer, Futuregames Boden, 2023

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

/* Explanation:
A Unity Tool used for moving GameObjects (not Camera, Volume, or Light) to a
random position, scale, or rotation. The amplitude, i.e. amount, of this
randomization can be set through the Editor Tools GUI by the Editor Window.
*/

/* TODO:
Add? Some more logic for changing / adding scenes might be necessary, have not
tested it thoroughly.

Increase performance of "animation loop". Looping through a dictionary every
frame just to manipulate the object a tiny bit is very wasteful.
I would like it to be a coroutine or something that goes of and does it the whole
lerp on it's own. No loop through the dictionary every frame and adding +0.05f to
the lerp, and then loops. But, I am noob. Coroutines in Editor requires package
and I do not know how to do some other async thing.

I intentionally did not pre-maturely optimize this code. I have to run some
performance diagnostics before I decide what to optimize.
*/

[EditorTool("Scramble Tool")]
class ScrambleTool : EditorTool //, IDrawSelectedHandles
{
    struct TransformData
    {
        public Vector3 localPosition;
        public Vector3 localScale;
        public Quaternion localRotation;
    }

    // 0 = Do not change the value
    private float positionAmplitude = 0;
    private float scaleAmplitude = 0;
    private float rotationAmplitude = 0;

    private float lerpTime = 0.0f;

    // Transform will be a reference, it will contain current values.
    private Dictionary<Transform, TransformData> oldTransformData
        = new Dictionary<Transform, TransformData>();

    // For animation. End point of the movement
    private Dictionary<Transform, TransformData> endpointTransformData
        = new Dictionary<Transform, TransformData>();

    // Global tools (like this one, not component tools) are initialized and
    // persisted by a Tool Manager.
    private void OnEnable()
    {
        Debug.Log("Tool enabled");

        // Delegates!
        EditorSceneManager.sceneOpened += OnSceneOpened;
        EditorSceneManager.sceneClosed += OnSceneClosed;
        EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChanged;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDisable()
    {
        Debug.Log("Tool disabled");

        EditorSceneManager.sceneOpened -= OnSceneOpened;
        EditorSceneManager.sceneClosed -= OnSceneClosed;

        EditorSceneManager.activeSceneChangedInEditMode -= OnActiveSceneChanged;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }


    public override void OnToolGUI(EditorWindow window)
    {
        if (window is not SceneView sceneView)
            return;

        Handles.BeginGUI();
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button("Scramble!"))
                {
                    Debug.Log("Scramble button!");
                    Scramble();
                }

                if (GUILayout.Button("Reset"))
                {
                    Debug.Log("Reset button");


                    /* Reset Transforms */
                    foreach (var entry in oldTransformData)
                    {
                        // Assume the object is deleted and remove it from the dictionary
                        if (entry.Key == null)
                        {
                            oldTransformData.Remove(entry.Key);
                            continue; // Just skip current iteration, do not return from whole function or break loop
                        }

                        Transform t = entry.Key;

                        // Set End Point to the Old Position
                        var oldData = oldTransformData[t];
                        var endpointData = new TransformData
                        {
                            localPosition = oldData.localPosition,
                            localScale = oldData.localScale,
                            localRotation = oldData.localRotation
                        };
                        endpointTransformData.Remove(t);
                        endpointTransformData.Add(t, endpointData);
                    }

                    lerpTime = 0; // Tells OnToolGUI there is stuff to lerp
                }

                /* Sliders */
                GUILayout.Label("Position Amplitude");
                positionAmplitude =
                    GUILayout.HorizontalSlider(positionAmplitude, 0.0f, 3.0f);
                GUILayout.Label(""); // Empty space

                GUILayout.Label("Scale Amplitude");
                scaleAmplitude =
                    GUILayout.HorizontalSlider(scaleAmplitude, 0.0f, 1.0f);
                GUILayout.Label(""); // Empty space

                GUILayout.Label("Rotation Amplitude");
                rotationAmplitude =
                    GUILayout.HorizontalSlider(rotationAmplitude, 0.0f, 4.0f);
                GUILayout.Label(""); // Empty space
            }

            GUILayout.FlexibleSpace();
        }

        Handles.EndGUI();


        /* Animations */

        // Update only if there is something left to lerp (animate)
        if (lerpTime > 1) return;

        // Don't start moving things around if it will not show
        if (Event.current.type is not EventType.Repaint) return;

        // Enable animations
        sceneView.sceneViewState.alwaysRefresh = true;
        if (!sceneView.sceneViewState.fxEnabled)
            sceneView.sceneViewState.fxEnabled = true;

        /* TODO: Increase performance
           Loop like this every frame is performance heavy. Coroutines? */
        foreach (var entry in endpointTransformData)
        {
            // If missing, assume the object is deleted and remove it from the dictionary
            if (entry.Key == null)
            {
                oldTransformData.Remove(entry.Key);
                continue; // continue. Do not break loop or return from function
            }

            Transform t = entry.Key;
            TransformData endData = entry.Value;

            // Animate! Manipulate position, scale and rotation.
            t.localPosition = Vector3.Lerp(
                t.localPosition, endData.localPosition, lerpTime);

            t.localScale = Vector3.Lerp(
                t.localScale, endData.localScale, lerpTime);

            t.localRotation = Quaternion.Slerp(
                t.localRotation, endData.localRotation, lerpTime);
        }

        // TODO: Hook this lerpTime to GUI?
        lerpTime += 0.05f; // Amount to lerp per frame. "Speed". 0.05 = 5% per frame
    }

    private void Scramble() // TODO: Rename me!
    {
        if (positionAmplitude == 0 &&
            scaleAmplitude    == 0 &&
            rotationAmplitude == 0)
        {
            Debug.LogWarning(
                "ScrambleTool: All scramble amplitudes are set to 0, no scrambling will be done.");
            return; // Return function void. Do not continue function
        }

        // Check if there is any new object to add
        var foundObjects = FindObjectsOfType<Transform>();
        foreach (var t in foundObjects)
        {
            if (oldTransformData.ContainsKey(t) ||
                t.gameObject.GetComponent<Camera>() != null ||
                /*t.GetComponent<Volume>()            != null ||*/
                t.GetComponent<Light>()             != null) continue;

            // SEBASTIAN, HELP! What is the lifetime of this struct? Where is it?
            // I was afraid there would only be 1 or it would be discarded once
            // we left the scope of Start. But it just works? Magic??

            // Add data for the newly found Transform to dictionary
            var data = new TransformData
            {
                localPosition = t.localPosition,
                localScale = t.localScale,
                localRotation = t.localRotation
            };
            oldTransformData.Add(t, data);
        }

        /* Scramble objects */
        foreach (var entry in oldTransformData)
        {
            if (entry.Key == null)
            {
                oldTransformData.Remove(entry.Key);
                continue; // Continue looping, just skip current iteration
            }

            var t = entry.Key; // Target transform to change

            TransformData endpointData = new TransformData(); // For movement

            // Populate endpointData with random positions modified by amplitude
            var oldPos = oldTransformData[t].localPosition;
            endpointData.localPosition = new Vector3(
                    Random.Range(
                        oldPos.x - positionAmplitude,
                        oldPos.x + positionAmplitude),
                    Random.Range(
                        oldPos.y - positionAmplitude,
                        oldPos.y + positionAmplitude),
                    Random.Range(
                        oldPos.z - positionAmplitude,
                        oldPos.z + positionAmplitude));

            var oldScale = oldTransformData[t].localScale;
            endpointData.localScale = new Vector3(
                    Random.Range(oldScale.x, oldScale.y + scaleAmplitude),
                    Random.Range(oldScale.y, oldScale.y + scaleAmplitude),
                    Random.Range(oldScale.z, oldScale.z + scaleAmplitude));

            var oldRot = oldTransformData[t].localRotation.eulerAngles;
            endpointData.localRotation = quaternion.Euler(
                    Random.Range(
                        oldRot.x - rotationAmplitude,
                        oldRot.x + rotationAmplitude),
                    Random.Range(
                        oldRot.y - rotationAmplitude,
                        oldRot.y + rotationAmplitude),
                    Random.Range(
                        oldRot.z - rotationAmplitude,
                        oldRot.z + rotationAmplitude));

            // Update data entry
            endpointTransformData.Remove(t);
            endpointTransformData.Add(t, endpointData);
        }

        lerpTime = 0; // Tells OnToolGUI there is stuff to lerp
    }

    void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        Debug.Log("Opened: " + scene.name);
    }

    void OnSceneClosed(Scene scene)
    {
        Debug.Log("Closed: " + scene.name);
    }

    void OnActiveSceneChanged(Scene current, Scene next)
    {
        Debug.Log("Active Scene Changed!");
        // Empty the dictionary, Resetting it
        oldTransformData.Clear();
        endpointTransformData.Clear();
    }
}
