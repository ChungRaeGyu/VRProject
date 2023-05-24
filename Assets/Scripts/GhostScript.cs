using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//WayPoint를 사용해서 기본 움직임을 만들었다.
//Navigation을 사용해서 적에게 달려 간다.
//적에게 가다가 숨으면 잠시 멈췄다가 움직인다.
//적 식별관련 코드는 ghostRange에 있다.
public class GhostScript : MonoBehaviour
{
    public bool targetCheck=false;
    private NavMeshAgent nav;
    //-------------------------------------------------------추적
    float timer=5.0f;
    float time;
    public Transform Player;
    public float playerDistance=10f;
    /////////////////////////////////-----------------------------------------WayPoint
    public enum MoveType
    {
        WAY_POINT,
        LOOK_AT
    }
    public MoveType moveType = MoveType.WAY_POINT; //이동방식
    public float speed = 5.0f;  //이동 속도
    public float damping = 3.0f; //회전 시 회전 속도를 조절하는 계수

    private Transform tr;    //캐릭터의 Transform값을 담을 함수
    private Transform[] points; //웨이포인트를 저장할 배열
    private int nextldx = 1;    //다음에 이동해야 할 위치 인데스 변수
    //////////////////////////////////////////_--------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        //WayPointGroup 게임오브젝트 아래에 있는 모든 point의 Transform 컴포넌트를 추출
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        //플레이어를 식별 했을 때 와 안했을 때를 구분
        if(!targetCheck){
            switch (moveType)
            {
                case MoveType.WAY_POINT:
                    MoveWayPoint();
                    break;

                case MoveType.LOOK_AT:
                    break;
            }
        }else{
            //targetCheck가 true일때 시간 제한 //적을 쫓아 갈 때
            time+=Time.deltaTime;
            if(time>timer){
                StartCoroutine(lost(false)); //플레이어를 놓치면 잠깐 멈춤
                time=0;
            }

            //플레이어와 귀신의 거리 계산
            float distance = Vector3.Distance(transform.position, Player.position);
            //거리가 멀어지면 놓친다.
            if(distance >playerDistance){
                StartCoroutine(lost(false));
            }
            if(distance<1){
                GameObject.Find("Main Camera").GetComponent<Camera>().enabled=false;
                GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
                GameObject.Find("CatchCamera").GetComponent<Camera>().enabled =true;
                GameObject.Find("CatchCamera").GetComponent<AudioListener>().enabled = true;
            }
        }
    }

    //------------------------------------------------------------------------목표를 잃어버렸을때
    //targetCheck의 값을 결정한다. FlashLight 스크립트에서 사용
    public void FoundTarget(bool check){
        if(!check && targetCheck==true){
            StartCoroutine(lost(check)); //플레이어를 놓치면 잠깐 멈춤
        }
    }

    IEnumerator lost(bool check)
    {
        nav.ResetPath(); //navigation을 초기화 한다.
        playerDistance=1000f;
        Debug.Log("잃어버렸다..");
        yield return new WaitForSecondsRealtime(3f);
        targetCheck = check;  //기본 움직임을 실행시킨다.
        time=0;
        playerDistance = 10f;
    }
    //---------------------------------------------------------------------------------목표 추적
    //목표를 향해 쫓아 간다.
    //자손에서 호출할 것이다.
    public void setDirection(Vector3 target)
    {
        targetCheck = true;
        nav.SetDestination(target);
    }

    //--------------------------------------------------------------------------------기본이동
    void MoveWayPoint()
    {
        //현재 위치에서 다음 웨이포인트를 바라보는 벡터를 계산(거리와 방향을 계산한다.)
        Vector3 direction = points[nextldx].position - tr.position;
        //산출된 벡터의 회전 각도를 쿼터니언 타입으로 산출
        Quaternion rot = Quaternion.LookRotation(direction);
        //현재 각도에서 회전해야 할 각도까지 부드럽게 회전 처리
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        //전진 방향으로 이동처리
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
        //Vector3.forward 양의 z축 방향을 가리키는 벡터를 의미 (0,0,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WAY_POINT"))
        {
            //맨 마지막 웨이포인트에 도달했을 때 처음 인덱스로 변경
            nextldx = Random.Range(1,points.Length);
            Debug.Log(nextldx);
        }
    }
}
