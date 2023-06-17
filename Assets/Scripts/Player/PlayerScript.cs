using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� ����, ��Ʈ�ѷ� Ŭ�� ���� cs ����
    /// </summary>
    public static int PlayerHP = 2;
    Image[] HealthImage;
    public bool attacked=false;
    //��׷� ��ġ ���� {
    public static float attention_level; //어그로 수치
    public float test;
    public float num;
    //어그로 수치 감소
    float decreaseTimer=5f;
    float decreaseAmount = 1;
    //가끔 나타나게 하기 위한 시간
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
    //public Text HealthItem;

    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        HealthImage = GameObject.Find("Canvas(hp)").GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        attention_level = test;
        num=attention_level;
        if (PlayerHP < 0&&PlayerDie==false)
        {
            //���� ��ȯ 
            GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
            GameObject.Find("CatchCamera").GetComponent<Camera>().enabled = true;
            GameObject.Find("CatchCamera").GetComponent<AudioListener>().enabled = true;
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().PlayerDIeAction(); //����ȿ���� ����
            PlayerDie = true;
            return;
        }
        //��׷� ��ġ ����~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //��׷μ�ġ�� 100�̻� �Ǹ� ��������
        if (attention_level >= 100)
        {
            //��������
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
        //��׷� ��ġ�� 5�ʴ� 1�� �پ��� �Ѵ�.
        if(attention_level>0&&attention_level<100){
            decreaseTimer -= Time.deltaTime;
            if(decreaseTimer<=0)
            {
                attention_level-=decreaseAmount;
                Debug.Log("����");
                decreaseTimer=5f;
            }
        }
        //���⸦ ���Ϳ� �ε����� ���� �ٲ۴�.
        if(attacked==true){
            HealthImage[PlayerHP].enabled=false;
            attacked=false;
            PlayerHP--;
        }
        //int HealthItemNum = Inventory.HelingItemNum;

        //HealthItem.text = "ġ����: " + HealthItemNum.ToString();



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
        //        Debug.Log("���� UI ���Ͷ�");
        //        UIImage.GetComponent<Image>().enabled = true;
        //        HealthImage.GetComponent<Image>().enabled = true;
        //        HealthItem.GetComponent<Text>().enabled = true;
        //        invenClick = true;
        //    }
        //    else if (invenClick == true)
        //    {
        //        Debug.Log("���� UI �����");
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
