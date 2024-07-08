using System;
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
    [SerializeField] private Transform gameOverMenu;
    [SerializeField] private Transform pointCounter;
    [SerializeField] private Transform pointCounter_position_default;
    [SerializeField] private Transform pointCounter_position_on_menu;

    public bool isMuted = false;
    public bool isPaused = false;
    private Vector2 defaultPosition_pointCounter;
    private Player player;

    private void Start() {
        pointCounter.transform.position = pointCounter_position_default.transform.position;
        pausedMenu.gameObject.SetActive(false);
    }

    public void Init() {
        instance = this;
        try {
            isPaused = false;
            isMuted = bool.Parse(PlayerPrefs.GetString("isMuted"));
        } catch (FormatException) {
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pointCounter.transform.position = defaultPosition_pointCounter;
    }

    private void Awake() {
        Init();
        Time.timeScale = 1f;
        if (isMuted ) {
            mutedBtn.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtn.GetComponent<Image>().sprite = unmutedBtn_sprite;
        }
    }

    private void Update() {
        if (player.GetCurrentHealth() <= 0 ) {
            GameOver();
        }
    }

    public void handleMute() {
        if (!isMuted) {
            mutedBtn.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtn.GetComponent<Image>().sprite = unmutedBtn_sprite;
        }
        isMuted = !isMuted;
    }

    public void handlePause() {
        if (!isPaused) {
            pausedBtn.GetComponent<Image>().sprite = pausedBtn_sprite;
            pausedMenu.gameObject.SetActive(true);
            pointCounter.transform.position = pointCounter_position_on_menu.transform.position;

            Time.timeScale = 0f;
        } else {
            pausedBtn.GetComponent<Image>().sprite = unpausedBtn_sprite;
            pausedMenu.gameObject.SetActive(false);
            pointCounter.transform.position = pointCounter_position_default.transform.position;

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

    public void ReturnMainMenu() {
        SceneManager.LoadSceneAsync(0);
        PlayerPrefs.SetString("isPaused", isPaused.ToString());
        PlayerPrefs.SetString("isMuted", isMuted.ToString());
    }

    public void GameOver() {
        Time.timeScale = 0f;
        gameOverMenu.gameObject.SetActive(true);
        pointCounter.transform.position = pointCounter_position_on_menu.transform.position;
    }
}
