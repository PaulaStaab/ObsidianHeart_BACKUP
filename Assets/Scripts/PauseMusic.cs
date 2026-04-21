using UnityEngine;

public class PauseMusic : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioSource backgroundMusic;

    private bool isPaused = false;

    private void Start()
    {
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause();
        }
    }
}