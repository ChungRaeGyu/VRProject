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

        if (PlayerHP <= 0)
        {
            PlayerDie = true;
            return;
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
