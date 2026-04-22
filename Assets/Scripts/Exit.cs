using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// Controls an exit confirmation panel that can be opened via UI button
// or Escape key and loads the lobby scene when confirmed.
public class Exit : MonoBehaviour
{
    // Button that opens or closes the confirmation panel.
    public Button exitButton;
    // UI panel asking the player to confirm leaving.
    public GameObject confirmationPanel;
    // Button that confirms the exit action.
    public Button yesButton;
    // Button that cancels and closes the panel.
    public Button noButton;
    // Name of the scene that should be loaded after confirmation.
    public string lobbySceneName = "Lobby";

    // Reference to the PlayerInput component on this object.
    private PlayerInput playerInput;
    // Input action used to detect the Escape key.
    private InputAction escapeAction;

    // Tracks whether the confirmation panel is currently open.
    private bool panelIstOffen = false;

    void Awake()
    {
        // Try to get the PlayerInput component and the Escape action.
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            escapeAction = playerInput.actions["Escape"];
        }
    }

    void Start()
    {
        // Register button callbacks for opening, confirming, and closing the panel.
        exitButton.onClick.AddListener(() => TogglePanel());
        yesButton.onClick.AddListener(GoToLobby);
        noButton.onClick.AddListener(() => HidePanel());

        // Hide the confirmation panel when the scene starts.
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);
    }

    void Update()
    {
        // Toggle the panel when the Escape action is triggered.
        if (escapeAction != null && escapeAction.triggered)
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        // Open or close the confirmation panel by toggling its current state.
        if (confirmationPanel != null)
        {
            panelIstOffen = !panelIstOffen;
            confirmationPanel.SetActive(panelIstOffen);
        }
    }

    private void HidePanel()
    {
        // Explicitly close the confirmation panel.
        if (confirmationPanel != null)
        {
            panelIstOffen = false;
            confirmationPanel.SetActive(false);
        }
    }

    void GoToLobby()
    {
        // Load the configured lobby scene.
        SceneManager.LoadScene(lobbySceneName);
    }
}