using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountUI : MonoBehaviour {
    [SerializeField] private Transform bulletUI_container;
    [SerializeField] private Transform bulletUI_prefab;
    private float bulletUI_prefab_height;
    private float bulletUI_container_height;
    public void Init(int max, int curr) {
        bulletUI_container_height = bulletUI_container.GetComponent<RectTransform>().rect.height;
        SetUpBar(max, curr);
    }
    public void SetUpBar(int max, int curr) {
        if (bulletUI_container_height != 0f) {
            bulletUI_prefab_height = bulletUI_container_height / max;
            for (int i = 0; i < curr; i++) {
                Transform newBullet = Instantiate(bulletUI_prefab, bulletUI_container);

                float yOffset = (bulletUI_prefab_height) * i;
                float yPos = -(bulletUI_container_height / 2) + yOffset + (bulletUI_prefab_height / 2);

                newBullet.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);

                newBullet.GetComponent<RectTransform>().sizeDelta 
                    = new Vector2(newBullet.GetComponent<RectTransform>().sizeDelta.x, bulletUI_prefab_height);
            }
        }
    }
}
