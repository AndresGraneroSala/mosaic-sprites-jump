using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCredit : MonoBehaviour
{
    void Start()
    {

        Text text = GetComponentInChildren<Text>();
        text.text += ConfigurationSprites.SharedInstance.ConfigurationSpritesData.spritesBy;

        OpenUrl openUrl = GetComponent<OpenUrl>();
        openUrl.url=ConfigurationSprites.SharedInstance.ConfigurationSpritesData.spritesByUrl;

    }

}
