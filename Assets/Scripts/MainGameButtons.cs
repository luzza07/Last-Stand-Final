using UnityEngine;

using UnityEngine.SceneManagement;  // Import SceneManager

public class MainGameButtons : MonoBehaviour
{
    public GameObject pausePanel;
    public void onPauseButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;  // Pause game (freeze all movement)
    }
    public void onPlayButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;  // Resume game
    }
    public void onRestartButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        Time.timeScale = 1f;  // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload current scene
    }
    public void onHomeButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        SceneManager.LoadScene(0);  // Load the MainMenu scene
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
