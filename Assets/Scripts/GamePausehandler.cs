using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseHandler : MonoBehaviour
{
    [SerializeField] private string PauseMenu = "PauseMenu";
    private bool gameIsFrozen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseState();
        }
    }

    private void TogglePauseState()
    {
        if (!gameIsFrozen)
        {
            ShowPauseMenu();
        }
        else
        {
            HidePauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        SceneManager.LoadScene(PauseMenu, LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.visible = true;     
        Cursor.lockState = CursorLockMode.None;
        gameIsFrozen = true;
    }

    private void HidePauseMenu()
    {
        SceneManager.UnloadSceneAsync(PauseMenu);
        Time.timeScale = 1f;
        Cursor.visible = false;   
        Cursor.lockState = CursorLockMode.Locked;  
        gameIsFrozen = false;
    }
}
