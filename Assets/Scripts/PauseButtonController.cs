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
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    private void ResumeGame()
    {
        AudioManager.Instance.PlaySFX("Play");
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void RestartGame()
    {
        AudioManager.Instance.PlaySFX("Restart");
        Time.timeScale = 1f; 
        restartGame();
    }

    public void GoToMainMenu()
    {
        AudioManager.Instance.PlaySFX("MainMenu");
        gm.ResetScore();
        ui.ResetLevel();
        SceneManager.LoadScene("StartMenu"); 
    }

    private void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.PlaySFX("Music");
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.PlaySFX("SFX");
        AudioManager.Instance.ToggleSFX();
    }


}
