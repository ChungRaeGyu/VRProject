using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutterOpen : MonoBehaviour
{
    GameObject StealDoor;
    Text StealTxt;

    private bool enter;
    private bool Lock;

    // Start is called before the first frame update
    void Start()
    {
        enter = false;
        Lock = true;
        StealDoor = this.gameObject;
        StealTxt = GameObject.Find("StealLock").GetComponent<Text>();
    }

    void Update()
    {
        if(enter == true && Lock == true)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                StealTxt.GetComponent<Text>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Cutter"))
        {
            Destroy(coll.gameObject);
            StealDoor.SetActive(false);
            Lock = false;
        }
        if(coll.CompareTag("Player"))
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            enter = false;
            if(StealTxt.GetComponent<Text>().enabled == true)
            {
                StealTxt.GetComponent<Text>().enabled = false;
            }
        }
    }
}
