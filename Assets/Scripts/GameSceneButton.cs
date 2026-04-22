using UnityEngine;
using UnityEngine.SceneManagement;

// Loads the configured game scene when triggered by a UI button or event.
public class GameSceneButton : MonoBehaviour
{
    // Name of the scene that should be loaded.
    [SerializeField] private string gameSceneName = "GameScene";

    public void LoadTargetScene()
    {
        // Load the target scene by name.
        SceneManager.LoadScene(gameSceneName);
    }
}