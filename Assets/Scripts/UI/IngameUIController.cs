using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static IngameUIController;

public class IngameUIController : MonoBehaviour {
    public static IngameUIController instance;

    [SerializeField] private Sprite mutedBtn_sprite;
    [SerializeField] private Sprite unmutedBtn_sprite;
    [SerializeField] private Sprite pausedBtn_sprite;
    [SerializeField] private Sprite unpausedBtn_sprite;

    [SerializeField] private Transform mutedBtnOnPauseMenu;
    [SerializeField] private Transform mutedBtnOnGameOverMenu;
    [SerializeField] private Transform pausedBtn;
    [SerializeField] private Transform pausedMenu;
    [SerializeField] private Transform gameOverMenu;
    [SerializeField] private Transform pointCounter;
    [SerializeField] private Transform pointCounter_position_default;
    [SerializeField] private Transform pointCounter_position_on_menu;
    [SerializeField] private Transform pointCounter_position_on_GAMEOVER;

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
            mutedBtnOnPauseMenu.GetComponent<Image>().sprite = mutedBtn_sprite;
            mutedBtnOnGameOverMenu.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtnOnPauseMenu.GetComponent<Image>().sprite = unmutedBtn_sprite;
            mutedBtnOnGameOverMenu.GetComponent<Image>().sprite = unmutedBtn_sprite;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            player.TakeDamage(1000f);
        }
    }
    private void FixedUpdate() {
        if (player.GetCurrentHealth() <= 0) {
            GameOver();
        }
    }

    public void handleMute() {
        if (!isMuted) {
            mutedBtnOnPauseMenu.GetComponent<Image>().sprite = mutedBtn_sprite;
            mutedBtnOnGameOverMenu.GetComponent<Image>().sprite = mutedBtn_sprite;
        } else {
            mutedBtnOnPauseMenu.GetComponent<Image>().sprite = unmutedBtn_sprite;
            mutedBtnOnGameOverMenu.GetComponent<Image>().sprite = unmutedBtn_sprite;
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
        pointCounter.transform.position = pointCounter_position_on_GAMEOVER.transform.position;
        pointCounter.transform.GetChild(0).GetComponent<Image>().enabled = false;
        SaveToScoreBoard();
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


    private void SaveToScoreBoard() {
        ScoreBoardItem scoreBoardItem = new ScoreBoardItem();
        scoreBoardItem.PlayerName = PlayerPrefs.GetString("playerName");
        if (string.IsNullOrEmpty(scoreBoardItem.PlayerName) || scoreBoardItem.PlayerName.Equals("\n")) {
            scoreBoardItem.PlayerName = "Player";
        }
        try {
            scoreBoardItem.PlayerScore =
                Int32.Parse(pointCounter.transform.GetChild(2).GetComponent<Text>().text);
        } catch (Exception) {
            scoreBoardItem.PlayerScore = 0;
        }
        string jsonString = JsonUtility.ToJson(scoreBoardItem, true);

        List<ScoreBoardItem> existScores = LoadFromScoreBoard();
        if (existScores.Count > 0 && scoreBoardItem.PlayerScore >= existScores[0].PlayerScore) {
            if (existScores.Count == 5) {
                existScores.Remove(existScores[existScores.Count - 1]);
            }
        }
        existScores.Add(scoreBoardItem);
        existScores.Sort((x, y) => y.PlayerScore.CompareTo(x.PlayerScore));
        ScoreBoard scoreBoard = new ScoreBoard();
        scoreBoard.items = new ScoreBoardItem[existScores.Count];
        for (int i = 0; i < existScores.Count; i++) {
            scoreBoard.items[i] = existScores[i];
        }
        string json = JsonConvert.SerializeObject(scoreBoard, Formatting.Indented);

        Debug.Log(json);

        string filePath = Path.Combine(Application.persistentDataPath, "score-board.json");

        File.WriteAllText(filePath, json);
    }
}
