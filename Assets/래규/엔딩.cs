using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class 엔딩 : MonoBehaviour
{
    float timer =5;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time>timer)
        SceneManager.LoadScene("Scenes/Clear");
    }
}
