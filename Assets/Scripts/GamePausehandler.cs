//////////using UnityEngine;
//////////using UnityEngine.SceneManagement;

//////////public class GamePauseHandler : MonoBehaviour
//////////{
//////////    [SerializeField] private string PauseMenu = "PauseMenu";
//////////    private bool gameIsFrozen = false;

//////////    void Update()
//////////    {
//////////        if (Input.GetKeyDown(KeyCode.P))
//////////        {
//////////            TogglePauseState();
//////////        }
//////////    }

//////////    private void TogglePauseState()
//////////    {
//////////        if (!gameIsFrozen)
//////////        {
//////////            ShowPauseMenu();
//////////        }
//////////        else
//////////        {
//////////            HidePauseMenu();
//////////        }
//////////    }

//////////    private void ShowPauseMenu()
//////////    {
//////////        SceneManager.LoadScene(PauseMenu, LoadSceneMode.Additive);
//////////        Time.timeScale = 0f;
//////////        Cursor.visible = true;
//////////        Cursor.lockState = CursorLockMode.None;
//////////        gameIsFrozen = true;
//////////    }

//////////    private void HidePauseMenu()
//////////    {
//////////        SceneManager.UnloadSceneAsync(PauseMenu);
//////////        Time.timeScale = 1f;
//////////        Cursor.visible = false;
//////////        Cursor.lockState = CursorLockMode.Locked;
//////////        gameIsFrozen = false;
//////////    }
//////////}

////////using UnityEngine;
////////using UnityEngine.SceneManagement;
////////using UnityEngine.InputSystem;

////////public class GamePauseHandler : MonoBehaviour
////////{
////////    [SerializeField] private string PauseMenu = "PauseMenu";
////////    private bool gameIsFrozen = false;

////////    void Update()
////////    {
////////        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
////////        {
////////            TogglePauseState();
////////        }
////////    }

////////    private void TogglePauseState()
////////    {
////////        if (!gameIsFrozen)
////////        {
////////            ShowPauseMenu();
////////        }
////////        else
////////        {
////////            HidePauseMenu();
////////        }
////////    }

////////    private void ShowPauseMenu()
////////    {
////////        SceneManager.LoadScene(PauseMenu, LoadSceneMode.Additive);
////////        Time.timeScale = 0f;
////////        Cursor.visible = true;
////////        Cursor.lockState = CursorLockMode.None;
////////        gameIsFrozen = true;
////////    }

////////    private void HidePauseMenu()
////////    {
////////        SceneManager.UnloadSceneAsync(PauseMenu);
////////        Time.timeScale = 1f;
////////        Cursor.visible = false;
////////        Cursor.lockState = CursorLockMode.Locked;
////////        gameIsFrozen = false;
////////    }
////////}

//////////using UnityEngine;
//////////using UnityEngine.SceneManagement;
//////////using UnityEngine.InputSystem;

//////////public class GamePauseHandler : MonoBehaviour
//////////{
//////////    [SerializeField] private string pauseMenuSceneName = "PauseMenu";
//////////    private bool gameIsFrozen = false;

//////////    void Start()
//////////    {
//////////        Time.timeScale = 1f;
//////////        Cursor.visible = false;
//////////        Cursor.lockState = CursorLockMode.Locked;
//////////    }

//////////    void Update()
//////////    {
//////////        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
//////////        {
//////////            TogglePauseState();
//////////        }
//////////    }

//////////    public void TogglePauseState()
//////////    {
//////////        if (!gameIsFrozen)
//////////        {
//////////            PauseGame();
//////////        }
//////////        else
//////////        {
//////////            ResumeGame();
//////////        }
//////////    }

//////////    public void PauseGame()
//////////    {
//////////        if (gameIsFrozen)
//////////            return;

//////////        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
//////////        Time.timeScale = 0f;
//////////        Cursor.visible = true;
//////////        Cursor.lockState = CursorLockMode.None;
//////////        gameIsFrozen = true;
//////////    }

//////////    public void ResumeGame()
//////////    {
//////////        if (!gameIsFrozen)
//////////            return;

//////////        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
//////////        Time.timeScale = 1f;
//////////        Cursor.visible = false;
//////////        Cursor.lockState = CursorLockMode.Locked;
//////////        gameIsFrozen = false;
//////////    }

//////////    public void ContinueButton()
//////////    {
//////////        ResumeGame();
//////////    }
//////////}

//////////using UnityEngine;
//////////using UnityEngine.SceneManagement;
//////////using UnityEngine.InputSystem;

//////////public class GamePauseHandler : MonoBehaviour
//////////{
//////////    [SerializeField] private string pauseMenuSceneName = "PauseMenu";
//////////    private bool gameIsFrozen = false;

