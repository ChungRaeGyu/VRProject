using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_start : MonoBehaviour
{
    public GameObject AllLight;
    public AudioSource bgmAudio; //플레이어 한테 붙여놓을 것이다.
    public AudioClip ghostAttack;

    // Start is called before the first frame update
    void Start()
    {
        AllLight = GameObject.Find("Light");
    }

    // Update is called once per frame
    void Update()
    {
        //원장실의 문을 열었을 때 조건을 준다.  
        if(Input.GetButtonDown("Fire1"))
        AllLight.SetActive(false);
    }
}
