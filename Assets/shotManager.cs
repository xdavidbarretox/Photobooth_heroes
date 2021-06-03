using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotManager : MonoBehaviour {

    public GameObject Smoke;
    public GameObject bullet;
    public GameObject Effect;
    public GameObject Reference;
    private GameObject Shield;
    
	// Use this for initialization
	void Start () {

        Shield = GameObject.Find("Shield");
    }

    private IEnumerator SureShot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("--------------------------Sure shot");
        ShowElements(false);
        ShowShot(true);
    }

    private int CheckPosition()
    {
        int pos = -1;
        int posx = 0;
        int posy = 0;

        float __Xbullet = transform.position.x;
        float __Ybullet = transform.position.y;
        float __Xreference = Reference.transform.position.x;
        float __Yreference = Reference.transform.position.y;

        if (__Xbullet > __Xreference)
        {
            posx = 1;
        }
        else
        {
            posx = -1;
        }
        if (__Ybullet > __Yreference)
        {
            posy = 1;
        }
        else
        {
            posy = -1;
        }
        if((posx == 1) && (posy == 1))
        {
            pos = 0;
        }
        if ((posx == 1) && (posy == -1))
        {
            pos = 1;
        }
        if ((posx == -1) && (posy == -1))
        {
            pos = 2;
        }
        if ((posx == -1) && (posy == 1))
        {
            pos = 3;
        }

        return pos;
    }

    private void ShowShot(bool isShown)
    {
        float __X = Random.Range(-0.25f, 0.25f);
        float __Y = Random.Range(-0.25f, 0.25f);
        float __Xbullet = transform.position.x;
        float __Ybullet = transform.position.y;
        float __Xreference = Reference.transform.position.x;
        float __Yreference = Reference.transform.position.y;

        int __case = CheckPosition();

        float TranslateX = 0f;
        float TranslateY = 0f;

        if (__case == 0)
        {
            TranslateX = (__Xreference - __Xbullet) * -1;
            TranslateY = (__Ybullet - __Yreference) * -1;
        }
        else if (__case == 1)
        {
            TranslateX = (__Xreference - __Xbullet) * -1;
            TranslateY = (__Yreference - __Ybullet);
        }
        else if (__case == 2)
        {
            TranslateX = (__Xbullet -__Xreference);
            TranslateY = (__Yreference - __Ybullet);
        }
        else
        {
            TranslateX = (__Xbullet - __Xreference);
            TranslateY = (__Ybullet - __Yreference) * -1;
        }
        transform.Translate(new Vector3(TranslateX, TranslateY, 0f), Space.Self);

        transform.Translate(new Vector3 (__X, __Y, 0f), Space.Self);
        ShowElements(isShown);
        HitSound(isShown);
    }

    void ShowElements(bool isShown)
    {
        Smoke.SetActive(isShown);
        bullet.SetActive(isShown);
        Effect.SetActive(isShown);
    }

    void HitSound(bool isSound)
    {
        if(isSound)
            gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Position = " + CheckPosition());
            ShowShot(true);
        }

        if(Shield.activeSelf)
        {
            Debug.Log("turn on------------------");
            float __wait = Random.Range(1.0f, 3.0f);
             StartCoroutine("SureShot", __wait);
        }
    }
}