//////////    void Start()
//////////    {
//////////        ResumeGameVisuals();
//////////    }

//////////    void Update()
//////////    {
//////////        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
//////////        {
//////////            TogglePauseState();
//////////        }
//////////    }

//////////    public void TogglePauseState()
//////////    {
//////////        if (gameIsFrozen)
//////////            ResumeGame();
//////////        else
//////////            PauseGame();
//////////    }

//////////    public void PauseGame()
//////////    {
//////////        if (gameIsFrozen)
//////////            return;

//////////        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
//////////        Time.timeScale = 0f;
//////////        Cursor.lockState = CursorLockMode.None;
//////////        Cursor.visible = true;
//////////        gameIsFrozen = true;
//////////    }

//////////    public void ResumeGame()
//////////    {
//////////        if (!gameIsFrozen)
//////////            return;

//////////        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
//////////        Time.timeScale = 1f;
//////////        Cursor.lockState = CursorLockMode.Locked;
//////////        Cursor.visible = false;
//////////        gameIsFrozen = false;
//////////    }

//////////    public void ContinueButton()
//////////    {
//////////        ResumeGame();
//////////    }

//////////    private void ResumeGameVisuals()
//////////    {
//////////        Cursor.lockState = CursorLockMode.Locked;
//////////        Cursor.visible = false;
//////////    }
//////////}

//////using UnityEngine;
//////using UnityEngine.InputSystem;

//////public class GamePauseHandler : MonoBehaviour
//////{
//////    [SerializeField] private GameObject pauseCanvas; // Ziehe dein PauseCanvas hier rein (im Inspector)
//////    private bool gameIsFrozen = false;

//////    void Start()
//////    {
//////        // Stelle sicher, dass das Canvas am Anfang deaktiviert ist
//////        if (pauseCanvas != null)
//////        {
//////            pauseCanvas.SetActive(false);
//////        }
//////        Time.timeScale = 1f;
//////        Cursor.visible = false;
//////        Cursor.lockState = CursorLockMode.Locked;
//////        gameIsFrozen = false;
//////    }

//////    void Update()
//////    {
//////        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
//////        {
//////            TogglePauseState();
//////        }
//////    }

//////    private void TogglePauseState()
//////    {
//////        if (!gameIsFrozen)
//////        {
//////            ShowPauseMenu();
//////        }
//////        else
//////        {
//////            HidePauseMenu();
//////        }
//////    }

//////    private void ShowPauseMenu()
//////    {
//////        if (pauseCanvas != null)
//////        {
//////            pauseCanvas.SetActive(true);
//////        }
//////        Time.timeScale = 0f;
//////        Cursor.visible = true;
//////        Cursor.lockState = CursorLockMode.None;
//////        gameIsFrozen = true;
//////    }

//////    private void HidePauseMenu()
//////    {
//////        if (pauseCanvas != null)
//////        {
//////            pauseCanvas.SetActive(false);
//////        }
//////        Time.timeScale = 1f;
//////        Cursor.visible = false;
//////        Cursor.lockState = CursorLockMode.Locked;
//////        gameIsFrozen = false;
//////    }
//////}

////using UnityEngine;
////using UnityEngine.InputSystem;
////using static UnityEngine.InputSystem.DefaultInputActions;

////public class GamePauseHandler : MonoBehaviour
////{
////    [SerializeField] private GameObject pauseCanvas;

////    private Player input;  // dein generierter Input-Wrapper
////    private bool gameIsFrozen;

////    private void Awake()
////    {
////        input = new Player();          // Klasse aus deinem InputActionAsset
////    }

////    private void OnEnable()
////    {
////        input.PlayerActions.Enable();
////        input.PlayerActions.Pause.performed += OnPause;
////    }

////    private void OnDisable()
////    {
////        input.PlayerActions.Pause.performed -= OnPause;
////        input.PlayerActions.Disable();
////    }

////    private void Start()
////    {
////        pauseCanvas.SetActive(false);
////        Time.timeScale = 1f;
////        Cursor.visible = false;
////        Cursor.lockState = CursorLockMode.Locked;
////        gameIsFrozen = false;
////    }

////    private void OnPause(InputAction.CallbackContext ctx)
////    {
////        TogglePauseState();
////    }

////    private void TogglePauseState()
////    {
////        if (gameIsFrozen) HidePauseMenu();
////        else ShowPauseMenu();
////    }

////    private void ShowPauseMenu()
////    {
////        pauseCanvas.SetActive(true);
////        Time.timeScale = 0f;
////        Cursor.visible = true;
////        Cursor.lockState = CursorLockMode.None;
////        gameIsFrozen = true;
////    }

