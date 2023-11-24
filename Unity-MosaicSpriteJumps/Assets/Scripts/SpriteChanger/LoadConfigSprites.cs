using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadConfigSprites : MonoBehaviour
{
    [SerializeField] private GameObject Game;


    private void Awake()
    {
#if UNITY_EDITOR
        LoadConfig();
#elif UNITY_WEBGL
        StartCoroutine(LoadConfigWeb());
#else
        LoadConfig();
#endif


    }



    public void LoadConfig()
    {
        string jsonConfig = File.ReadAllText($"{Application.streamingAssetsPath}/default/config.txt");
        
        ConfigurationSprites.SharedInstance.ConfigurationSpritesData =
            JsonUtility.FromJson<ConfigurationSpritesData>(jsonConfig);
        
        Game.SetActive(true);

    }

    IEnumerator LoadConfigWeb()
    {
        
        string urlAll = Application.absoluteURL;
        int indiceHash = urlAll.IndexOf('#');
        string urlWihtoutParameter = "";
        string parameterInUrl = "default/";
        
        if (indiceHash >= 0)
        {
            urlWihtoutParameter = urlAll.Substring(0, indiceHash);
            parameterInUrl = $"{urlAll.Substring(indiceHash + 1)}/";
        }
        else
        {
            urlWihtoutParameter = urlAll;
        }
        
        
        string urlConfig = $"{urlWihtoutParameter}StreamingAssets/{parameterInUrl}config.txt";
        
        
        using (UnityWebRequest www = UnityWebRequest.Get(urlConfig))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error al cargar el JSON desde la URL: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                ConfigurationSprites.SharedInstance.ConfigurationSpritesData =
                    JsonUtility.FromJson<ConfigurationSpritesData>(json);
                
                //yield return new WaitForSeconds(0.5f);
                
                Game.SetActive(true);
            }
        }
        

        yield return null;
    }




}
