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
    
    enum TargetScene
    {
        MainMenu,
        Store
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
            
            case TargetScene.Store:
                shaderEffect2Feature.SetActive(true);
                break;
        }
        
        urpPipelineAsset.SetDirty();
    }

    void DisableAllRenderFeatures()
    {
        shaderEffect1Feature.SetActive(false);
        shaderEffect2Feature.SetActive(false);
    }
}
