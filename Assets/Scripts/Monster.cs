using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;


public class Monster : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �ǰ� �� Panel ����, HP ���� ���� cs ����
    /// </summary>
    public Text HPText;
    public float changeDuration = 0.5f; // ���� ���� ���� �ð�
    public GameObject GameLight; // ���� ������Ʈ���� ����Ʈ�� �������� ���� ����

    private bool isTriggered = false; // OnTriggerEnter �̺�Ʈ �߻� ���� ����� ����
    private float elapsedTime = 0.0f; // ��� �ð� ����� ����

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
            HPText.text = "���� HP :" + PlayerScript.PlayerHP;
        }
        else if(PlayerScript.PlayerDie == true)
        {
            HPText.text = "You Dead...";
        }
        if (isTriggered)
        {
            // ��� �ð� ����
            elapsedTime += Time.deltaTime;

            // 2�ʰ� ������ ���� ���
            if (elapsedTime < changeDuration)
            {

                AttackPanel.GetComponent<Image>().enabled = true;
            }
            // 2�ʰ� ���� ���
            else
            {
                AttackPanel.GetComponent<Image>().enabled = false;

                // ���� �ʱ�ȭ
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
