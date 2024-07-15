using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform player;
    public float parallaxEffectMultiplier = 0.1f;

    private Vector3 initialPlayerPosition;
    private Renderer quadRenderer;
    private float lastPlayerY;

    void Start()
    {
        initialPlayerPosition = player.position;
        quadRenderer = GetComponent<Renderer>();
        lastPlayerY = player.position.y;
    }

    void Update()
    {
        float playerMoveDown = lastPlayerY - player.position.y;

        if (playerMoveDown > 0)
        {
            float backgroundMoveY = playerMoveDown * parallaxEffectMultiplier;
            Vector2 newTextureOffset = quadRenderer.material.mainTextureOffset + Vector2.up * backgroundMoveY;
            quadRenderer.material.mainTextureOffset = newTextureOffset;
        }

        lastPlayerY = player.position.y;
    }
}