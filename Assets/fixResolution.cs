using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixResolution : MonoBehaviour {

    int __width = 1080;
    int __height = 720;

    int currentWidth;
    int currentHeight;

    public float scaleX;
    public float scaleY;

    public bool ChangePosition;

	// Use this for initialization
	void Start ()
    {
        currentWidth = Screen.currentResolution.width;
        currentHeight = Screen.currentResolution.height;

        scaleX = ((float)currentWidth / (float)__width);
        scaleY = ((float)currentHeight / (float)__height);

        changeResolution();
    }

    public void changeResolution()
    {
       // #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        transform.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1);
        if (ChangePosition)
        {
            float currentPositionX = transform.GetComponent<RectTransform>().localPosition.x;
            float currentPositionY = transform.GetComponent<RectTransform>().localPosition.y;

            transform.GetComponent<RectTransform>().localPosition = new Vector3(currentPositionX * scaleX, currentPositionY * scaleY, 0);
        }
       // #endif
    }
}
