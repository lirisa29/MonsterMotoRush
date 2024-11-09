using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneControlButton : MonoBehaviour
{
    enum TargetScene
    {
        MainMenu,
        Tutorial,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Retry,
        Quit
    }

    [SerializeField] private TargetScene targetScene;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.RemoveAllListeners();
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                button.onClick.AddListener(() => SceneController.LoadMainScene());
                break;
            
            case TargetScene.Tutorial:
                button.onClick.AddListener(() => SceneController.LoadTutorial());
                break;
            
            case TargetScene.Level1:
                button.onClick.AddListener(() => SceneController.LoadLevel1());
                break;
            
            case TargetScene.Level2:
                button.onClick.AddListener(() => SceneController.LoadLevel2());
                break;
            
            case TargetScene.Level3:
                button.onClick.AddListener(() => SceneController.LoadLevel3());
                break;
            
            case TargetScene.Level4:
                button.onClick.AddListener(() => SceneController.LoadLevel4());
                break;
            
            case TargetScene.Level5:
                button.onClick.AddListener(() => SceneController.LoadLevel5());
                break;
            
            case TargetScene.Level6:
                button.onClick.AddListener(() => SceneController.LoadLevel6());
                break;
            
            case TargetScene.Retry:
                button.onClick.AddListener(() => SceneController.RetryButton());
                break;
            
            case TargetScene.Quit:
                button.onClick.AddListener(() => SceneController.QuitButton());
                break;
        }
    }
}
