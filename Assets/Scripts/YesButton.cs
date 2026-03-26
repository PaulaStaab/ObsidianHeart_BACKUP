using UnityEngine;
using UnityEngine.SceneManagement;

public class YesButton : MonoBehaviour
{
    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
