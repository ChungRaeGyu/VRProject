using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    /*
    Ligth 오브젝트에 넣는다.
    */
    int num;
    bool uvLight = false;
    public bool LightON=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { //VR컨트롤러 키를 넣으면 된다.
    
        //if(OVRInput.GetDown(OVRInput.Button.One) && uvLight == false)
        //{
        //    if (num != 1){
        //        transform.GetChild(0).GetComponent<Light>().enabled=true;
        //        transform.GetChild(1).GetComponent<Light>().enabled = false;
        //        num=1;
        //        LightON=true;
        //    }else if (num == 1){
        //        transform.GetChild(0).GetComponent<Light>().enabled = false;
        //        num=0;
        //        LightON=false;
        //    }
        //    uvLight = true;
        //    if (!LightON)
        //        GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().FoundTarget(LightON);
        //}
        //if(OVRInput.GetDown(OVRInput.Button.One) && uvLight == true)
        //{ //UVLIght 작동 
        //    if (num != 2){
        //        transform.GetChild(0).GetComponent<Light>().enabled = false;
        //        transform.GetChild(1).GetComponent<Light>().enabled = true;
        //        num=2;
        //        LightON=true;
        //    }else if(num==2){
        //        transform.GetChild(1).GetComponent<Light>().enabled = false;
        //        num=0;
        //        LightON=false;
        //    }
        //    uvLight = false;
        //    if (!LightON)
        //    GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().FoundTarget(LightON);
        //}

        if (OVRInput.GetDown(OVRInput.Button.One))
        { //UVLIght 작동
            if (num == 0)
            {
                transform.GetChild(0).GetComponent<Light>().enabled = true;
                transform.GetChild(1).GetComponent<Light>().enabled = false;
                LightON = true;
                num = 1;
                Debug.Log("0");
                return;
            }
            else if (num == 1)
            {
                transform.GetChild(0).GetComponent<Light>().enabled = false;
                transform.GetChild(1).GetComponent<Light>().enabled = true;
                LightON = true;
                num = 2;
                Debug.Log("1");
                return;
            }
            else if (num == 2)
            {
                transform.GetChild(1).GetComponent<Light>().enabled = false;
                LightON = false;
                num = 0;
                Debug.Log("2");
                return;
            }
            if (!LightON)
                GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().FoundTarget(LightON);
        }
    }   
}
