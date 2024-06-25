using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour {
    public static IngameUIController instance;

    [SerializeField] private Sprite mutedBtn_sprite;
    [SerializeField] private Sprite unmutedBtn_sprite;
    [SerializeField] private Sprite pausedBtn_sprite;
    [SerializeField] private Sprite unpausedBtn_sprite;

    [SerializeField] private Transform mutedBtn;
    [SerializeField] private Transform pausedBtn;
    [SerializeField] private Transform pausedMenu;

    private bool isMuted = false;
    public bool isPaused = false;

    private void Awake() {
        instance = this;
        if (StartupMenu.instance != null) {
            isMuted = StartupMenu.instance.isMuted;
        }

        if (isMuted ) {
            mutedBtn.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtn.GetComponent<Image>().sprite = unmutedBtn_sprite;
        }
    }

    public void handleMute() {
        if (!isMuted) {
            mutedBtn.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtn.GetComponent<Image>().sprite = unmutedBtn_sprite;
        }
        isMuted = !isMuted;
        if (StartupMenu.instance != null) {
            StartupMenu.instance.isMuted = isMuted;
        }
    }

    public void handlePause() {
        if (!isPaused) {
            pausedBtn.GetComponent<Image>().sprite = pausedBtn_sprite;
            pausedMenu.gameObject.transform.position = new Vector2(pausedMenu.transform.position.x, 300);
            pausedMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        } else {
            pausedBtn.GetComponent<Image>().sprite = unpausedBtn_sprite;
            pausedMenu.gameObject.SetActive(false);

            Time.timeScale = 1f;
        }
        isPaused = !isPaused;
    }

    public void RestartGame() {
        if (StartupMenu.instance != null) {
            StartupMenu.instance.RestartGame();
        } else {
            SceneManager.LoadSceneAsync(1);
        }
        Time.timeScale = 1f;
        isPaused = false; 
    }
}
