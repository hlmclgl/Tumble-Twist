using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{

    public void playGame()
    {
        AudioManager.Instance.PlaySFX("Play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        AudioManager.Instance.PlaySFX("Exit");
        SceneManager.LoadScene("StartMenu");
    }

    public void settingsMenu()
    {
        AudioManager.Instance.PlaySFX("Options");
    }

    
}
