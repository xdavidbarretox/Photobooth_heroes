using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour
{
    float imageSize;
    float imageMiddle;
    float offset = 30f;
    float Xpos;
    float Ypos;

	// Use this for initialization
	void Start ()
    {
        imageSize = (float)Screen.height / 3f;

        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(imageSize, imageSize);
        
        imageMiddle = imageSize / 2f;
        Xpos = (float)Screen.width - imageMiddle - offset;
        Ypos = imageMiddle + offset;
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(Screen.width + " width - " + Screen.height + " height");
        Debug.Log(transform.position.x + " x - " + transform.position.y + " y");
        Vector3 newPosition = new Vector3(Xpos, Ypos, 0f);
        transform.position = newPosition;
    }
}   