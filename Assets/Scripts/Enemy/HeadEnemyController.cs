using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemyController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("head va cham voi bullet");
            EL1Health eL1Health = GetComponentInParent< EL1Health>();
            if (eL1Health != null)
            {
                eL1Health.TakeDamage(Weapon.Instance.damage);
                Debug.Log("enemy1 bi tru mau boi bullet qua head");
            }
            if (eL1Health == null)
            {
                Debug.LogError("EL1Health component not found in parent object!");
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("head va cham voi player");
            EL1Health eL1Health = GetComponentInParent<EL1Health>();
            if (eL1Health != null)
            {
                eL1Health.TakeDamage(3);
                Debug.Log("enemy1 bi tru mau boi player qua head");
            }
            if (eL1Health == null)
            {
                Debug.LogError("EL1Health component not found in parent object!");
            }
        }
    }
}
