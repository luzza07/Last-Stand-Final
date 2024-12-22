using UnityEngine;
using System.Collections;  // Make sure this is at the top

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    public float playerMoveSpeed = 3f; // Movement speed
    public static PlayerController instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        // Get raw input for immediate response
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Combine inputs into a direction vector
        Vector3 direction = new Vector3(inputX, inputY, 0);

        // Normalize direction to maintain consistent speed
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        // Apply movement
        transform.position += direction * playerMoveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided");
            // Decrease player health
            GameManager.gameManager.decreasePlayerHealth();

            // Get the Rigidbody2D component of the player
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Calculate the direction away from the enemy
                Vector2 forceDirection = (transform.position - collision.transform.position).normalized;

                // Apply a small force to push the player away
                float forceMagnitude = 10f; // Adjust the force magnitude as needed
                rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);

                // Stop the player after a short delay
                StartCoroutine(StopPlayerAfterPush(rb));
            }
        }
    }

    private IEnumerator StopPlayerAfterPush(Rigidbody2D rb)
    {
        // Wait for a short time to allow the push to take effect
        yield return new WaitForSeconds(0.1f);

        // Stop the player's movement
        rb.linearVelocity = Vector2.zero;
    }
}
