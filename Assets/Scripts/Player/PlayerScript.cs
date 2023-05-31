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
    public static int attention_level; //어그로 수치
    public int test;
    public static bool PlayerDie = false;
    public static bool RNear = false;
    public static bool LeftClick = false;
    public static bool RightClick = false;
    private int buttonInt = 0;
    private bool invenClick = false;

    public GameObject UIImage;
    public GameObject HealthImage;
    public Text HealthItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attention_level=test;
        if (PlayerHP <= 0)
        {
            PlayerDie = true;
            return;
        }

        //어그로수치가 100이상 되면 추적시작
        if (attention_level >= 100)
        {
            //추적시작
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().setDirection(transform.position);
        }

        int HealthItemNum = Inventory.HelingItemNum;

        HealthItem.text = "치료제: " + HealthItemNum.ToString();



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

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            if (invenClick == false)
            {
                Debug.Log("왼쪽 UI 나와라");
                UIImage.GetComponent<Image>().enabled = true;
                HealthImage.GetComponent<Image>().enabled = true;
                HealthItem.GetComponent<Text>().enabled = true;
                invenClick = true;
            }
            else if (invenClick == true)
            {
                Debug.Log("왼쪽 UI 사라져");
                UIImage.GetComponent<Image>().enabled = false;
                HealthImage.GetComponent<Image>().enabled = false;
                HealthItem.GetComponent<Text>().enabled = false;
                invenClick = false;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            //Debug.Log(Inventory.HelingItemNum);
        }

    }

}
