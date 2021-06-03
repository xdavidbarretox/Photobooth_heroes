using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour {

    int __width = 1080;
    int __height = 720;

    int currentWidth;
    int currentHeight;

    public float scaleX;
    public float scaleY;

    // Use this for initialization
    void Start()
    {
        currentWidth = Screen.currentResolution.width;
        currentHeight = Screen.currentResolution.height;

        scaleX = ((float)currentWidth / (float)__width);
        scaleY = ((float)currentHeight / (float)__height);

        Cursor.visible = false;
    }
}
