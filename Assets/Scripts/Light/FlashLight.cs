using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    /*
    Ligth 오브젝트에 넣는다.
    */
    int num;
    public bool LightON=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { //VR컨트롤러 키를 넣으면 된다.
    
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (num != 1){
                transform.GetChild(0).GetComponent<Light>().enabled=true;
                transform.GetChild(1).GetComponent<Light>().enabled = false;
                num=1;
                LightON=true;
            }else if (num == 1){
                transform.GetChild(0).GetComponent<Light>().enabled = false;
                num=0;
                LightON=false;
            }
            if(!LightON)
                GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().FoundTarget(LightON);
        }
        if(Input.GetKeyDown("2")){ //UVLIght 작동 
            if (num != 2){
                transform.GetChild(0).GetComponent<Light>().enabled = false;
                transform.GetChild(1).GetComponent<Light>().enabled = true;
                num=2;
                LightON=true;
            }else if(num==2){
                transform.GetChild(1).GetComponent<Light>().enabled = false;
                num=0;
                LightON=false;
            }
            if (!LightON)
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().FoundTarget(LightON);
        }
    }   
}
