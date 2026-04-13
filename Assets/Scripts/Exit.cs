//////////using UnityEngine;
//////////using UnityEngine.SceneManagement;
//////////using UnityEngine.UI;
//////////using UnityEngine.InputSystem;

//////////public class Exit : MonoBehaviour
//////////{
//////////    public Button exitButton;
//////////    public GameObject confirmationPanel;
//////////    public Button yesButton;
//////////    public Button noButton;
//////////    public string lobbySceneName = "Lobby";  // Name deiner Lobby-Szene in Build Settings

//////////    void Start()
//////////    {
//////////        exitButton.onClick.AddListener(ShowConfirmation);
//////////        yesButton.onClick.AddListener(GoToLobby);
//////////        noButton.onClick.AddListener(HideConfirmation);
//////////    }

//////////    void Update()
//////////    {
//////////        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
//////////        {
//////////            ShowConfirmation();
//////////        }
//////////    }

//////////    void ShowConfirmation()
//////////    {
//////////        confirmationPanel.SetActive(true);
//////////    }

//////////    void HideConfirmation()
//////////    {
//////////        confirmationPanel.SetActive(false);
//////////    }

//////////    void GoToLobby()
//////////    {
//////////        SceneManager.LoadScene(lobbySceneName);
//////////    }
//////////}

////////using UnityEngine;
////////using UnityEngine.SceneManagement;
////////using UnityEngine.UI;
////////using UnityEngine.InputSystem;

////////public class Exit : MonoBehaviour
////////{
////////    public Button exitButton;
////////    public GameObject confirmationPanel;
////////    public Button yesButton;
////////    public Button noButton;
////////    public string lobbySceneName = "Lobby";

////////    void Start()
////////    {
////////        exitButton.onClick.AddListener(ShowConfirmation);
////////        yesButton.onClick.AddListener(GoToLobby);
////////        noButton.onClick.AddListener(HideConfirmation);
////////    }

////////    void Update()
////////    {
////////        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
////////        {
////////            ShowConfirmation();
////////        }
////////    }

////////    void ShowConfirmation()
////////    {
////////        confirmationPanel.SetActive(true);
////////    }

////////    void HideConfirmation()
////////    {
////////        confirmationPanel.SetActive(false);
////////    }

////////    void GoToLobby()
////////    {
////////        SceneManager.LoadScene(lobbySceneName);
////////    }
////////}

//////using UnityEngine;
//////using UnityEngine.SceneManagement;
//////using UnityEngine.UI;
//////using UnityEngine.InputSystem;

//////public class Exit : MonoBehaviour
//////{
//////    public Button exitButton;
//////    public GameObject confirmationPanel;
//////    public Button yesButton;
//////    public Button noButton;
//////    public string lobbySceneName = "Lobby";

//////    private bool panelIstOffen = false;

//////    void Start()
//////    {
//////        exitButton.onClick.AddListener(() => TogglePanel());
//////        yesButton.onClick.AddListener(GoToLobby);
//////        noButton.onClick.AddListener(() => HidePanel());

//////        if (confirmationPanel != null)
//////            confirmationPanel.SetActive(false);
//////    }

//////    void Update()
//////    {
//////        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
//////        {
//////            TogglePanel();
//////        }
//////    }

//////    private void TogglePanel()
//////    {
//////        if (confirmationPanel != null)
//////        {
//////            panelIstOffen = !panelIstOffen;
//////            confirmationPanel.SetActive(panelIstOffen);
//////        }
//////    }

//////    private void HidePanel()
//////    {
//////        if (confirmationPanel != null)
//////        {
//////            panelIstOffen = false;
//////            confirmationPanel.SetActive(false);
//////        }
//////    }

//////    void GoToLobby()
//////    {
//////        SceneManager.LoadScene(lobbySceneName);
//////    }
//////}

////using UnityEngine;
////using UnityEngine.InputSystem;
////using UnityEngine.SceneManagement;
////using UnityEngine.UI;

