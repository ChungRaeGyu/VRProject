using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject Player;
    void Start() {
        Player = GameObject.Find("OVRPlayerController");
    }
    IEnumerator UseHealth()
    {
        PlayerScript.PlayerHP += 1;
        Player.GetComponent<PlayerScript>().drawHealth();
        yield return null;
    }

    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Item")
        {
            if (OVRInput.GetDown(OVRInput.Button.Two)&&PlayerScript.PlayerHP<2)
            {
                StartCoroutine(UseHealth());
                Destroy(coll.gameObject);
            }
        }
        
    }
}
