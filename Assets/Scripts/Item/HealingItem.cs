using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class HealingItem : MonoBehaviour
{
    public static bool canEat = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && PlayerScript.RNear == true)
        {
            //Inventory.HelingItemNum += 1;
        }
    }
}