////public class Exit : MonoBehaviour
////{
////    public Button exitButton;
////    public GameObject confirmationPanel;
////    public Button yesButton;
////    public Button noButton;
////    public string lobbySceneName = "Lobby";

////    private PlayerInput playerInput;  // Deine Input Actions
////    private InputAction escapeAction;

////    private bool panelIstOffen = false;

////    void Awake()
////    {
////        playerInput = GetComponent<PlayerInput>();
////        if (playerInput != null)
////        {
////            escapeAction = playerInput.actions["Escape"];
////        }
////    }

////    void Start()
////    {
////        exitButton.onClick.AddListener(() => TogglePanel());
////        yesButton.onClick.AddListener(GoToLobby);
////        noButton.onClick.AddListener(() => HidePanel());

////        if (confirmationPanel != null)
////            confirmationPanel.SetActive(false);
////    }

////    void Update()
////    {
////        if (escapeAction != null && escapeAction.triggered)
////        {
////            TogglePanel();
////        }
////    }

////    // Rest unverändert...
////    private void TogglePanel()
////    {
////        if (confirmationPanel != null)
////        {
////            panelIstOffen = !panelIstOffen;
////            confirmationPanel.SetActive(panelIstOffen);
////        }
////    }

////    private void HidePanel()
////    {
////        if (confirmationPanel != null)
////        {
////            panelIstOffen = false;
////            confirmationPanel.SetActive(false);
////        }
////    }

////    void GoToLobby()
////    {
////        SceneManager.LoadScene(lobbySceneName);
////    }
////}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class Exit : MonoBehaviour
//{
//    public Button exitButton;
//    public GameObject confirmationPanel;
//    public Button yesButton;
//    public Button noButton;
//    public string lobbySceneName = "Lobby";

//    private PlayerInput playerInput;
//    private InputAction escapeAction;

//    private bool panelIstOffen = false;

//    void Awake()
//    {
//        playerInput = GetComponent<PlayerInput>();
//        if (playerInput != null)
//        {
//            escapeAction = playerInput.actions["Escape"];
//        }
//    }

//    void Start()
//    {
//        exitButton.onClick.AddListener(() => TogglePanel());
//        yesButton.onClick.AddListener(GoToLobby);
//        noButton.onClick.AddListener(() => HidePanel());

//        if (confirmationPanel != null)
//            confirmationPanel.SetActive(false);
//    }

//    void Update()
//    {
//        if (escapeAction != null && escapeAction.triggered)
//        {
//            TogglePanel();
//        }
//    }

//    private void TogglePanel()
//    {
//        if (confirmationPanel != null)
//        {
//            panelIstOffen = !panelIstOffen;
//            confirmationPanel.SetActive(panelIstOffen);
//        }
//    }

//    private void HidePanel()
//    {
//        if (confirmationPanel != null)
//        {
//            panelIstOffen = false;
//            confirmationPanel.SetActive(false);
//        }
//    }

//    void GoToLobby()
//    {
//        SceneManager.LoadScene(lobbySceneName);
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Exit : MonoBehaviour
{
    public Button exitButton;
    public GameObject confirmationPanel;
    public Button yesButton;
    public Button noButton;
    public string lobbySceneName = "Lobby";

    private PlayerInput playerInput;
    private InputAction escapeAction;

    private bool panelIstOffen = false;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            escapeAction = playerInput.actions["Escape"];
        }
    }

    void Start()
    {
        exitButton.onClick.AddListener(() => TogglePanel());
        yesButton.onClick.AddListener(GoToLobby);
        noButton.onClick.AddListener(() => HidePanel());

        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);
    }

    void Update()
    {
        if (escapeAction != null && escapeAction.triggered)
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        if (confirmationPanel != null)
        {
            panelIstOffen = !panelIstOffen;
            confirmationPanel.SetActive(panelIstOffen);
        }
    }

    private void HidePanel()
    {
        if (confirmationPanel != null)
        {
            panelIstOffen = false;
            confirmationPanel.SetActive(false);
        }
    }

    void GoToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
}