using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 플레이어 죽음, 컨트롤러 클릭 관련 cs 파일
    /// </summary>
    public static int PlayerHP = 3;
    //어그로 수치 관련 {
    public static float attention_level; //어그로 수치
    public float test;
    public float num;
    //어그로가 0이 아닐때 일정량만큼 수치를 떨어뜨리기 위한 값
    float decreaseTimer=5f;
    float decreaseAmount = 1;
    //어그로 수치가 50퍼 이상일때 귀신이 가끔 나오게 하기 위한 것이다.
    float timer;
    float settime = 5f;

    //}
    public static bool PlayerDie = false;
    public static bool RNear = false;
    public static bool LeftClick = false;
    public static bool RightClick = false;
    private int buttonInt = 0;
    private bool invenClick = false;

    //public GameObject UIImage;
    //public GameObject HealthImage;
    //public Text HealthItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        attention_level =num;
        if (PlayerHP <= 0&&PlayerDie==false)
        {
            //시점 전환 
            GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
            GameObject.Find("CatchCamera").GetComponent<Camera>().enabled = true;
            GameObject.Find("CatchCamera").GetComponent<AudioListener>().enabled = true;
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().PlayerDIeAction(); //점멸효과의 시작
            PlayerDie = true;
            return;
        }
        //어그로 수치 관련~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //어그로수치가 100이상 되면 추적시작
        if (attention_level >= 100)
        {   

            //추적시작
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().setDirection(transform.position);
        }
        else if(attention_level>=50){
            if(timer>settime){
                timer=0;
                if(Random.Range(1,10)%5==0){
                    GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().over50per();
                }
            }
            timer+=Time.deltaTime;
        }
        //어그로 수치를 5초당 1씩 줄어들게 한다.
        if(attention_level!=0&&attention_level<100){
            decreaseTimer -= Time.deltaTime;
            if(decreaseTimer<=0)
            {
                attention_level-=decreaseAmount;
                Debug.Log("감소");
                decreaseTimer=5f;
            }
        }

        //int HealthItemNum = Inventory.HelingItemNum;

        //HealthItem.text = "치료제: " + HealthItemNum.ToString();



        /*
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            LeftClick = true;
        }
        else if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            LeftClick = false;
            return;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && RNear == true)
        {
            RightClick = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && RNear == true)
        {
            RightClick = false;
            return;
        }
        */

        //if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        //{
        //    if (invenClick == false)
        //    {
        //        Debug.Log("왼쪽 UI 나와라");
        //        UIImage.GetComponent<Image>().enabled = true;
        //        HealthImage.GetComponent<Image>().enabled = true;
        //        HealthItem.GetComponent<Text>().enabled = true;
        //        invenClick = true;
        //    }
        //    else if (invenClick == true)
        //    {
        //        Debug.Log("왼쪽 UI 사라져");
        //        UIImage.GetComponent<Image>().enabled = false;
        //        HealthImage.GetComponent<Image>().enabled = false;
        //        HealthItem.GetComponent<Text>().enabled = false;
        //        invenClick = false;
        //    }
        //}

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            //Debug.Log(Inventory.HelingItemNum);
        }

    }
}
