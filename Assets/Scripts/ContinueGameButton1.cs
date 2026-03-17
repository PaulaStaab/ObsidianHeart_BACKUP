using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ContinueGameButton1 : MonoBehaviour
{
    public void ResumeAndCleanup()
    {
        StartCoroutine(ResumeCleanup());
    }

    private IEnumerator ResumeCleanup()
    {
        // Szenen aufräumen
        SceneManager.UnloadSceneAsync(gameObject.scene);

        // Spiel fortsetzen
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        yield return null;
    }
}
