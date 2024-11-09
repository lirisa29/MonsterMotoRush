using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    public Button pauseButton;
    public Button resumeButton;

    private void Start()
    {
        pauseMenu.SetActive(false);
        AddEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
    
    void AddEvents()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(TogglePause);
        
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(ResumeGame);
    }
}
