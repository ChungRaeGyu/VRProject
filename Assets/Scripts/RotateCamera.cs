using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Renderer renderers;

    private void Start()
    {
        // Renderer ������Ʈ ��������
        renderers = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        // �ٸ� Collider�� �浹 ���� ���
        if (other.CompareTag("head")) // YourTag�� �ش� ������Ʈ�� �ĺ��ϴ� �±׷� �ٲ�� �մϴ�
        {
            // ������ ����
            renderers.enabled = false;
        }
    }
}
