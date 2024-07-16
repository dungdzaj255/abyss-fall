using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupMenu : MonoBehaviour
{
    public static StartupMenu instance;

    [SerializeField] private Transform hightScoreWindow;
    [SerializeField] private Transform menuWindow;
    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private Transform playBtn;
    [SerializeField] private Transform volumnBtn;
    [SerializeField] private Transform scoreBoardItemPrefab;
    [SerializeField] private Transform scoreBoardItemContainer;
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
        hightScoreWindow.gameObject.SetActive(false);
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
        AudioManager.instance.PlayBackGroundMusic();
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
        PlayerPrefs.SetString("playerName", nameInput.text);
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
            AudioManager.instance.Muted();
        } else {
            volumnBtn.GetComponent<Image>().sprite = volumnBtn_unmuted;
            AudioManager.instance.UnMuted();
        }
        isMuted = !isMuted;
    }

    public void handleClickHighScoreBtn() {
        menuWindow.gameObject.SetActive(false);
        hightScoreWindow.gameObject.SetActive(true);
        handleClickLeaderBoard();
    }

    public void handleClickLeaderBoard() {
        List<ScoreBoardItem> items = LoadFromScoreBoard();
        for (int i = 0; i < scoreBoardItemContainer.childCount; i++) {
            Destroy(scoreBoardItemContainer.GetChild(i).gameObject);
        }
        if (items.Count > 0) {
            for (int i = 0; i < items.Count; i++) {
                Transform newItem = Instantiate(scoreBoardItemPrefab, scoreBoardItemContainer);
                newItem.GetChild(0).GetComponent<Text>().text = items[i].PlayerName;
                newItem.GetChild(1).GetComponent<Text>().text = items[i].PlayerScore + "";

                // Set position of the new item at the top center
                RectTransform newItemRect = newItem.GetComponent<RectTransform>();
                float containerWidth = scoreBoardItemContainer.GetComponent<RectTransform>().rect.width;
                float itemWidth = newItemRect.rect.width;
                float xPosition = (containerWidth - itemWidth) / 2f; // Center horizontally
                float yPosition = -i * newItemRect.rect.height; // Stack items vertically

                newItemRect.anchoredPosition = new Vector2(xPosition, yPosition);

                // Optionally, you can set other properties of newItem based on items[i]

                newItem.gameObject.SetActive(true);
            }
        }
    }

    public void returnToMainMenu() {
        menuWindow.gameObject.SetActive(true);

        hightScoreWindow.gameObject.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }


    private List<ScoreBoardItem> LoadFromScoreBoard() {
        string filePath = Path.Combine(Application.persistentDataPath, "score-board.json");
        if (File.Exists(filePath)) {
            string jsonContent = File.ReadAllText(filePath);
            var scoreboard = JsonConvert.DeserializeObject<Dictionary<string, List<ScoreBoardItem>>>(jsonContent);
            List<ScoreBoardItem> scoreboardItems = scoreboard["items"];
            return scoreboardItems;
        } else {
            return new List<ScoreBoardItem>();
        }
    }

}
