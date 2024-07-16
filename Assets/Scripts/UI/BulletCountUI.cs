using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountUI : MonoBehaviour {
    [SerializeField] private Transform bulletUI_container;
    [SerializeField] private Transform bulletUI_prefab;
    [SerializeField] private Text textCount;
    private float bulletUI_prefab_height;
    private float bulletUI_container_height;
    private Transform[] bullets;
    public void UpdateBar(int max, int curr) {
        DestroyAll();
        bulletUI_container_height = bulletUI_container.GetComponent<RectTransform>().rect.height - (2.5f * (max - 1));
        if (bulletUI_container_height != 0f) {
            bulletUI_prefab_height = bulletUI_container_height / max;
            for (int i = 0; i < curr; i++) {
                Transform newBullet = Instantiate(bulletUI_prefab, bulletUI_container);

                float yOffset = (bulletUI_prefab_height + 2.5f) * i;
                float yPos = yOffset + (bulletUI_prefab_height / 2);

                newBullet.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);

                newBullet.GetComponent<RectTransform>().sizeDelta 
                    = new Vector2(newBullet.GetComponent<RectTransform>().sizeDelta.x, bulletUI_prefab_height);
            }
        }
        textCount.text = curr + "";
    }

    public void DecreaseByOne(int curr) {
        BulletToArray();
        Transform bullet = BulletOntop();
        if (bullet != null) {
            bullet.GetComponent<Animator>().Play("Remove");
            StartCoroutine(DestroyBulletAfterDelay(bullet.gameObject, 0.1f));
            textCount.text = curr + "";
        }
    }
    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void BulletToArray() {
        int childCount = bulletUI_container.transform.childCount;
        bullets = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            bullets[i] = bulletUI_container.transform.GetChild(i);
        }
    }
    private Transform BulletOntop() {
        if (bullets != null) {
            return bullets[bullets.Length - 1];
        } else {
            return null;
        }
    }

    private void DestroyAll() {
        BulletToArray();
        for (int i = 0; i < bullets.Length; i++) {
            Destroy(bullets[i].gameObject);
        }
    }
}
