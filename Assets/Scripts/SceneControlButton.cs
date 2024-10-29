using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneControlButton : MonoBehaviour
{
    enum TargetScene
    {
        MainMenu,
        Store
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
            
            case TargetScene.Store:
                button.onClick.AddListener(() => SceneController.LoadStore());
                break;
        }
    }
}
