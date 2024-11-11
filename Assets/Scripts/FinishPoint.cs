using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TimerUI timerUI;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
            Time.timeScale = 0;
            audioManager.PlaySFX(audioManager.win);
            winScreen.SetActive(true);

            float currentTime = timerUI.GetRemainingTime();
            timerUI.SaveBestTimeForLevel(currentTime);

            DisplayTimes(currentTime);
            
            UnlockNewLevel();
    }

    private void DisplayTimes(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        currentTimeText.text = string.Format("Current Time: {0:00}:{1:00}", minutes, seconds);

        float bestTime = timerUI.GetBestTimeForLevel();
        if (bestTime < float.MaxValue)
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60);
            int bestSeconds = Mathf.FloorToInt(bestTime % 60);
            bestTimeText.text = string.Format("Best Time: {0:00}:{1:00}", bestMinutes, bestSeconds);
        }
        else
        {
            bestTimeText.text = "Best Time: --:--";
        }
    }

    void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = GameDataManager.GetUnlockedLevel();  // Use GameDataManager to get the unlocked level

        // If the player has reached or surpassed the current scene, unlock the next level
        if (currentSceneIndex >= reachedIndex)
        {
            int nextLevel = currentSceneIndex + 1;

            // Set the new unlocked level in GameDataManager
            GameDataManager.SetUnlockedLevel(nextLevel);

            Debug.Log($"New level unlocked! Current scene: {currentSceneIndex}, Next unlocked level: {nextLevel}");
        }
    }
}
