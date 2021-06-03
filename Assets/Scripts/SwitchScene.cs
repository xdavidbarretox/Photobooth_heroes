using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene(1);
    }
}
