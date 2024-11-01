using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Shop Events")] 
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;
    void Start()
    {
        AddShopEvents();
    }

    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);
        
        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
