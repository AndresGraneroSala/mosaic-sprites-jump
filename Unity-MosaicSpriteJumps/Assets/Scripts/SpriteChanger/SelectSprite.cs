using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class SelectSprite 
{



    public Sprite ChangeSprite(string nameFile)
    {



        string path = $"{Application.streamingAssetsPath}/default/{nameFile}";
        //Debug.Log(path);
        
        // Cargar la textura desde la ruta
        Texture2D texture = new Texture2D(2, 2); // Crea una textura temporal
        byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            texture.LoadImage(fileData); // Carga la imagen desde la ruta
        }

        // Convierte la textura en un sprite
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            100,      // pixelsPerUnit
            0,        // extrude
            SpriteMeshType.FullRect, // meshType
            new Vector4(0,0,0,0),// border
            false //generateFallbackPhysicsShape
            
        );
        sprite.texture.wrapMode = TextureWrapMode.Clamp;




        if (ConfigurationSprites.SharedInstance.ConfigurationSpritesData.isPixelArt|| texture.width <=128||texture.height <=128)
        {
            sprite.texture.filterMode = FilterMode.Point;
        }

        return sprite;





    }
    
    
    public IEnumerator ChangeSpriteWeb(string resourceName, Action<Sprite> onComplete)
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
        
        
        
         
        
        string imageUrl = $"{urlWihtoutParameter}StreamingAssets/{parameterInUrl}{resourceName}"; 

        
        
        
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

        

        
        // Envía la solicitud y espera a que se complete
        www.SendWebRequest();

        // Espera hasta que la descarga esté completa
        while (!www.isDone)
        {
            // Puedes agregar lógica de espera opcional aquí si es necesario
            yield return null;

        }

        // Verifica si ocurrieron errores durante la descarga
        if (www.result ==UnityWebRequest.Result.ConnectionError) 
        {
            Debug.LogError("Error al descargar la imagen: " + www.error);
        }
        else
        {
            // La descarga se completó con éxito, así que crea un Sprite a partir de la textura descargada
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            
            
            if (ConfigurationSprites.SharedInstance.ConfigurationSpritesData.isPixelArt|| texture.width <=128||texture.height <=128)
            {
                sprite.texture.filterMode = FilterMode.Point;
            }
            sprite.texture.wrapMode = TextureWrapMode.Clamp;

            onComplete?.Invoke(sprite);
        }

        yield return null;
        
    }


    public IEnumerator ChangeSpriteMultiplatform(string resourceName, Action<Sprite> onComplete)
    {
        Sprite sprite=null;

#if UNITY_EDITOR

        //Debug.Log("no web");
            sprite = ChangeSprite(resourceName);
#elif UNITY_WEBGL
        yield return ChangeSpriteWeb(resourceName, (spriteChanged =>
        {
            sprite = spriteChanged;
        }));
        //Debug.Log("web");
#else
        //Debug.Log("no web");
            sprite = ChangeSprite(resourceName);
#endif
        
        
        onComplete?.Invoke(sprite);
        yield return null;

    }
}
