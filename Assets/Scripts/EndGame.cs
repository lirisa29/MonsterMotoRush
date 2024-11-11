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
     private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
            Debug.Log("Skill issue");
            GameOver();
        
        //finalScore.text = "Score: "+ fm.score.ToString();
    }

    public void GameOver()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        }
        
        Time.timeScale = 0;
        audioManager.PlaySFX(audioManager.death);
        GameDataManager.LostCoins(bc.collectedCoins);
        GameSharedUI.Instance.UpdateCoinsUIText();
        gameOverScreen.SetActive(true);
    }
}
