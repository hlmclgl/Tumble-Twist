using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
    public int bestScore;
    public int levelStartScore;
    public Text scoreText;
    public Text scoreNumberText;
    public Text bestScoreNumberText;
    public GameObject gameOverUI;
    public GameObject finishUI;
    public GameObject homeUI;
    public GameObject pauseButton;


    private bool isGameStarted = false;
    private bool isLevelCompleted = false;
    void Start()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(false);
        finishUI.SetActive(false);
        homeUI.SetActive(true);
        pauseButton.SetActive(false);

        LoadScore();
        LoadBestScore();

        if (PlayerPrefs.HasKey("LevelCompleted"))
        {
            PlayerPrefs.DeleteKey("LevelCompleted"); 
            ShowFinishUI(); 
        }
    }

    private void Update()
    {
        if (!isGameStarted && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartGame();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (gameOverUI.activeSelf)
            {
                restartGame();
            }
            else if (finishUI.activeSelf && isLevelCompleted)
            {
                StartGame();
            }
        }
    }

    public void RestartButtonClicked()
    {
        restartGame();
    }

    public void gameScore(int ringScore)
    {
        score += ringScore;
        scoreText.text = score.ToString();
        SaveScore();

        if (score > bestScore)
        {
            bestScore = score;
            bestScoreNumberText.text = bestScore.ToString();
            SaveBestScore();
        }
    }

    public void restartGame()
    {
        score = levelStartScore;
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.UnPauseMusic();
    }

    private void StartGame()
    {
        isGameStarted = true;
        homeUI.SetActive(false);
        finishUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        AudioManager.Instance.UnPauseMusic();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        pauseButton.SetActive(false);
        scoreNumberText.text = score.ToString();
        bestScoreNumberText.text = bestScore.ToString();
    }

    public void NextLevel()
    {
        finishUI.SetActive(true);
        pauseButton.SetActive(false);
        AudioManager.Instance.PauseMusic();

        GameUI_Manager gameUIManager = FindObjectOfType<GameUI_Manager>();
        if (gameUIManager != null)
        {
            gameUIManager.LevelCompleted();
        }

        SaveLevelScore();
        PlayerPrefs.SetInt("LevelCompleted", 1); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ShowFinishUI() 
    {
        finishUI.SetActive(true);
        isLevelCompleted = true;
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentScore", score);
        PlayerPrefs.Save();
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("CurrentScore"))
        {
            score = PlayerPrefs.GetInt("CurrentScore");
            scoreText.text = score.ToString();
        }

        if (PlayerPrefs.HasKey("LevelStartScore"))
        {
            levelStartScore = PlayerPrefs.GetInt("LevelStartScore");
        }
        else
        {
            levelStartScore = score;
        }
    }

    private void LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreNumberText.text = bestScore.ToString();
        }
    }

    private void SaveLevelScore()
    {
        PlayerPrefs.SetInt("LevelStartScore", score);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        score = 0;
        levelStartScore = 0;
        SaveScore();
        SaveLevelScore();
    }


    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("CurrentScore");
        PlayerPrefs.DeleteKey("LevelStartScore");
    }
}
