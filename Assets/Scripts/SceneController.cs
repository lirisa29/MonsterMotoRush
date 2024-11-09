using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static int mainScene = 0;
    private static int tutorialScene = 1;
    private static int level1Scene = 2;
    private static int level2Scene = 3;
    private static int level3Scene = 4;
    private static int level4Scene = 5;
    private static int level5Scene = 6;
    private static int level6Scene = 7;
    
    public static void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }

    public static void LoadTutorial()
    {
        SceneManager.LoadScene(tutorialScene);
    }
    
    public static void LoadLevel1()
    {
        SceneManager.LoadScene(level1Scene);
    }
    
    public static void LoadLevel2()
    {
        SceneManager.LoadScene(level2Scene);
    }
    
    public static void LoadLevel3()
    {
        SceneManager.LoadScene(level3Scene);
    }
    
    public static void LoadLevel4()
    {
        SceneManager.LoadScene(level4Scene);
    }
    
    public static void LoadLevel5()
    {
        SceneManager.LoadScene(level5Scene);
    }
    
    public static void LoadLevel6()
    {
        SceneManager.LoadScene(level6Scene);
    }
    
    public static void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void QuitButton()
    {
        Application.Quit();
    }
}

