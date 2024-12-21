using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip levelUPSound;
    public AudioClip buttonClickSound;
    public AudioClip gameOverSound;

    private AudioSource backgroundSource;
    private AudioSource sfxSource;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Create two Audio Sources for BGM and SFX
        backgroundSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Set background music properties
        backgroundSource.clip = backgroundMusic;
        backgroundSource.loop = true;  // Loop background music
        backgroundSource.volume = 0.5f;  // Set volume

        // Play background music
        backgroundSource.Play();
    }

    // Play sound effects
    public void PlayShootSound()
    {
        sfxSource.PlayOneShot(shootSound);
    }

    public void PlayLevelUpSound()
    {
        sfxSource.PlayOneShot(levelUPSound);
    }

    // Pause or Stop BGM
    public void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }
    public void ButtomClickSound()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }
    public void GameOverSound()
    {
        sfxSource.PlayOneShot(gameOverSound);
    }
}
