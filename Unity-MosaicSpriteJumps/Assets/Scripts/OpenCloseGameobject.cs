using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGameobject : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectt;

    public void OpenCloseThisObject()
    {
        gameObjectt.SetActive(!gameObjectt.activeSelf);
    }

}
