using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class KeyListeners : MonoBehaviour {

    PhotoShooter PBC;
    public string Unlock;

	void Start ()
    {
        PBC = GameObject.Find("KinectController").GetComponent<PhotoShooter>();

        string macAddr = ( from nic in NetworkInterface.GetAllNetworkInterfaces()
                           where nic.OperationalStatus == OperationalStatus.Up
                           select nic.GetPhysicalAddress().ToString() ).FirstOrDefault();

        if (macAddr != Unlock)
            Application.Quit();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.P))
            PBC.CountdownAndMakePhoto();

        if (Input.GetKeyDown(KeyCode.F))
           PBC.StartCoroutine(PBC.showFlash());

        if (Input.GetKeyDown(KeyCode.D))
            Debug.Log(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
    }
}
