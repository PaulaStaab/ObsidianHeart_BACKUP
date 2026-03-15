using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string GameScene)
    {
        StartCoroutine(LoadSceneAsync(GameScene));
    }

    private IEnumerator LoadSceneAsync(string GameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameScene);

        if (operation == null)
        {
            Debug.LogError("Scene '" + GameScene + "' could not be loaded.");
            yield break;
        }

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
