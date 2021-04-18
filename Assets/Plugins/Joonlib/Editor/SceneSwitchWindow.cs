// --------------------------------
// <copyright file="SceneSwitchWindow.cs" company="Rumor Games">
//     Copyright (C) Rumor Games, LLC.  All rights reserved.
// </copyright>
// --------------------------------

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// SceneSwitchWindow class.
/// </summary>
public class SceneSwitchWindow : EditorWindow
{
    /// <summary>
    /// Tracks scroll position.
    /// </summary>
    private Vector2 scrollPos;

    private string[] addrSceneHack = new string[] { "StartOfWorld",  "boot", "loading", "chapter1", "chapter2", "chapter3", "chapter4",
        "chapter6", "chapter7", "credits",
        "stripped/boot_STRIPPED", "stripped/chapter1_STRIPPED", "stripped/chapter2_STRIPPED", "stripped/chapter3_STRIPPED", "stripped/chapter4_STRIPPED",
        "stripped/chapter6_STRIPPED", "stripped/chapter7_STRIPPED", "stripped/credits_STRIPPED"
    };

    /// <summary>
    /// Initialize window state.
    /// </summary>
    [MenuItem("Tools/Scene Switch Window")]
    internal static void Init()
    {
        // EditorWindow.GetWindow() will return the open instance of the specified window or create a new
        // instance if it can't find one. The second parameter is a flag for creating the window as a
        // Utility window; Utility windows cannot be docked like the Scene and Game view windows.
        var window = (SceneSwitchWindow)GetWindow(typeof(SceneSwitchWindow), false, "Scene Switch");
        window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
    }

    /// <summary>
    /// Called on GUI events.
    /// </summary>
    internal void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);

        //GUILayout.Label("Scenes In Build", EditorStyles.boldLabel);
        //for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
        for(int i = 0; i < addrSceneHack.Length; i++)
        {
            //var scene = EditorBuildSettings.scenes[i];
            var scene = addrSceneHack[i];
            //if (scene.enabled)
            {
                EditorGUILayout.BeginHorizontal();

                //var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                var sceneName = scene;
                string scenePath = "Assets/_Assets/Scenes/" + scene + ".unity";
                var pressed = GUILayout.Button(i + ": " + sceneName, new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft }, GUILayout.Width(EditorGUIUtility.currentViewWidth -30));
                var pressedAdditive = GUILayout.Button("+", new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft }, GUILayout.Width(18));
                if (pressed)
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        //EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
                        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                    }
                }

                if (pressedAdditive)
                {
                    //EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Additive);
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}