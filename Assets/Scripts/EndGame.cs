using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField]private BikeController bc;
    [SerializeField]private FlipManager fm;
   

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Skill issue");
        Time.timeScale = 0;
       // GameDataManager.LostCoins(bc.collectedCoins);
       // GameSharedUI.Instance.UpdateCoinsUIText();
        gameOverScreen.SetActive(true);
        finalScore.text = "Score: "+ fm.score.ToString();
    }
}
