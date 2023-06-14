using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    IEnumerator UseHealth()
    {
        PlayerScript.PlayerHP += 1;
        Debug.Log(PlayerScript.PlayerHP);
        yield return null;
    }

    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Item")
        {
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                StartCoroutine(UseHealth());
                Destroy(coll.gameObject);
            }
        }
        
    }
}
