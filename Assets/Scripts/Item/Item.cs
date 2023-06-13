using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject obj;
    IEnumerator UseHealth()
    {
        PlayerScript.PlayerHP += 1;
        Debug.Log(PlayerScript.PlayerHP);
        yield return null;
    }

    private void OnTriggerStay(Collider coll)
    {
        if(coll.tag == "Item")
        {
            obj = coll.gameObject;
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                
                StartCoroutine(UseHealth());
            }
        }
        else if(coll == null)
        {
            return;
        }
    }
}
