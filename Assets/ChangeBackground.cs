using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeBackground : MonoBehaviour {

    private KinectManager kinectManager;
    private PhotoBoothController Manager;
    private GUITexture Texture;

    public GameObject Background;
    public Texture[] Bacgrounds;

    public GameObject VideoBackground;
    private VideoPlayer VideoBG;
    public VideoClip[] Videos;

    // Use this for initialization
    void Start() {

        kinectManager = KinectManager.Instance;
        Manager = kinectManager.GetComponent<PhotoBoothController>();
        Texture = Background.GetComponent<GUITexture>();

        VideoBG = VideoBackground.GetComponent<VideoPlayer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (kinectManager)
        {
            if (Manager.currentIndex == -1)
                Texture.texture = null;

            if (Manager.currentIndex == 0)
            {
                Texture.texture = Bacgrounds[0];
                //VideoBG.clip = Videos[0];
            }

            if (Manager.currentIndex == 1)
            {
                Texture.texture = Bacgrounds[1];
                //VideoBG.clip = Videos[1];
            }

            if (Manager.currentIndex == 2)
            {
                Texture.texture = Bacgrounds[2];
                //VideoBG.clip = Videos[2];
            }

            if (Manager.currentIndex == 3)
            {
                Texture.texture = Bacgrounds[3];
                //VideoBG.clip = Videos[3];
            }

            if (Manager.currentIndex == 4)
            {
                Texture.texture = Bacgrounds[4];
                //VideoBG.clip = Videos[4];
            }
        }
    }
}
