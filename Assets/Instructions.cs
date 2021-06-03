using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    private KinectManager __kinectManager;
    public GameObject Instruction;

    void Start () {
        __kinectManager = KinectManager.Instance;
    }

    void Update()
    {
        if (__kinectManager)
        {
            int users = __kinectManager.GetUsersCount();
            Instruction.SetActive(users == 0);
        }
    }
}