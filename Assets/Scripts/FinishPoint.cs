using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
            Time.timeScale = 0;
            audioManager.PlaySFX(audioManager.win);
            winScreen.SetActive(true);
            UnlockNewLevel();
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
