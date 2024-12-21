using UnityEngine;
using UnityEngine.SceneManagement;  // Import SceneManager for scene loading
using System.Collections;  // To use Coroutines

public class TapToStart : MonoBehaviour
{
    private float timeElapsed = 0f;
    private bool sceneStarted = false;

    public static TapToStart instance = null;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void resetvalues()
    {
        timeElapsed = 0f;
        sceneStarted = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!sceneStarted)
        {
            timeElapsed += Time.deltaTime;

            // Check if 2 seconds have passed and if space or screen is tapped
            if (timeElapsed >= 2f && (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
            {
                AudioManager.instance.ButtomClickSound();
                sceneStarted = true;
                Destroy(AudioManager.instance);
                resetvalues();
                StartCoroutine(LoadSceneAsync(2));  // Start loading scene asynchronously
            }
        }
    }

    // Coroutine to load scene asynchronously
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Begin loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // While the scene is still loading, wait
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
