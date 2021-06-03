using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;


public class PhotoShooter : MonoBehaviour
{
    [Tooltip("Camera used to render the background.")]
    public Camera backroundCamera;

    [Tooltip("Camera used to render the background layer-2.")]
    public Camera backroundCamera2;

    [Tooltip("Camera used to overlay the 3D-objects over the background.")]
    public Camera foreroundCamera;

    [Tooltip("Array of sprite transforms that will be used for displaying the countdown until image shot.")]
    public Transform[] countdown;

    [Tooltip("UI-Text used to display information messages.")]
    public UnityEngine.UI.Text infoText;

    //Added 2017 shown a line in the middle of screen
    public GameObject MiddleLine;
    public Texture2D screenShot = null;
    public GameObject PhotoTexture;
    public GameObject FlashTexture;
    public GameObject SoundContainer;

    private bool isTaking = false;


    /// <summary>
    /// Counts down (from 3 for instance), then takes a picture and opens it
    /// </summary>
    public void CountdownAndMakePhoto()
    {
        StartCoroutine(CoCountdownAndMakePhoto());
    }


    // counts down (from 3 for instance), then takes a picture and opens it
    private IEnumerator CoCountdownAndMakePhoto()
    {
        if (!isTaking)
        {
            isTaking = true;
            MiddleLine.SetActive(true);

            if (countdown != null && countdown.Length > 0)
            {
                for (int i = 0; i < countdown.Length; i++)
                {
                    if (countdown[i])
                        countdown[i].gameObject.SetActive(true);

                    yield return new WaitForSeconds(1.0f);

                    if (countdown[i])
                        countdown[i].gameObject.SetActive(false);
                }
            }

            MiddleLine.SetActive(false);
            isTaking = false;
            MakePhoto();
            yield return null;
        }
    }


    /// <summary>
    /// Saves the screen image as png picture, and then opens the saved file.
    /// </summary>
    public void MakePhoto()
    {
        MakePhoto(false);
        //David Barreto modify
    }

    /// <summary>
    /// Saves the screen image as png picture, and optionally opens the saved file.
    /// </summary>
    /// <returns>The file name.</returns>
    /// <param name="openIt">If set to <c>true</c> opens the saved file.</param>
    public string MakePhoto(bool openIt)
    {
        int resWidth = Screen.width;
        int resHeight = Screen.height;

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false); //Create new texture
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);

        // hide the info-text, if any
        if (infoText)
        {
            infoText.text = string.Empty;
        }

        // render background and foreground cameras
        if (backroundCamera && backroundCamera.enabled)
        {
            backroundCamera.targetTexture = rt;
            backroundCamera.Render();
            backroundCamera.targetTexture = null;
        }

        if (backroundCamera2 && backroundCamera2.enabled)
        {
            backroundCamera2.targetTexture = rt;
            backroundCamera2.Render();
            backroundCamera2.targetTexture = null;
        }

        if (foreroundCamera && foreroundCamera.enabled)
        {
            foreroundCamera.targetTexture = rt;
            foreroundCamera.Render();
            foreroundCamera.targetTexture = null;
        }

        // get the screenshot
        RenderTexture prevActiveTex = RenderTexture.active;
        RenderTexture.active = rt;

        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        screenShot.Apply();//Added

        // clean-up
        RenderTexture.active = prevActiveTex;
        Destroy(rt);
        byte[] btScreenShot = screenShot.EncodeToJPG();
        //Destroy(screenShot);

        StartCoroutine(showFlash());
        PhotoTexture.GetComponent<RawImage>().texture = screenShot;
        PhotoTexture.SetActive(true);
        StartCoroutine(movePhoto());

        float scaleX = GameObject.Find("Addons").GetComponent<ScreenResolution>().scaleX;
        float scaleY = GameObject.Find("Addons").GetComponent<ScreenResolution>().scaleY;
        PhotoTexture.transform.localScale = new Vector3(scaleX, scaleY, 1f);


        // save the screenshot as jpeg file
        // string sDirName = Application.persistentDataPath + "/Screenshots";
        string sDirName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Screenshots";
        string sAndroidDir = "Y:/DCIM/Camera";

        Debug.Log("--------------------------" + sDirName);

        if (!Directory.Exists(sDirName))
            Directory.CreateDirectory(sDirName);

        //string date = System.DateTime.Now.ToString();
        string sFileName = "photobooth_" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".jpg";
        //string sFileName = sDirName + "/" + string.Format("{0:F0}", Time.realtimeSinceStartup * 10f) + ".jpg";
        File.WriteAllBytes(sDirName + "/" + sFileName, btScreenShot);
        File.WriteAllBytes(sAndroidDir + "/" + sFileName, btScreenShot);

        //Uploader upload = GameObject.Find("Addons").GetComponent<Uploader>();
        //StartCoroutine(upload.UploadImage(sFileName));//it was comment
        //upload.UploadImage2(sFileName);

        Debug.Log("Photo saved to: " + sFileName);
        if (infoText)
        {
            infoText.text = "Saved to: " + sFileName;
        }

        // open file
        if (openIt)
        {
            System.Diagnostics.Process.Start(sFileName);
        }

        return sFileName;
}

