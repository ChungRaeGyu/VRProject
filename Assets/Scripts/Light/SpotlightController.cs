using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    Light[] Spotlight;
    int i=0;
    AudioSource[] lightsound;
    AudioSource laughsound;
    Vector3[] position=new Vector3[]{
        new Vector3(0.627f, 3.510628f, -40.83f),
        new Vector3(0.627f, 3.510628f, -31.033f),
        new Vector3(0.627f, 3.510628f, -20.819f)
    };
    // Start is called before the first frame update
    void Start()
    {
        Spotlight=GetComponentsInChildren<Light>();
        lightsound = new AudioSource[Spotlight.Length];
        for(int j=0; j<Spotlight.Length;j++){
            lightsound[j]=Spotlight[j].GetComponent<AudioSource>();
        }
        laughsound = transform.GetChild(3).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //시간이 남으면 스파게티를 좀 풀어보자
    public IEnumerator lightout(){
        laughsound.Play();
        yield return new WaitForSecondsRealtime(7f);
        for(i =0;i<Spotlight.Length;i++){
            yield return new WaitForSecondsRealtime(1f);
            lightsound[i].Play();
            Spotlight[i].enabled = true;
            GameObject.Find("HorrorGirl").transform.position=position[i];
            yield return new WaitForSecondsRealtime(1f);
            Spotlight[i].enabled = false;
            GameObject.Find("HorrorGirl").transform.position = new Vector3(0,0,0);
        }
        i=0;
    }
}
