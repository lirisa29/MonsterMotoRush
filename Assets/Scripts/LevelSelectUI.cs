using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text[] buttonsText;
    
    [Header("Events")] 
    [SerializeField] private GameObject levelSelectUI;
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button closeMenuButton;

    private void Start()
    {
        AddEvents();
        LockLevel();
    }

    void LockLevel()
    {
        int unlockedLevel = GameDataManager.GetUnlockedLevel();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttonsText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            buttonsText[i].gameObject.SetActive(true);
        }
    }
    
    void AddEvents()
    {
        openMenuButton.onClick.RemoveAllListeners();
        openMenuButton.onClick.AddListener(OpenMenu);

        closeMenuButton.onClick.RemoveAllListeners();
        closeMenuButton.onClick.AddListener(CloseMenu);
    }

    void OpenMenu()
    {
        levelSelectUI.SetActive(true);
    }

    void CloseMenu()
    {
        levelSelectUI.SetActive(false);
    }
}
