using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    GameObject Player;
    Text FullTxt;
    private bool Full;

    void Start()
    {
        Full = true;
        Player = GameObject.Find("OVRPlayerController");
        FullTxt = GameObject.Find("FullHealth").GetComponent<Text>();
    }
    IEnumerator UseHealth()
    {
        PlayerScript.PlayerHP += 1;
        Player.GetComponent<PlayerScript>().drawHealth();
        yield return null;
    }
    IEnumerator FalseText()
    {
        FullTxt.GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(2.0f);
    }


    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Item")
        {
            if (OVRInput.GetDown(OVRInput.Button.Two)&&PlayerScript.PlayerHP<2)
            {
                if(PlayerScript.PlayerHP<2)
                {
                    StartCoroutine(UseHealth());
                    Destroy(coll.gameObject);
                }
                else if(PlayerScript.PlayerHP==2)
                {
                    FullTxt.GetComponent<Text>().enabled = true;
                    StartCoroutine(FalseText());
                }
            }
        }
        
    }
}
