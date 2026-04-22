//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditor.SceneManagement;
//using UnityEngine;

//[InitializeOnLoad]
//public static class PlayFromStartScene
//{
//    static PlayFromStartScene()
//    {
//        string startScenePath = "Assets/Scenes/StartScreen.unity"; // Path to the scene that should always be loaded on Play

//        SceneAsset startScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath); // Load the scene asset from the given path

//        if (startScene != null)
//        {
//            EditorSceneManager.playModeStartScene = startScene; // Set the scene as the default Play Mode start scene
//        }
//        else
//        {
//            Debug.LogWarning("Start scene not found at: " + startScenePath); // Warning if the scene path is wrong
//        }
//    }
//}
//#endif