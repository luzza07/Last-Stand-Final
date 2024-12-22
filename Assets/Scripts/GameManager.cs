using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int playerHealth = 5;
    [SerializeField]
    int playerDamage = 1;
    [SerializeField]
    float playerMoveSpeed = 3f;
    [SerializeField]
    float shootInterval = 0.7f;
    public bool gameOver = false;

    public Text HPText;
    public GameObject gameOverPanel;

    // Enemy Prefabs
    public GameObject[] enemyPrefabs;

    // Variables for timing and managing enemy spawning
    private float elapsedTime = 0f;

    // Variables to control spawn rates for each enemy type
    private float enemy0SpawnRate = 0.02f;
    private float enemy1SpawnRate = 1f; // Enemy 1 spawns every 2 seconds initially
    private float enemy2SpawnRate = 6f; // Enemy 2 spawns every 6 seconds initially

    private float enemy0Timer = 0f; // Timer for enemy 0
    private float enemy1Timer = 0f; // Timer for enemy 1
    private float enemy2Timer = 0f; // Timer for enemy 2

    public static GameManager gameManager = null;

    public void UpgradeHealth(int amount)
    {
        playerHealth += amount;
        if (playerHealth >=5)
        {
            playerHealth = 5;
        }
        HPText.text = playerHealth + "/5";  // Update UI immediately
    }

    public void UpgradeDamage(int amount)
    {
        playerDamage += amount;
    }

    public void UpgradeMoveSpeed(float amount)
    {
        playerMoveSpeed += amount;
    }

    public void UpgradeShootInterval(float amount)
    {
        shootInterval -= amount;  // Reduce interval to shoot faster
        if (shootInterval < 0.2f) shootInterval = 0.2f;  // Cap the minimum interval
    }


    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = this;
        }
    }

    // Public getter for moveSpeed
    public int PlayerDamage
    {
        get { return playerDamage; }
    }

    public float PlayerMoveSpeed
    {
        get { return playerMoveSpeed; }
    }

    public float ShootInterval
    {
        get { return shootInterval; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update elapsed time
        elapsedTime += Time.deltaTime;

        if (!gameOver)
        {
            // Adjust spawn rates based on elapsed time
            AdjustSpawnRates();

            // Handle spawning of each enemy based on timers
            HandleEnemySpawning();
        }
        
    }

    public void decreasePlayerHealth()
    {

        playerHealth--;
        HPText.text = playerHealth+"/5";
        if(playerHealth <= 0)
        {
            AudioManager.instance.GameOverSound();
            gameOver = true;
            PlayerController.instance.canMove = false;
            PlayerAimAndShoot.playerAimAndShoot.canAim = false;
            gameOverPanel.SetActive(true);
            Destroy(GameObject.FindWithTag("Player"));
        }
    }

    void HandleEnemySpawning()
    {
        // Spawn enemy 0 if its timer has reached its spawn rate
        if (enemy0Timer >= enemy0SpawnRate)
        {
            SpawnEnemy(0); // Spawn enemy 0
            enemy0Timer = 0f; // Reset timer
        }
        else
        {
            enemy0Timer += Time.deltaTime; // Increment timer for enemy 0
        }

        // Spawn enemy 1 if its timer has reached its spawn rate
        if (enemy1Timer >= enemy1SpawnRate)
        {
            SpawnEnemy(1); // Spawn enemy 1
            enemy1Timer = 0f; // Reset timer
        }
        else
        {
            enemy1Timer += Time.deltaTime; // Increment timer for enemy 1
        }

        // Spawn enemy 2 if its timer has reached its spawn rate
        if (enemy2Timer >= enemy2SpawnRate)
        {
            SpawnEnemy(2); // Spawn enemy 2
            enemy2Timer = 0f; // Reset timer
        }
        else
        {
            enemy2Timer += Time.deltaTime; // Increment timer for enemy 2
        }
    }

    void AdjustSpawnRates()
    {
        // If time is more than 1 minute and less than 2 minutes, spawn both enemy[0] and enemy[1]
        if (elapsedTime >= 60f && elapsedTime < 120f)
        {
            enemy0SpawnRate = 0.5f; // Enemy 0 every 1 second (faster spawn rate)
            enemy1SpawnRate = 2f; // Enemy 1 every 2 seconds
            enemy2SpawnRate = Mathf.Infinity; // Don't spawn enemy 2 yet
        }
        // If time is more than 2 minutes, spawn all three enemies
        else if (elapsedTime >= 120f)
        {
            enemy0SpawnRate = 1; // Enemy 0 every 1 second (faster spawn rate)
            enemy1SpawnRate = 2; // Enemy 1 every 2 seconds
            enemy2SpawnRate = 10f; // Enemy 2 every 4 seconds (faster spawn rate)
        }
        // If time is less than 1 minute, only spawn enemy 0
        else
        {
            enemy0SpawnRate = 0.7f; // Enemy 0 every 1 second (faster spawn rate)
            enemy1SpawnRate = Mathf.Infinity; // Don't spawn enemy 1 yet
            enemy2SpawnRate = Mathf.Infinity; // Don't spawn enemy 2 yet
        }
    }


    // Method to spawn an enemy based on its index
    void SpawnEnemy(int enemyIndex)
    {
        if (enemyIndex < enemyPrefabs.Length)
        {
            // Get random position outside the screen
            Vector3 spawnPosition = GetRandomOffScreenPosition();

            // Instantiate the enemy at the chosen position
            Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        }
    }

    // Get a random position outside the screen's view
    Vector3 GetRandomOffScreenPosition()
    {
        // Get the camera's viewport size in world units
        Camera camera = Camera.main;
        float screenWidth = camera.orthographicSize * camera.aspect;
        float screenHeight = camera.orthographicSize;

        // Randomly decide if we want to spawn outside the top/bottom/left/right
        Vector3 spawnPosition;

        int side = Random.Range(0, 4); // 0 = Top, 1 = Bottom, 2 = Left, 3 = Right

        switch (side)
        {
            case 0: // Top side
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), screenHeight + 1, 0);
                break;
            case 1: // Bottom side
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), -screenHeight - 1, 0);
                break;
            case 2: // Left side
                spawnPosition = new Vector3(-screenWidth - 1, Random.Range(-screenHeight, screenHeight), 0);
                break;
            case 3: // Right side
                spawnPosition = new Vector3(screenWidth + 1, Random.Range(-screenHeight, screenHeight), 0);
                break;
            default:
                spawnPosition = Vector3.zero;
                break;
        }

        return spawnPosition;
    }
}
