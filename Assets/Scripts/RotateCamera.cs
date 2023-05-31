using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Renderer renderers;

    private void Start()
    {
        // Renderer 컴포넌트 가져오기
        renderers = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        // 다른 Collider와 충돌 중인 경우
        if (other.CompareTag("head")) // YourTag는 해당 오브젝트를 식별하는 태그로 바꿔야 합니다
        {
            // 렌더링 끄기
            renderers.enabled = false;
        }
    }
}
