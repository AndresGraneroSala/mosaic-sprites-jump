using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject bg;

    // Update is called once per frame
    void Update()
    {
        
        bg.SetActive(!Application.isFocused);
        Time.timeScale = !Application.isFocused ? 0 : 1;

    }
}
