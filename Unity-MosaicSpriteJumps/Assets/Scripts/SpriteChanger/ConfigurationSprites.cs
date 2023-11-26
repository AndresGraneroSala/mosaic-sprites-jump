using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationSprites : MonoBehaviour
{
    public static ConfigurationSprites SharedInstance;
    public ConfigurationSpritesData ConfigurationSpritesData;
    
    
    private void Awake()
    {
        if (SharedInstance!=null)
        {
            Destroy(SharedInstance);
        }

        SharedInstance = this;

        
    }
    
    
    [ContextMenu("see json")]
    void SeeJson()
    {
        print( JsonUtility.ToJson(new ConfigurationSpritesData()));
    }
}
[System.Serializable]
public class ConfigurationSpritesData
{
    
    public bool isLoaded = false;
    public bool isPixelArt;
    public float xPlayerMove;
    public float xPlayerDead;
    public float yPlayerDead;
    public int maxFPS;
    public int framesWalk;
    public int framesRun;
    public int framesIdle;
    public int framesJump;
    public int framesDead;
    public float delayFramesInSeconds;
    public float timeStartRun;
    public string spritesBy;
    public string spritesByUrl;
}