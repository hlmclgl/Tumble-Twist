using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
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
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Oyunu devam ettir
        isPaused = false;
    }
}
