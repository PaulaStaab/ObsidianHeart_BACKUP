//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GamePauseHandler : MonoBehaviour
//{
//    [SerializeField] private string PauseMenu = "PauseMenu";
//    private bool gameIsFrozen = false;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.P))
//        {
//            TogglePauseState();
//        }
//    }

//    private void TogglePauseState()
//    {
//        if (!gameIsFrozen)
//        {
//            ShowPauseMenu();
//        }
//        else
//        {
//            HidePauseMenu();
//        }
//    }

//    private void ShowPauseMenu()
//    {
//        SceneManager.LoadScene(PauseMenu, LoadSceneMode.Additive);
//        Time.timeScale = 0f;
//        Cursor.visible = true;
//        Cursor.lockState = CursorLockMode.None;
//        gameIsFrozen = true;
//    }

//    private void HidePauseMenu()
//    {
//        SceneManager.UnloadSceneAsync(PauseMenu);
//        Time.timeScale = 1f;
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        gameIsFrozen = false;
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GamePauseHandler : MonoBehaviour
{
    [SerializeField] private string PauseMenu = "PauseMenu";
    private bool gameIsFrozen = false;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
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

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

//public class GamePauseHandler : MonoBehaviour
//{
//    [SerializeField] private string pauseMenuSceneName = "PauseMenu";
//    private bool gameIsFrozen = false;

//    void Start()
//    {
//        Time.timeScale = 1f;
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
//        {
//            TogglePauseState();
//        }
//    }

//    public void TogglePauseState()
//    {
//        if (!gameIsFrozen)
//        {
//            PauseGame();
//        }
//        else
//        {
//            ResumeGame();
//        }
//    }

//    public void PauseGame()
//    {
//        if (gameIsFrozen)
//            return;

//        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
//        Time.timeScale = 0f;
//        Cursor.visible = true;
//        Cursor.lockState = CursorLockMode.None;
//        gameIsFrozen = true;
//    }

//    public void ResumeGame()
//    {
//        if (!gameIsFrozen)
//            return;

//        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
//        Time.timeScale = 1f;
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        gameIsFrozen = false;
//    }

//    public void ContinueButton()
//    {
//        ResumeGame();
//    }
//}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

//public class GamePauseHandler : MonoBehaviour
//{
//    [SerializeField] private string pauseMenuSceneName = "PauseMenu";
//    private bool gameIsFrozen = false;

//    void Start()
//    {
//        ResumeGameVisuals();
//    }

//    void Update()
//    {
//        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
//        {
//            TogglePauseState();
//        }
//    }

//    public void TogglePauseState()
//    {
//        if (gameIsFrozen)
//            ResumeGame();
//        else
//            PauseGame();
//    }

//    public void PauseGame()
//    {
//        if (gameIsFrozen)
//            return;

//        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
//        Time.timeScale = 0f;
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//        gameIsFrozen = true;
//    }

//    public void ResumeGame()
//    {
//        if (!gameIsFrozen)
//            return;

//        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
//        Time.timeScale = 1f;
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//        gameIsFrozen = false;
//    }

//    public void ContinueButton()
//    {
//        ResumeGame();
//    }

//    private void ResumeGameVisuals()
//    {
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }
//}