////    private void HidePauseMenu()
////    {
////        pauseCanvas.SetActive(false);
////        Time.timeScale = 1f;
////        Cursor.visible = false;
////        Cursor.lockState = CursorLockMode.Locked;
////        gameIsFrozen = false;
////    }

////    // Für Button im UI
////    public void ContinueButton()
////    {
////        HidePauseMenu();
////    }
////}

//using UnityEngine;
//using UnityEngine.InputSystem;
//using static UnityEngine.InputSystem.DefaultInputActions;

//public class GamePauseHandler : MonoBehaviour
//{
//    [SerializeField] private GameObject pauseCanvas;
//    [SerializeField] private GameObject escapeMenu;   // <- Dein "EscapeMenu" Canvas

//    private Player input;  // dein generierter Input-Wrapper
//    private bool gameIsFrozen;

//    private void Awake()
//    {
//        input = new Player();
//    }

//    private void OnEnable()
//    {
//        input.PlayerActions.Enable();
//        input.PlayerActions.Pause.performed += OnPause;
//    }

//    private void OnDisable()
//    {
//        input.PlayerActions.Pause.performed -= OnPause;
//        input.PlayerActions.Disable();
//    }

//    private void Start()
//    {
//        if (pauseCanvas != null)
//            pauseCanvas.SetActive(false);

//        if (escapeMenu != null)
//            escapeMenu.SetActive(false);      // EscapeMenu am Anfang zu

//        Time.timeScale = 1f;
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        gameIsFrozen = false;
//    }

//    private void OnPause(InputAction.CallbackContext ctx)
//    {
//        TogglePauseState();
//    }

//    private void TogglePauseState()
//    {
//        if (gameIsFrozen) HidePauseMenu();
//        else ShowPauseMenu();
//    }

//    private void ShowPauseMenu()
//    {
//        if (pauseCanvas != null)
//            pauseCanvas.SetActive(true);

//        if (escapeMenu != null)
//            escapeMenu.SetActive(false);      // sicherstellen, dass es geschlossen startet

//        Time.timeScale = 0f;
//        Cursor.visible = true;
//        Cursor.lockState = CursorLockMode.None;
//        gameIsFrozen = true;
//    }

//    private void HidePauseMenu()
//    {
//        if (pauseCanvas != null)
//            pauseCanvas.SetActive(false);

//        if (escapeMenu != null)
//            escapeMenu.SetActive(false);      // beim Verlassen auch schließen

//        Time.timeScale = 1f;
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        gameIsFrozen = false;
//    }

//    // UI-Button „Continue“
//    public void ContinueButton()
//    {
//        HidePauseMenu();
//    }

//    // UI-Button auf deinem PauseCanvas: öffnet/ schließt das EscapeMenu
//    public void ToggleEscapeMenu()
//    {
//        if (escapeMenu == null) return;

//        bool isActive = escapeMenu.activeSelf;
//        escapeMenu.SetActive(!isActive);
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class GamePauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject escapeMenu;

    private Player input;
    private bool gameIsFrozen;

    private void Awake()
    {
        input = new Player();
    }

    private void OnEnable()
    {
        input.PlayerActions.Enable();
        input.PlayerActions.Pause.performed += OnPause;
        input.PlayerActions.Escape.performed += OnEscape;  // <- Neue Action
    }

    private void OnDisable()
    {
        input.PlayerActions.Pause.performed -= OnPause;
        input.PlayerActions.Escape.performed -= OnEscape;
        input.PlayerActions.Disable();
    }

    private void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        if (escapeMenu != null)
            escapeMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsFrozen = false;
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        TogglePauseState();
    }

    private void OnEscape(InputAction.CallbackContext ctx)
    {
        ToggleEscapeMenu();  // Gleiche Logik wie Button
    }

    private void TogglePauseState()
    {
        if (gameIsFrozen) HidePauseMenu();
        else ShowPauseMenu();
    }

    private void ShowPauseMenu()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        if (escapeMenu != null)
            escapeMenu.SetActive(false);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameIsFrozen = true;
    }

    private void HidePauseMenu()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        if (escapeMenu != null)
            escapeMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsFrozen = false;
    }

    // UI-Button „Continue“
    public void ContinueButton()
    {
        HidePauseMenu();
    }

    // UI-Button auf PauseCanvas
    public void ToggleEscapeMenu()
    {
        if (escapeMenu == null) return;

        bool isActive = escapeMenu.activeSelf;
        escapeMenu.SetActive(!isActive);
    }
}

