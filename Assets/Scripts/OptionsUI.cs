using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsUI : MonoBehaviour
{
    [Header("Resolution Settings")]
    private Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bikeSlider;
    
    [Header("Events")] 
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void Start()
    {
        AddEvents();
        
        page2.SetActive(false);
        page1.SetActive(true);
        
        if (GameDataManager.GetMusicVolume() != -1)
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetBikeVolume();
            SetSFXVolume();
        }

        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        GameDataManager.SetMusicVolume(volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        GameDataManager.SetSFXVolume(volume);
    }
    
    public void SetBikeVolume()
    {
        float volume = bikeSlider.value;
        myMixer.SetFloat("bike", Mathf.Log10(volume)*20);
        GameDataManager.SetBikeVolume(volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = GameDataManager.GetMusicVolume();
        sfxSlider.value = GameDataManager.GetSFXVolume();
        bikeSlider.value = GameDataManager.GetBikeVolume();
        
        SetMusicVolume();
        SetBikeVolume();
        SetSFXVolume();
    }
    
    void AddEvents()
    {
        openMenuButton.onClick.RemoveAllListeners();
        openMenuButton.onClick.AddListener(OpenMenu);

        closeMenuButton.onClick.RemoveAllListeners();
        closeMenuButton.onClick.AddListener(CloseMenu);
        
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextButton);
        
        prevButton.onClick.RemoveAllListeners();
        prevButton.onClick.AddListener(PrevButton);
    }

    void OpenMenu()
    {
        audioManager.PlaySFX(audioManager.buttons);
        optionsUI.SetActive(true);
    }

    void CloseMenu()
    {
        audioManager.PlaySFX(audioManager.buttons);
        optionsUI.SetActive(false);
    }

    void NextButton()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    void PrevButton()
    {
        page2.SetActive(false);
        page1.SetActive(true);
    }
}
