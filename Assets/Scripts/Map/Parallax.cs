using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform player;
    public float parallaxEffectMultiplier = 0.05f;

    private Vector3 initialPlayerPosition;
    private Renderer quadRenderer;

    void Start()
    {
        initialPlayerPosition = player.position;
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate the difference in vertical positions between quad and player
        float verticalDifference = player.position.y - transform.position.y;

        // Move the quad to match the player's y-position
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);

        // Calculate the background movement based on the difference
        float backgroundMoveY = verticalDifference * parallaxEffectMultiplier;

        // Update the texture offset for the parallax effect
        Vector2 newTextureOffset = quadRenderer.material.mainTextureOffset + Vector2.up * backgroundMoveY;
        quadRenderer.material.mainTextureOffset = newTextureOffset;
    }
}