private IEnumerator hidephoto()
{
    yield return new WaitForSeconds(1.0f);
    PhotoTexture.SetActive(false);
}

private IEnumerator movePhoto()
{
    float scaleX = GameObject.Find("Addons").GetComponent<ScreenResolution>().scaleX * 0.3f;
    float scaleY = GameObject.Find("Addons").GetComponent<ScreenResolution>().scaleY * 0.3f;

    Vector3 _endPosition = new Vector3(100f, 30f, 0f);
    Vector3 _endScale = new Vector3(scaleX, scaleY, 1f);

    float _advance = 0f;
    float framePerSecond = 30f;
    float timing = 1.0f / framePerSecond;
    float frame = 0f;

    while (_advance <= 1.0f)
    {
        PhotoTexture.transform.position = Vector3.Lerp(PhotoTexture.transform.position, _endPosition, _advance);
        PhotoTexture.transform.rotation = Quaternion.Lerp(PhotoTexture.transform.rotation, Quaternion.Euler(0, 0, 15), _advance);
        PhotoTexture.transform.localScale = Vector3.Lerp(PhotoTexture.transform.localScale, _endScale, _advance);

        yield return new WaitForSeconds(timing);
        _advance += timing;
        frame++;
        //Debug.Log("_advance " + _advance + " " + frame);
    }

    PhotoTexture.transform.position = Vector3.Lerp(PhotoTexture.transform.position, _endPosition, 1f);
    PhotoTexture.transform.rotation = Quaternion.Lerp(PhotoTexture.transform.rotation, Quaternion.Euler(0, 0, 15), 1f);
    PhotoTexture.transform.localScale = Vector3.Lerp(PhotoTexture.transform.localScale, _endScale, 1f);

    yield return new WaitForSeconds(1.0f);

    PhotoTexture.SetActive(false);

    Debug.Log("ending------- ");

}

public IEnumerator showFlash()
{
    SoundContainer.GetComponent<AudioSource>().Play();
    FlashTexture.SetActive(true);
    RawImage rend = FlashTexture.GetComponent<RawImage>();
    float Alpha = 1.0f;
    Color changeAlpha;
    float framePerSecond = 30f;
    float timing = 1.0f / framePerSecond;
    float steps = 6f;
    float decraeseValue = 1.0f / steps;

    while (Alpha >= 0.0f)
    {
        changeAlpha = new Color(1f, 1f, 1f, Alpha);
        rend.color = changeAlpha;
        Alpha -= decraeseValue;
        yield return new WaitForSeconds(timing);
    }

    Alpha = 0.0f;
    changeAlpha = new Color(1f, 1f, 1f, Alpha);
    rend.color = changeAlpha;
    FlashTexture.SetActive(false);
    //Debug.Log(rend.color.ToString());

}


}