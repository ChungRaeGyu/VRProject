using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 엔딩크래딧 : MonoBehaviour
{
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = transform.position;
        newPosition.y--;
        newPosition.z--;
        transform.position = newPosition;
    }
}
