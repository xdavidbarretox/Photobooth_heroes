using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeldisplay : MonoBehaviour
{
    PhotoBoothController Manager;
    //Component[] Managers;
    //List<PhotoBoothController> Managers = new List<PhotoBoothController>();
    PhotoBoothController[] Managers;

    public GameObject[] Masks;
    public GameObject[] Props;
    public int userIndex;
    SkeletonOverlayer SK;
    private KinectManager kinectManager;
    private ModelHatController Controler;
    long userId;
    public long userTarget;

    //Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("KinectController").GetComponent<PhotoBoothController>();
        SK = GameObject.Find("KinectController").GetComponent<SkeletonOverlayer>();
        kinectManager = KinectManager.Instance;

        Managers = GameObject.Find("KinectController").GetComponents<PhotoBoothController>();
        foreach (PhotoBoothController FBM in Managers)
        {
            Debug.Log("PhotoBoothController " + FBM.playerIndex);
        }
        Debug.Log("PhotoBoothController 0 ++++" + Managers[2].playerIndex);
    }

    void ShowCostume()
    {
        
        for (int i = 0; i < Masks.Length; i++)
        {
            if (Masks[i] != null)
                Masks[i].SetActive(Manager.currentIndex == i);
        }
        for (int i = 0; i < Props.Length; i++)
        {
            if (Props[i] != null)
                Props[i].SetActive(Manager.currentIndex == i);
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (kinectManager)
        {
            userId = kinectManager.GetUserIdByIndex(userIndex);
            if ((Manager.currentIndex >= 0) && (Manager.currentIndex < Masks.Length))
            {
                if (Masks[Manager.currentIndex] != null)
                {
                    if ((userId != 0) && (Manager.currentIndex <= (Masks.Length - 1)))
                    {
                        ShowCostume();

                        if (Manager.currentIndex == 4)
                        {
                            Props[4].transform.Rotate(new Vector3(0, SK.joints[1].transform.rotation.y, 0));
                        }
                    }
                }
            }
        }
	}
}