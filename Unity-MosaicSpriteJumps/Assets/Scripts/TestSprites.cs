using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class TestSprites : MonoBehaviour
{
    [SerializeField] SpriteRenderer ciruculo;
    [SerializeField] string nameFile;
    [SerializeField] private bool pixelArt;

    [SerializeField] private Text _text;





	public void Change()
	{
        
        
        
#if UNITY_EDITOR
        ChangeSprite(ciruculo,nameFile);
#elif UNITY_WEBGL
        StartCoroutine( ChangeSpriteWeb(ciruculo,nameFile));
        

#else
        ChangeSprite(ciruculo,nameFile);
#endif
	}

	void ChangeSprite(SpriteRenderer spriteRenderer,string nameFile)
    {
        
        
        
        string path = $"{Application.streamingAssetsPath}/{nameFile}";
        
        // Cargar la textura desde la ruta
        Texture2D texture = new Texture2D(2, 2); // Crea una textura temporal
        byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            texture.LoadImage(fileData); // Carga la imagen desde la ruta
        }

        // Convierte la textura en un sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));




        float widthScale = (float)spriteRenderer.sprite.texture.width/texture.width ;
        float heightScale = (float)spriteRenderer.sprite.texture.height / texture.height;

        //print(widthtScale);


        // Asigna el sprite al componente SpriteRenderer (o donde lo necesites)
        spriteRenderer.sprite = sprite;
        spriteRenderer.transform.localScale = new Vector3(widthScale*spriteRenderer.transform.localScale.x, heightScale *spriteRenderer.transform.localScale.y, 1);

        //pixel art
        if (pixelArt)
        {
            //spriteRenderer.sprite.pivot = new Vector2(0, 0);
            spriteRenderer.sprite.texture.filterMode = FilterMode.Point;
            
        }





    }

    public IEnumerator ChangeSpriteWeb(SpriteRenderer spriteRenderer, string resourceName)
    {

        string urlAll = Application.absoluteURL;
        int indiceHash = urlAll.IndexOf('#');
        string urlWihtoutParameter = "";
        string parameterInUrl = "";
        
        if (indiceHash >= 0)
        {
            urlWihtoutParameter = urlAll.Substring(0, indiceHash);
            parameterInUrl = $"{urlAll.Substring(indiceHash + 1)}/";
        }
        else
        {
            urlWihtoutParameter = urlAll;
        }
        
        
        
         
        
        string imageUrl = $"{urlWihtoutParameter}StreamingAssets/{parameterInUrl}{resourceName}"; // Reemplaza con la URL de tu imagen
        //imageUrl = "https://test-url-e89fe.web.app/StreamingAssets/cara.png";
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

        _text.text = urlWihtoutParameter;

        
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

            // Ahora puedes asignar el Sprite a un SpriteRenderer, Image, o cualquier otro componente que lo requiera
            // Ejemplo:
            
            float widthScale = (float)spriteRenderer.sprite.texture.width/texture.width ;
            float heightScale = (float)spriteRenderer.sprite.texture.height / texture.height;
            
            spriteRenderer.transform.localScale = new Vector3(widthScale*spriteRenderer.transform.localScale.x, heightScale *spriteRenderer.transform.localScale.y, 1);

            
            spriteRenderer.sprite = sprite;

            
        }

        yield return null;
        
    }


    

}
