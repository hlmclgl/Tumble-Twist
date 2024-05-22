using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonController : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    public void OnPauseButtonClick()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Oyunu duraklat
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Oyunu devam ettir
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Zamaný tekrar baþlat
        restartGame();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); // Ana menü sahnesine geçiþ yapar
    }

    private void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
