using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        this.enabled = true;
    }

    void Update()
    {

    }

    void OnEnable()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
