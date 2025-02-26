using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Navigation을 사용해서 적에게 달려 간다.
//적에게 가다가 숨으면 잠시 멈췄다가 움직인다.
//적 식별관련 코드는 ghostRange에 있다.
public class GhostScript_NoPoint : MonoBehaviour
{
    public bool targetCheck = false;
    private NavMeshAgent nav;
    //-------------------------------------------------------추적
    float timer = 20.0f;
    float time;
    public Transform Player;
    float playerDistance = 1.5f;
    
    public float speed = 5.0f;  //이동 속도
    public float damping = 3.0f; //회전 시 회전 속도를 조절하는 계수
    Vector3 spawn= new Vector3(-11f,3.510628f,-3);
    //사용 할 곳 (50per, catch)
    private Transform tr;    //캐릭터의 Transform값을 담을 함수
    private Material girl;
    private Material cloth;
    private Material hairs;
    public float fadeDuration = 3f; // 서서히 투명해지는 시간
    private float currentAlpha; // 현재 Alpha 값
    //////////////////////////////////////////_--------------------------------------------
    bool first = true; //한번만 실행하기 위한
    //--------------------------------------------------------die
    public static bool diestart=false; //정신력이 0이 되었을 때 //그냥 static으로 써버리고 해도 될듯?

    //--------------------------------------------Anim
    Animator anim;
    //-------------------------------------------------ghost audio
    AudioSource over50perSound;
    //얼굴 
    public GameObject ghostImage; 
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        over50perSound = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        //사라지는 효과를 위한 Renderer
        cloth = transform.GetChild(0).GetComponent<Renderer>().material;
        girl = transform.GetChild(1).GetComponent<Renderer>().material;
        hairs = transform.GetChild(2).GetComponent<Renderer>().material;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        
        //초기값 저장
    }
    // Update is called once per frame
    void Update()
    {
        //플레이어를 식별 했을 때 와 안했을 때를 구분
        //targetCheck는 setDirection함수에 있다.
        if(!diestart){
            if (targetCheck)
            {
                if (first)
                {
                    first = false;
                    StartCoroutine(Fadein(fadeDuration));
                }

                //--------------------------------------------targetCheck가 true일때 시간 제한 //적을 쫓아 갈 때
                time += Time.deltaTime;
                if (time > timer)
                {
                    StartCoroutine(lost(false)); //플레이어를 놓치면 잠깐 멈춤 빼버릴까
                    time = 0;
                }

                //플레이어와 귀신의 거리 계산
                float distance = Vector3.Distance(transform.position, Player.position);
                //잡았다.                  
                if (distance < playerDistance)
                {
                    Debug.Log(" 거리 : " + distance);
                    ghostImage.SetActive(true);
                    ghostImage.GetComponent<Animator>().SetTrigger("Damaged");
                    StartCoroutine(catchPlayer(false));
                }
            }
            else
            { 
                //플레이어를 놓치면 사라진다.
                //first는 한번만 실행하기 위한 것이다.
                if (first)
                {
                    first = false;
                    StartCoroutine(Fadeout());
                }
            }
        }else{
            return;
        }
    }
    IEnumerator catchPlayer(bool check){
        PlayerScript.attention_level = 0; //어그로 수치를 0으로 만든다.
        nav.ResetPath(); //navigation을 초기화 한다.
        anim.SetBool("walk", false);
        Debug.Log("몇번 실행 되니?");
        targetCheck = check;  //기본 움직임을 실행시킨다.
        first = true;
        time = 0;
        Player.GetComponent<PlayerScript>().attacked = true;
        yield return new WaitForSecondsRealtime(1.5f);
        ghostImage.SetActive(false);
        
    }
    IEnumerator velocity0(){
        yield return new WaitForSecondsRealtime(2.5f);
        rigid.velocity=transform.forward*0f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // 경과 시간에 따라 Alpha 값 서서히 변경
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            currentAlpha = Mathf.Lerp(1, 0, t);

            // Material의 Alpha 값 설정
            Color gcolor = girl.color;
            gcolor.a = currentAlpha;
            girl.color = gcolor;

            Color hcolor = hairs.color;
            hcolor.a = currentAlpha;
            hairs.color = hcolor;

            Color ccolor = cloth.color;
            ccolor.a = currentAlpha;
            cloth.color = ccolor;

            
            yield return null;
        }
        transform.position = spawn;
    }
    public void over50per(){ //정신력 수치가 50퍼 이상일 때
        StartCoroutine(Fadein(1));
        nav.enabled=false;
        rigid.useGravity=false;
        over50perSound.Play();
        //플레이어의 뒤에 그리고 90도로 사라지기 
        GameObject Playertr = GameObject.Find("OVRPlayerController");
        //Plyaer포지션에 방향에서 12만큼만 빼준다.
        transform.position = Playertr.transform.position - Playertr.transform.forward * 4;
        transform.position -= new Vector3(0, 1.5f, 0);
        Quaternion newRotation = Quaternion.Euler(0f, Playertr.transform.rotation.y + 90, 0f);
        transform.rotation = newRotation;
        rigid.velocity = transform.forward * 1.5f;
        StartCoroutine(velocity0());
        
    }
    //PlayerScript에서 사용
    public void PlayerDIeAction(){
        diestart=true; //한번만 하기 위한 값
        nav.ResetPath();    
        Debug.Log("네비 초기화");
        anim.SetBool("walk",false);
        StartCoroutine(Fadein(1));
        rigid.useGravity = false;
        nav.enabled = false;
        SpotlightController light = GameObject.Find("SpotlightController").GetComponent<SpotlightController>();
        light.StartCoroutine(light.lightout());
    }
    private System.Collections.IEnumerator Fadeout()
    {
        anim.SetTrigger("lost");
        Debug.Log("사라진다");
        float elapsedTime = 0f; // 경과 시간
        yield return new WaitForSecondsRealtime(3f);
        while (elapsedTime < fadeDuration)
        {
            // 경과 시간에 따라 Alpha 값 서서히 변경
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            currentAlpha = Mathf.Lerp(1, 0, t);

            // Material의 Alpha 값 설정
            Color gcolor = girl.color;
            gcolor.a = currentAlpha;
            girl.color = gcolor;

            Color hcolor = hairs.color;
            hcolor.a = currentAlpha;
            hairs.color = hcolor;

            Color ccolor = cloth.color;
            ccolor.a = currentAlpha;
            cloth.color = ccolor;

            yield return null;
        }
        transform.position = spawn;
    }

    private System.Collections.IEnumerator Fadein(float duration)
    {
        Debug.Log("보인다");
        float elapsedTime = 0f; // 경과 시간

        while (elapsedTime < duration)
        {
            // 경과 시간에 따라 Alpha 값 서서히 변경
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            currentAlpha = Mathf.Lerp(0, 1, t);

            // Material의 Alpha 값 설정
            Color gcolor = girl.color;
            gcolor.a = currentAlpha;
            girl.color = gcolor;

            Color hcolor = hairs.color;
            hcolor.a = currentAlpha;
            hairs.color = hcolor;

            Color ccolor = cloth.color;
            ccolor.a = currentAlpha;
            cloth.color = ccolor;

            yield return null;
        }
    }
        //------------------------------------------------------------------------목표를 잃어버렸을때
        //targetCheck의 값을 결정한다. FlashLight 스크립트에서 사용
        //여기 bool값을 추가해서 숨는 방법을 추가 할 수 있다.
        
    public void FoundTarget(bool check) //check는 손전등이 켜져 있을때 true값을 반환한다.
    {
        if (!check && targetCheck == true)
        {
            Debug.Log("손전등이 안켜져있어서 놓침");
            StartCoroutine(lost(check)); //플레이어를 놓치면 잠깐 멈춤
        }
    }

    IEnumerator lost(bool check)
    {
        nav.ResetPath(); //navigation을 초기화 한다.
        anim.SetBool("walk", false);
        Debug.Log("잃어버렸다..");
        PlayerScript.attention_level = 0; //어그로 수치를 0으로 만든다.
        yield return new WaitForSecondsRealtime(3f);
        targetCheck = check;  //기본 움직임을 실행시킨다. 
        first=true;
        time = 0;
    }

    //---------------------------------------------------------------------------------목표 추적
    //목표를 향해 쫓아 간다.
    //ghostRange에서 호출할 것이다.
    //playerScript(어그로 수치가 꽉 차면)에서 사용한다.
    public void setDirection(Vector3 target)
    {
        if(targetCheck==false){
            Debug.Log("추적시작");
            targetCheck = true;
            first=true;
            //rigid.useGravity=true;
            nav.enabled = true;
            anim.SetBool("walk",true);
        }
        nav.SetDestination(target);
    }
    
}
