using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject instructionsUI;
    public Button closeInstructionsButton;
    
    void Start()
    {
        ShowInstructions();
        AddEvents();
    }

    void AddEvents()
    {
        closeInstructionsButton.onClick.RemoveAllListeners();
        closeInstructionsButton.onClick.AddListener(CloseInstructions);
    }

    private void ShowInstructions()
    {
        instructionsUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInstructions()
    {
        instructionsUI.SetActive(false);
        Time.timeScale = 1;
    }
}
