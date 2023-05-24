using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Renderer renderer;

    private void Start()
    {
        // Renderer ������Ʈ ��������
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        // �ٸ� Collider�� �浹 ���� ���
        if (other.CompareTag("head")) // YourTag�� �ش� ������Ʈ�� �ĺ��ϴ� �±׷� �ٲ�� �մϴ�
        {
            // ������ ����
            renderer.enabled = false;
        }
    }
}
