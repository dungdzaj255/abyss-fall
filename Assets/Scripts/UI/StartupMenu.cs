using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupMenu : MonoBehaviour
{
    public static StartupMenu instance;

    [SerializeField] private Transform hightScoreWindow;
    [SerializeField] private Transform menuWindow;

    [SerializeField] private Transform playBtn;
    [SerializeField] private Transform volumnBtn;
    [SerializeField] private Sprite volumnBtn_muted;
    [SerializeField] private Sprite volumnBtn_unmuted;

    [SerializeField] private Sprite defaultPrimaryBtn_sprite;
    [SerializeField] private Sprite defaultSecondaryBtn_sprite;
    [SerializeField] private Sprite defaultSquarePrimaryBtn_sprite;
    [SerializeField] private Sprite defaultSquareSecondaryBtn_sprite;
    [SerializeField] private Sprite disabledBtn_sprite;
    [SerializeField] private Sprite disabledSquareBtn_sprite;

    public bool isMuted = false;

    private void Start() {
        PlayerPrefs.SetString("isMuted", isMuted.ToString());
    }

    public void Init() {
        instance = this;
        try {
            isMuted = bool.Parse(PlayerPrefs.GetString("isMuted"));
        } catch (Exception) {
            
        }
    }

    private void Awake() {
        Init();
        if (isMuted) {
            volumnBtn.GetComponent<Image>().sprite = volumnBtn_muted;
        } else {
            volumnBtn.GetComponent<Image>().sprite = volumnBtn_unmuted;
        }
        playBtn.GetComponent<Button>().onClick.AddListener(PlayGame);
        volumnBtn.GetComponent<Button>().onClick.AddListener(handleClickVolumnBtn);

        hightScoreWindow.gameObject.SetActive(false);
    }

    public void PlayGame() {
        SceneManager.LoadSceneAsync(1);
        PlayerPrefs.SetString("isMuted", isMuted.ToString());
    }

    public void Exit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void handleClickVolumnBtn() {
        if (!isMuted) {
            volumnBtn.GetComponent<Image>().sprite = volumnBtn_muted;
        } else {
            volumnBtn.GetComponent<Image>().sprite = volumnBtn_unmuted;
        }
        isMuted = !isMuted;
    }

    public void handleClickHighScoreBtn() {
        menuWindow.gameObject.SetActive(false);
        hightScoreWindow.gameObject.SetActive(true);
        hightScoreWindow.gameObject.transform.position = new Vector2(hightScoreWindow.gameObject.transform.position.x, 300);
    }

    public void returnToMainMenu() {
        menuWindow.gameObject.SetActive(true);

        hightScoreWindow.gameObject.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

}
