using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneButton : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
