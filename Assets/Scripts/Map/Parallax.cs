using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    float offset;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        speed = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * speed;
        renderer.material.mainTextureOffset = new Vector2(0, -offset);
        //offset = Time.time * speed;
        //renderer.material.mainTextureOffset = new Vector2(0, speed * Time.deltaTime);
        if (PointSystem.instance.currentPoint == 10)
        {
            speed = 0.135f;
        }
        if (PointSystem.instance.currentPoint == 20)
        {
            speed = 0.175f;
        }
        if (PointSystem.instance.currentPoint == 30)
        {
            speed = 0.21f;
        }
    }
}