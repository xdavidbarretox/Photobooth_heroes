using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePoint : MonoBehaviour {

    public GameObject point1;
    public GameObject point2;
    public GameObject Middle_Point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float middleX =  (point1.transform.position.x + point2.transform.position.x) / 2;
        float middleY;
        if (point1.transform.position.y > point2.transform.position.y)
        {
            middleY = ((point1.transform.position.y - point2.transform.position.y) / 2) + point2.transform.position.y;
        }
        else
        {
            middleY = ((point2.transform.position.y - point1.transform.position.y) / 2) + point1.transform.position.y;
        }

        float middleZ;
        if (point1.transform.position.z > point2.transform.position.z)
        {
            middleZ = ((point1.transform.position.z - point2.transform.position.z) / 2) + point2.transform.position.z;
        }
        else
        {
            middleZ = ((point2.transform.position.z - point1.transform.position.z) / 2) + point1.transform.position.z;
        }
        Vector3 Middle = new Vector3(middleX, middleY, middleZ);
        //Debug.Log("Middle " + middleX + " - " + middleY + " " + middleZ);
        Middle_Point.transform.position = Middle;
    }
}
