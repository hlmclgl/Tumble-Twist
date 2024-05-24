using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameManager gm;
    private GameUI_Manager ui;

    private bool isPaused = false;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        ui = GameObject.FindObjectOfType<GameUI_Manager>();
    }

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
        gm.ResetScore();
        ui.ResetLevel();
        SceneManager.LoadScene("StartMenu"); // Ana menü sahnesine geçiþ yapar
    }

    private void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

   
}
