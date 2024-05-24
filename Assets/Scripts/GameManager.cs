using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
    public int levelStartScore;
    public Text scoreText;
    public Text scoreNumberText;
    public GameObject gameOverUI;
    public GameObject finishUI;
    public GameObject homeUI;
    public GameObject pauseButton;


    private bool isGameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(false);
        finishUI.SetActive(false);
        homeUI.SetActive(true);
        pauseButton.SetActive(false);

        LoadScore();
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
    }

    public void restartGame()
    {
        score = levelStartScore;
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartGame()
    {
        isGameStarted = true;
        homeUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        pauseButton.SetActive(false) ;
        scoreNumberText.text = score.ToString();
    }

    public void NextLevel()
    {
        finishUI.SetActive(true);
        pauseButton.SetActive(false ) ;
        SaveLevelScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentScore", score);
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

    private void SaveLevelScore()
    {
        PlayerPrefs.SetInt("LevelStartScore", score);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("CurrentScore");
        PlayerPrefs.DeleteKey("LevelStartScore");
    }
}
