using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider xpBar;
    public GameObject perkSystem;
    public GameObject HealthUpgrade;
    public Text levelText; 
    public Text scoreText; 
    public Text scoreTextGameOver; 
    public Text highScore; 

    [Header("XP Settings")]
    public int level = 1;
    public int currentXP = 0;
    public int maxXP = 100;
    int savedHighScore;
    public int currentScore = 0;

    void Start()
    {
        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = "High Score: " + savedHighScore;
        UpdateUI();
    }

    // Call this method when the player gains XP
    public void GainXPScore(int xp, int score)
    {
        currentXP += xp;
        currentScore += score;

        // Check if the current score exceeds the saved high score
        if (currentScore > savedHighScore)
        {
            savedHighScore = currentScore; // Update the local high score variable
            PlayerPrefs.SetInt("HighScore", savedHighScore); // Save it to PlayerPrefs
        }

        if (currentXP >= maxXP)
        {
            LevelUp();
        }

        UpdateUI();
    }


    private void LevelUp()
    {
        AudioManager.instance.PlayLevelUpSound();
        level++;
        if (level % 10 == 0)
        {
            HealthUpgrade.SetActive(true);
        }
        currentXP -= maxXP;  // Carry over excess XP
        maxXP += 50;  // Increase XP requirement
        ShowAbilityPanel();
    }

    // Update slider and text
    private void UpdateUI()
    {
        xpBar.value = (float)currentXP / maxXP;
        levelText.text = "" + level;
        scoreText.text = "Score: " + currentScore;
        scoreTextGameOver.text = "Score: " + currentScore;

        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = "High Score: " + savedHighScore;
        Debug.Log("HS: "+savedHighScore);
    }

    // Display ability selection
    private void ShowAbilityPanel()
    {
        Time.timeScale = 0f;  // Pause game (freeze all movement)
        // Example: Enable ability panel
         perkSystem.SetActive(true);
    }
    public void onHealthUpgrade()
    {
        Time.timeScale = 1f;  // Pause game (freeze all movement)
        perkSystem.SetActive(false);
        HealthUpgrade.SetActive(false);
        GameManager.gameManager.UpgradeHealth(1);  // +1 to health
    }

    public void onDamageUpgrade()
    {
        Time.timeScale = 1f;  // Pause game (freeze all movement)
        perkSystem.SetActive(false);
        GameManager.gameManager.UpgradeDamage(1);  // +1 to damage
    }

    public void onMoveUpgrade()
    {
        Time.timeScale = 1f;  // Pause game (freeze all movement)
        perkSystem.SetActive(false);
        GameManager.gameManager.UpgradeMoveSpeed(0.2f);  // +0.5 move speed
    }

    public void onAttackUpgrade()
    {
        Time.timeScale = 1f;  // Pause game (freeze all movement)
        perkSystem.SetActive(false);
        GameManager.gameManager.UpgradeShootInterval(0.1f);  // Faster shooting
    }
}
