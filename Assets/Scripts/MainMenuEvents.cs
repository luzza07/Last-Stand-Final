using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public void onStartButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        Debug.Log("Click");
        SceneManager.LoadScene(1);
    }public void onQuittButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        Application.Quit();
    }
}
