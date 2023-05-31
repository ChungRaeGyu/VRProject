using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;


public class Monster : MonoBehaviour
{
    /// <summary>
    /// 플레이어 피격 시 Panel 변경, HP 감소 관련 cs 파일
    /// </summary>
    public Text HPText;
    public float changeDuration = 0.5f; // 색상 변경 지속 시간
    public GameObject GameLight; // 게임 오브젝트에서 라이트를 가져오기 위한 변수

    private bool isTriggered = false; // OnTriggerEnter 이벤트 발생 여부 저장용 변수
    private float elapsedTime = 0.0f; // 경과 시간 저장용 변수

    public GameObject AttackPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.PlayerDie == false)
        {
            HPText.text = "현재 HP :" + PlayerScript.PlayerHP;
        }
        else if(PlayerScript.PlayerDie == true)
        {
            HPText.text = "You Dead...";
        }
        if (isTriggered)
        {
            // 경과 시간 증가
            elapsedTime += Time.deltaTime;

            // 2초가 지나지 않은 경우
            if (elapsedTime < changeDuration)
            {

                AttackPanel.GetComponent<Image>().enabled = true;
            }
            // 2초가 지난 경우
            else
            {
                AttackPanel.GetComponent<Image>().enabled = false;

                // 변수 초기화
                isTriggered = false;
                elapsedTime = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player" && !isTriggered)
        {
            PlayerScript.PlayerHP -= 1;
            isTriggered = true;
        }
    }
}
