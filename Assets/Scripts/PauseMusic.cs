using UnityEngine;

// Handles pausing and resuming the game, toggles the pause screen,
// and pauses or resumes the background music.
public class PauseMusic : MonoBehaviour
{
    // UI object shown while the game is paused.
    [SerializeField] private GameObject pauseScreen;
    // Background music source controlled by the pause state.
    [SerializeField] private AudioSource backgroundMusic;

    // Tracks whether the game is currently paused.
    private bool isPaused = false;

    private void Start()
    {
        // Hide the pause screen when the scene starts.
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        // Toggle between pause and resume when Escape is pressed.
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
        // Show the pause screen and stop game time.
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Pause the background music if it is currently playing.
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    public void ResumeGame()
    {
        // Hide the pause screen and resume game time.
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Resume the background music if an audio source is assigned.
        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause();
        }
    }
}