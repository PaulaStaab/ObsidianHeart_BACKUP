using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Loads scenes asynchronously by name.
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string GameScene)
    {
        // Start the asynchronous scene loading process.
        StartCoroutine(LoadSceneAsync(GameScene));
    }

    private IEnumerator LoadSceneAsync(string GameScene)
    {
        // Request asynchronous loading of the target scene.
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameScene);

        // Stop if Unity could not create a valid load operation.
        if (operation == null)
        {
            Debug.LogError("Scene '" + GameScene + "' could not be loaded.");
            yield break;
        }

        // Wait until the scene loading process has finished.
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}