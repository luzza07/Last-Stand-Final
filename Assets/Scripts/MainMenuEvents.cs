using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public void onStartButtonClick()
    {
        if (TapToStart.instance)
        {
            TapToStart.instance.resetvalues();
        }
        AudioManager.instance.ButtomClickSound();
        Debug.Log("Click");
        Destroy(AudioManager.instance);
        SceneManager.LoadScene(1);
    }public void onQuittButtonClick()
    {
        AudioManager.instance.ButtomClickSound();
        Application.Quit();
    }
}
