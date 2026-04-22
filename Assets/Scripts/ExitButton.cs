using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Exits play mode in the Unity Editor or closes the application in a build.
public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Stop Play Mode when running inside the Unity Editor.
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a standalone build.
        Application.Quit();
#endif
    }
}