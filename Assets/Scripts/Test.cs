using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int i = 1;
    int j = 1;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        while(i<10){
            
        i++;
        Debug.Log(i);
            StartCoroutine(test());
     }
     Debug.Log("메렁");
     
    }
    IEnumerator test(){
        while(j<10){
            Debug.Log("J"+j);
            j++;
        }
        yield return 0;
    }

}
