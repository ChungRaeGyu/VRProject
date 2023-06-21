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
    public Image[] HealthImage;
    Text attentionText;
    
    public bool attacked;
    //��׷� ��ġ ���� {
    public static float attention_level; //��׷� ��ġ
    public float test;
    public float num;
    //��׷ΰ� 0�� �ƴҶ� ��������ŭ ��ġ�� ����߸��� ���� ��
    float decreaseTimer;
    float decreaseAmount;
    //��׷� ��ġ�� 50�� �̻��϶� �ͽ��� ���� ������ �ϱ� ���� ���̴�.
    float timer;
    float settime;

    //}
    public static bool PlayerDie = false;
    public static bool RNear = false;
    public static bool LeftClick = false;
    public static bool RightClick = false;
    private int buttonInt = 0;
    private bool invenClick = false;

    //public GameObject UIImage;
    //public Text HealthItem;
    public bool dieTest=false;
    AudioSource heartbit;
    bool Audiobool = true;
    // Start is called before the first frame update
    void Start()
    {
        HealthImage = GameObject.Find("Canvas(hp)").GetComponentsInChildren<Image>();
        attentionText = GameObject.Find("attentionText").GetComponent<Text>();
        heartbit = GetComponent<AudioSource>();
        drawHealth();
        attacked=false;
        decreaseAmount=1;
        decreaseTimer=5;
        settime=5;
        timer=0;
        Debug.Log("시작 HP : " + PlayerHP);
    }

    // Update is called once per frame
    void Update()
    {
        
        attentionText.text="어그로 수치 : "+attention_level;
        if (PlayerHP >10&&PlayerDie==false)
        {
            Debug.Log("플레이어 HP" + PlayerHP);
            transform.position = new Vector3(0.535f,4.833f,-14.604f);
            transform.rotation = Quaternion.Euler(0f,180f,0f);
            GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().PlayerDIeAction(); //����ȿ���� ����
            PlayerDie = true;
            dieTest=false;
            return;
        }
        //��׷� ��ġ ����~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //��׷μ�ġ�� 100�̻� �Ǹ� ��������
        if (attention_level >= 100)
        {
            if(GhostScript_NoPoint.diestart == false){
                GameObject.Find("HorrorGirl").GetComponent<GhostScript_NoPoint>().setDirection(transform.position);
                if(Audiobool){
                    heartbit.Play();
                    Audiobool=false;
                }
                
            }
        }
        else if(attention_level >=50){
            if(timer>settime){
                Debug.Log("체크");
                timer=0;
                if(Random.Range(1,10)%3==0){
                    Debug.Log("실행");
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
                decreaseTimer=5f;
            }
        }
        //���⸦ ���Ϳ� �ε����� ���� �ٲ۴�.
        if(attacked==true){
            Debug.Log("맞음 : " + PlayerHP);
            PlayerHP--;
            drawHealth();
            attacked = false;
            Audiobool=true;
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
    public void drawHealth()
    {
        for (int i = 2; i >= 0; i--)
        {
            HealthImage[i].enabled = false;
        }
        for (int j = PlayerHP; j >= 0; j--)
        {
            HealthImage[j].enabled = true;
        }
    }
}
