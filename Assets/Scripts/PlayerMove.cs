using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //어그로 수치
    public static int attention_level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //어그로수치가 100이상 되면 추적시작
        if (attention_level >= 100)
        {
            //추적시작
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().setDirection(transform.position);
            attention_level =0;
        }
    }
    //문이나 아이템 먹을때 어그로 수치 상승 코드를 넣어주세요
}
