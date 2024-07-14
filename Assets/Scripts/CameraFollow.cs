using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float offsetY = 0f; // Offset in the Y direction

    void Update()
    {
        // Check if the player reference is set
        if (player != null)
        {
            // Get the current camera position
            Vector3 newPosition = transform.position;

            // Update the camera's y position to follow the player
            newPosition.y = player.position.y + offsetY;

            // Apply the new position to the camera
            transform.position = newPosition;
        }
    }
}
