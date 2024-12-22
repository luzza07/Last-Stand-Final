using UnityEngine;

public class PlayerYChange : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if player is pressing 'A' (for Y rotation = 180 degrees)
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);  // Rotate 180 degrees around the Y-axis
        }
        // Check if player is pressing 'D' (for Y rotation = 0 degrees)
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Rotate to 0 degrees (default rotation)
        }
    }
}
