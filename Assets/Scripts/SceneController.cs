using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static int mainScene = 0;

    private static int storeScene = 1;

    public static void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }

    public static void LoadStore()
    {
        SceneManager.LoadScene(storeScene);
    }
}

