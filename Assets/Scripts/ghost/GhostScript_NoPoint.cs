using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Navigation을 사용해서 적에게 달려 간다.
//적에게 가다가 숨으면 잠시 멈췄다가 움직인다.
//적 식별관련 코드는 ghostRange에 있다.
public class GhostScript_NoPoint : MonoBehaviour
{
    public bool targetCheck = false;
    private NavMeshAgent nav;
    //-------------------------------------------------------추적
    float timer = 10.0f;
    float time;
    public Transform Player;
    public float playerDistance = 10f;
    
    public float speed = 5.0f;  //이동 속도
    public float damping = 3.0f; //회전 시 회전 속도를 조절하는 계수

    private Transform tr;    //캐릭터의 Transform값을 담을 함수
    private Material girl;
    private Material cloth;
    private Material hairs;
    public float fadeDuration = 3f; // 서서히 투명해지는 시간
    private float currentAlpha; // 현재 Alpha 값
    //////////////////////////////////////////_--------------------------------------------
    bool first = true; //한번만 실행하기 위한
    //--------------------------------------------------------die
    public bool diestart=false; //정신력이 0이 되었을 때 //그냥 static으로 써버리고 해도 될듯?
    bool die=true;
    //--------------------------------------------Anim
    Animator anim;
    //-------------------------------------------------ghost audio
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        cloth = transform.GetChild(0).GetComponent<Renderer>().material;
        girl = transform.GetChild(1).GetComponent<Renderer>().material;
        hairs = transform.GetChild(2).GetComponent<Renderer>().material;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        audioSource = GameObject.Find("Spot Light").GetComponent<AudioSource>();
        //초기값 저장
    }
    // Update is called once per frame
    void Update()
    {
        //플레이어를 식별 했을 때 와 안했을 때를 구분
        //targetCheck는 setDirection함수에 있다.
        if(!diestart){
            if (!targetCheck)
            {
                //플레이어를 놓치면 사라진다.
                //first는 한번만 실행하기 위한 것이다.
                if (first)
                {
                    first = false;
                    StartCoroutine(Fadeout());
                }
            }
            else
            {
                if (first)
                {
                    first = false;
                    StartCoroutine(Fadein(fadeDuration));
                }

                //targetCheck가 true일때 시간 제한 //적을 쫓아 갈 때
                time += Time.deltaTime;
                if (time > timer)
                {
                    StartCoroutine(lost(false)); //플레이어를 놓치면 잠깐 멈춤
                    time = 0;
                }

                //플레이어와 귀신의 거리 계산
                float distance = Vector3.Distance(transform.position, Player.position);
                //거리가 멀어지면 놓친다.                    
                if (distance > playerDistance)
                {
                    Debug.Log("거리가 멀어져서 놓침");
                    StartCoroutine(lost(false));
                }
                if (distance < 1)
                {
                    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
                    GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
                    GameObject.Find("CatchCamera").GetComponent<Camera>().enabled = true;
                    GameObject.Find("CatchCamera").GetComponent<AudioListener>().enabled = true;
                }
            }
        }else{
            if(die){
                die=false;
                audioSource.Play();
                StartCoroutine(Fadein(1));
                SpotlightController light =GameObject.Find("SpotlightController").GetComponent<SpotlightController>();
                light.StartCoroutine(light.lightout());
            }
        }
    }
    private System.Collections.IEnumerator Fadeout()
    {
        anim.SetTrigger("lost");
        Debug.Log("사라진다");
        float elapsedTime = 0f; // 경과 시간
        yield return new WaitForSecondsRealtime(10f);
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
        playerDistance = 1000f;
        Debug.Log("잃어버렸다..");
        PlayerScript.attention_level = 0; //어그로 수치를 0으로 만든다.
        yield return new WaitForSecondsRealtime(3f);
        targetCheck = check;  //기본 움직임을 실행시킨다.
        first=true;
        time = 0;
        playerDistance = 10f;
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
        }
        nav.SetDestination(target);
    }
}
