using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterOpen : MonoBehaviour
{
    GameObject chainObject;
    GameObject StealDoor;

    // Start is called before the first frame update
    void Start()
    {
        chainObject = transform.Find("Chain").gameObject;
        StealDoor = transform.Find("MeshFence").gameObject;
    }


    private void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Cutter"))
        {
            Destroy(coll.gameObject);
            chainObject.SetActive(false);
            StealDoor.SetActive(false);
        }
    }
}
