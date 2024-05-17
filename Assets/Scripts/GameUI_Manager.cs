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

    void Start()
    {
        if (levelSlider == null || levelSliderFill == null || currentLevelImage == null || currentLevelText == null || nextLevelImage == null || nextLevelText == null || ball == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("One or more serialized fields are not assigned in the inspector.");
            return;
        }

        // Calculate the total distance
        totalDistance = Vector3.Distance(startPoint.position, endPoint.position);

        // Set initial level text values
        currentLevelText.text = "1"; // Example current level
        nextLevelText.text = "2"; // Example next level

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
    }
}
