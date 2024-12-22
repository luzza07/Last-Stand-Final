using UnityEngine;
using System.Collections;

public class PlayerAimAndShoot : MonoBehaviour
{
    public bool canAim = true;
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint;     // The point where bullets are spawned
    public float bulletSpeed = 40f; // Speed of the bullet

    public static PlayerAimAndShoot playerAimAndShoot = null;

    private void Awake()
    {
        if (!playerAimAndShoot)
        {
            playerAimAndShoot = this;
        }
    }

    void Start()
    {
        // Start the shooting coroutine immediately when the game starts
        StartCoroutine(AutoShoot());
    }

    void Update()
    {
        if (canAim)
        {
            AimWithMouse();
        }
    }

    void AimWithMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Ignore Z-axis
        direction.z = 0;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the player's Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator AutoShoot()
    {
        while (canAim) // Continuously shoot as long as the game is running
        {
            Shoot();  // Call the shoot method

            // Wait for the specified interval before shooting again
            yield return new WaitForSeconds(GameManager.gameManager.ShootInterval);
        }
    }

    void Shoot()
    {
        AudioManager.instance.PlayShootSound();

        // Spawn bullet at firePoint position with firePoint rotation (for direction)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction to apply force
        Vector3 direction = firePoint.up;

        // Apply velocity to move bullet straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        // Reset Z-rotation to keep bullet visually straight
        bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
