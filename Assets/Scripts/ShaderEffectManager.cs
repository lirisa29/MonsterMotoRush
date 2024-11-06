using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ShaderEffectManager : MonoBehaviour
{
    public UniversalRenderPipelineAsset urpPipelineAsset;

    public FullScreenPassRendererFeature shaderEffect1Feature;
    public FullScreenPassRendererFeature shaderEffect2Feature;
    public FullScreenPassRendererFeature shaderEffect3Feature;
    public FullScreenPassRendererFeature shaderEffect4Feature;
    
    
    enum TargetScene
    {
        MainMenu,
        Tut,
        Level1,
        Level2,
        Level3
    }
    
    [SerializeField] private TargetScene targetScene;

    private void Start()
    {
        DisableAllRenderFeatures();
        
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                shaderEffect1Feature.SetActive(true);
                break;
            
            case TargetScene.Tut:
                shaderEffect2Feature.SetActive(true);
                break;
            
            case TargetScene.Level1:
                shaderEffect2Feature.SetActive(true);
                break;
            
            case TargetScene.Level2:
                shaderEffect3Feature.SetActive(true);
                break;
            
            case TargetScene.Level3:
                shaderEffect4Feature.SetActive(true);
                break;
        }
        
        urpPipelineAsset.SetDirty();
    }

    void DisableAllRenderFeatures()
    {
        shaderEffect1Feature.SetActive(false);
        shaderEffect2Feature.SetActive(false);
        shaderEffect3Feature.SetActive(false);
        shaderEffect4Feature.SetActive(false);
    }
}
