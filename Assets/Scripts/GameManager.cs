using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
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
    }

    public void restartGame()
    {
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

    }
}
