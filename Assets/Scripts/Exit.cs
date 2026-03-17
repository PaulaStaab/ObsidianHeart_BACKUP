using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button exitButton;
    public GameObject confirmationPanel;
    public Button yesButton;
    public Button noButton;
    public string lobbySceneName = "Lobby";  // Name deiner Lobby-Szene in Build Settings

    void Start()
    {
        exitButton.onClick.AddListener(ShowConfirmation);
        yesButton.onClick.AddListener(GoToLobby);
        noButton.onClick.AddListener(HideConfirmation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowConfirmation();
        }
    }

    void ShowConfirmation()
    {
        confirmationPanel.SetActive(true);
    }

    void HideConfirmation()
    {
        confirmationPanel.SetActive(false);
    }

    void GoToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
}
