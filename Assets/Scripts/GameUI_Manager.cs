using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_Manager : MonoBehaviour
{
    [SerializeField] private Image levelSlider; // Main image of the slider
    [SerializeField] private Image levelSliderFill; // Fill image of the slider
    [SerializeField] private Image currentLevelImage; // Image that contains current level text
    [SerializeField] private Text currentLevelText; // Text for the current level
    [SerializeField] private Image nextLevelImage; // Image that contains next level text
    [SerializeField] private Text nextLevelText; // Text for the next level
    [SerializeField] private Transform ball; // The ball object
    [SerializeField] private Transform startPoint; // The starting point of the level
    [SerializeField] private Transform endPoint; // The end point of the level

    private float totalDistance;
    private int currentLevel ;
    private int nextLevel ;

    void Start()
    {
        if (levelSlider == null || levelSliderFill == null || currentLevelImage == null || currentLevelText == null || nextLevelImage == null || nextLevelText == null || ball == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("One or more serialized fields are not assigned in the inspector.");
            return;
        }

        LoadLevelData();

        // Calculate the total distance
        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);

        // Set initial level text values
        UpdateLevelText();

        // Initialize the fill amount of the slider
        levelSliderFill.fillAmount = 0f;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        if (ball == null || startPoint == null || endPoint == null)
        {
            Debug.LogWarning("Ball, startPoint or endPoint has been destroyed or not assigned.");
            return;
        }

        // Calculate the distance covered by the ball
        float distanceCovered = Vector3.Distance(startPoint.position, ball.position);

        // Calculate the progress percentage
        float progress = distanceCovered / totalDistance;

        // Update the fill amount of the slider
        levelSliderFill.fillAmount = Mathf.Clamp01(progress);

        if (progress >= 1f)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        // Increment the levels
        currentLevel++;
        nextLevel = currentLevel + 1;

        SaveLevelData();

        // Reset the ball position to the start point (or handle it as needed)
        ball.position = startPoint.position;

        // Update the progress bar
        levelSliderFill.fillAmount = 0f;

        // Update level text
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = nextLevel.ToString();
    }

    private void SaveLevelData()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("NextLevel", nextLevel);
        PlayerPrefs.Save();
    }

    private void LoadLevelData()
    {
        if (PlayerPrefs.HasKey("CurrentLevel") && PlayerPrefs.HasKey("NextLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            nextLevel = PlayerPrefs.GetInt("NextLevel");
        }
        else
        {
            currentLevel = 1; // Starting level
            nextLevel = 2; // Next level
        }
    }

    public void ResetLevel()
    {
        currentLevel = 1;
        nextLevel = currentLevel + 1;
        SaveLevelData() ;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("NextLevel");
    }


}
